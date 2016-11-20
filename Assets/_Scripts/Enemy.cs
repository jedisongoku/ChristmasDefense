using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public delegate void RestartLevelAction();
    public static event RestartLevelAction DestroyAll;

    public int enemyId;
    public int enemyHealth;
    public float enemySpeed;
    public int enemyCoin;
    public bool isDead;
    public bool isFirstHit;
    public bool isFirstHeal;
    public bool canTakeDamage;
    public int sinkSpeed;
    public int enemyPath;
    public Transform healthBar;
    public Image healthImage;

    public ParticleSystem slowDownParticle;
    public ParticleSystem dotParticle;

    
    private NavMeshAgent enemyController;
    private Animator enemyAnimation;
    private MonsterHornet monsterHornet;
    private MushroomMonster mushroomMonster;
    //private Collider enemyCollider;
    private int currentDestination = 0;
    private int randomDestinationIndex;
    private int enemyHealthMax;
    private bool canSlowDown = true;
    private bool canDebuffed = true;
    private float debufTimer;
    private float debufTime = 3;

    void OnEnable()
    {

        enemyController = GetComponent<NavMeshAgent>();
        if (enemyId != 7)
        {
            enemyAnimation = GetComponent<Animator>();
        }




        DestroyAll += DestroyOnRestart;

        //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        if (enemyId == 4)
        {

        }
        else if (enemyId == 5)
        {
            monsterHornet = GetComponent<MonsterHornet>();
        }
        else if (enemyId == 7)
        {
            mushroomMonster = GetComponent<MushroomMonster>();
            Debug.Log("MUSHROOM");
        }


        //enemyCollider = GetComponent<Collider>();

        enemyHealth = (int)(enemyHealth * GameManager.gameManager.healthMultiplier);
        //Debug.Log(name + " health " + enemyHealth);
        enemyHealthMax = enemyHealth;

        currentDestination = 0;
        isDead = false;
        isFirstHit = true;
        isFirstHeal = true;
        canTakeDamage = true;
        healthBar.gameObject.SetActive(true);
        slowDownParticle.Stop();
        dotParticle.Stop();

        if(GameManager.gameManager.level == 3 && GameManager.gameManager.GetCurrentWave() == 15)
        {
            enemyHealth = 1166;
            enemyHealthMax = enemyHealth;
            gameObject.transform.localScale = new Vector3(2, 2, 2);
        }
        
    }

    void Start()
    {
        enemyController.speed *= enemySpeed;
        randomDestinationIndex = Mathf.CeilToInt(Random.Range(0, GameManager.gameManager.enemyDestination[enemyPath][currentDestination].Count));
        enemyController.SetDestination(GameManager.gameManager.enemyDestination[enemyPath][currentDestination][randomDestinationIndex].position);
        healthImage.fillAmount = (float)enemyHealth / (float)enemyHealthMax;
        
    }

    void Update()
    {
        debufTimer += Time.deltaTime;

        if (currentDestination < GameManager.gameManager.enemyDestination[enemyPath].Count - 1 && !isDead && !GameManager.gameManager.IsLevelEnded())
        {
            if (Vector3.Distance(transform.position, GameManager.gameManager.enemyDestination[enemyPath][currentDestination][randomDestinationIndex].position) <= 0.5f)
            {
                currentDestination++;
                randomDestinationIndex = Mathf.CeilToInt(Random.Range(0, GameManager.gameManager.enemyDestination[enemyPath][currentDestination].Count));
                enemyController.SetDestination(GameManager.gameManager.enemyDestination[enemyPath][currentDestination][randomDestinationIndex].position);
            }
        }
    }


    public void TakeDamage(int damage, bool debuff, GameObject player)
    {
        //Debug.Log(damage);
        if (!canDebuffed)
        {
            damage = (int)(damage * 1.15f);
        }

        if (enemyHealth - damage > 0)
        {
            if(enemyId == 5 && isFirstHit)
            {
                //Debug.Log("Dodge HITS");
                isFirstHit = false;
                canTakeDamage = false;
                monsterHornet.DodgeHits();
            }
            else if(enemyId == 7 && (float)enemyHealth / (float)enemyHealthMax < 0.5f && isFirstHeal)
            {
                isFirstHeal = false;
                mushroomMonster.Heal();

            }
            else if(canTakeDamage)
            {
                if(enemyId != 7)
                {
                    enemyAnimation.SetTrigger("Take Damage");
                }
                else
                {
                    StartCoroutine(mushroomMonster.TakeDamageAnimation());
                    //mushroomMonster.TakeDamageAnimation();
                }
                
                //Debug.Log("TAKING DAMAGE");
                enemyHealth -= damage;

                healthImage.fillAmount = (float)enemyHealth / (float)enemyHealthMax;
                if(debuff)
                {
                    if(canDebuffed)
                    {
                        canDebuffed = false;
                        debufTimer = 0;
                        StartCoroutine(DamageOverTime());
                        dotParticle.Play();
                    }
                    
                }
            }
            
        }
        else
        {

            Dead();
            
            if(player != null)
            {
                player.GetComponent<Hero>().RemoveEnemy(gameObject);
            }
            
            
        }
        
    }

    public void Dead()
    {
        if(!isDead)
        {
            isDead = true;
            enemyController.Stop();
            StopAllCoroutines();
            slowDownParticle.Stop();
            dotParticle.Stop();
            GameManager.gameManager.EnemyDead();
            healthBar.gameObject.SetActive(false);
            Player.resource += enemyCoin;
            Player.score += enemyCoin * 101;
            GameHUDManager.gameHudManager.GameHudUpdate(); 
            enemyController.enabled = false;
            if(enemyId != 7)
            {
                enemyAnimation.SetTrigger("Die");
            }
            else
            {
                StartCoroutine(mushroomMonster.DieAnimation());
            }
            
            Invoke("SinkEnemy", 3);
            DestroyAll -= DestroyOnRestart;
            //Destroy(this, 2);

        }

    }

    public void Success()
    {
        if(!isDead)
        {
            StopAllCoroutines();
            healthBar.gameObject.SetActive(false);
            enemyController.Stop();
            GameManager.gameManager.EnemyDead();
            GameHUDManager.gameHudManager.GameHudUpdate();
            slowDownParticle.Stop();
            dotParticle.Stop();
            Invoke("SinkEnemy", 0);
            DestroyAll -= DestroyOnRestart;

        }
        



        
    }

    public void SinkEnemy()
    {
        StartCoroutine(Sink());
        Destroy(gameObject, 2);
    }

    IEnumerator Sink()
    {
        transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);

        yield return new WaitForSeconds(0);

        StartCoroutine(Sink());
    }


    public void SlowEnemy()
    {
        if(canSlowDown)
        {
            canSlowDown = false;
            StartCoroutine(Slow());
        }
        
    }

    IEnumerator Slow()
    {
        enemyController.speed *= 0.7f;
        slowDownParticle.Play();
        Debug.Log("SLOWEEEDDDD");

        yield return new WaitForSeconds(3);

        enemyController.speed = 2 * enemySpeed;
        slowDownParticle.Stop();
        canSlowDown = true;
    }

    IEnumerator DamageOverTime()
    {
        //Debug.Log("DOT");

        TakeDamage(2, false, null);
       
        yield return new WaitForSeconds(0.5f);


        if(debufTimer < debufTime)
        {
            StartCoroutine(DamageOverTime());
        }
        else
        {
            StopCoroutine(DamageOverTime());
            canDebuffed = true;
            dotParticle.Stop();
        }

    }

    public static void DestroyAllEnemies()
    {
        if(DestroyAll != null)
        {
            DestroyAll();
        }
        
    }

    void DestroyOnRestart()
    {
        DestroyAll -= DestroyOnRestart;
        Destroy(gameObject);
    }

    public int GetMaxHealth()
    {
        return enemyHealthMax;
    }
}
