using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialHero : MonoBehaviour
{

    public delegate void HeroDestroyAction();
    public static event HeroDestroyAction HeroDestroy;

    public GameObject projectile;
    public GameObject mineExplosion;
    public Transform projectileReleaseTransform;
    public Transform spawnParticle;
    public AudioClip explosionClip;

    private List<GameObject> enemiesInRange = new List<GameObject>();
    private int currentDestination;
    private NavMeshAgent controller;

    //private Animation heroAnimation;
    private Animator heroAnimation;
    private AudioSource playerAudio;
    private int path = 0;
    private bool isAttacking = false;
    private bool isStarted = false;

    
    void Awake()
    {
        controller = GetComponent<NavMeshAgent>();
        heroAnimation = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

    }

    void Start()
    {
        path = Mathf.CeilToInt(Random.Range(0, GameManager.gameManager.enemyDestination.Count));
        currentDestination = GameManager.gameManager.enemyDestination[path].Count - 2;
        HeroDestroy += DestroyHero;
        Invoke("StartHeroDelayed", 0.5f);
    }

    public void StartHero()
    {
        if(!isStarted)
        {
            isStarted = true;
            controller.SetDestination(GameManager.gameManager.enemyDestination[path][currentDestination][0].position);
        }
        
    }

	//It is used incase StartHero is not called - That happens because the animation ends early and event never get a chance run
    void StartHeroDelayed()
    {
        if (!isStarted)
        {
            isStarted = true;
            controller.SetDestination(GameManager.gameManager.enemyDestination[path][currentDestination][0].position);
        }
    }

    void Update()
    {
        if (currentDestination > 0 && !GameManager.gameManager.IsLevelEnded())
        {
            if (Vector3.Distance(transform.position, GameManager.gameManager.enemyDestination[path][currentDestination][0].position) <= 0.5f)
            {
                
                currentDestination--;
                controller.SetDestination(GameManager.gameManager.enemyDestination[path][currentDestination][0].position);
            }
        }
        else if(currentDestination == 0 && !GameManager.gameManager.IsLevelEnded())
        {
            
            if (Vector3.Distance(transform.position, GameManager.gameManager.enemyDestination[path][currentDestination][0].position) <= 0.5f)
            {
                currentDestination--;
                controller.SetDestination(GameManager.gameManager.spawnPoints[path].position);
            }
        }
        else if(Vector3.Distance(transform.position, GameManager.gameManager.spawnPoints[path].position) <= 0.5f)
        {
            Instantiate(mineExplosion, transform.position - (transform.forward * 1), transform.rotation);
            
            Destroy(gameObject);
            HeroDestroy -= DestroyHero;
        }

    }
    
    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {

            if(!enemy.GetComponent<Enemy>().isDead && !enemy.GetComponent<Enemy>().isSuccess)
            {
                controller.Stop();

                enemiesInRange.Add(enemy.gameObject);
                if (!isAttacking)
                {
                    StartCoroutine(Attack());
                }
                /*
                if (!enemy.GetComponent<Enemy>().isBoss)
                {
                    
                }*/
                
            }
            
            
            //GetComponent<Animation>().Play("attack01");
            //enemy.GetComponent<Enemy>().Dead();
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        //heroAnimation.Play("attack01");
        heroAnimation.SetTrigger("Attack");
        playerAudio.Play();

        yield return new WaitForSeconds(0.5f);

        if(enemiesInRange.Count != 0)
        {
			foreach (var enemy in enemiesInRange)
			{
                if(enemy != null)
                {
                    if (!enemy.GetComponent<Enemy>().isDead && !enemy.GetComponent<Enemy>().isSuccess)
                    {
                        //enemiesInRange.Remove(enemy);
                        if (!enemy.GetComponent<Enemy>().isBoss)
                        {

                            enemy.GetComponent<Enemy>().Dead();
                        }
                        else
                        {
                            enemy.GetComponent<Enemy>().TakeDamage(enemy.GetComponent<Enemy>().GetMaxHealth() / 5, false, null);
                            //enemiesInRange.Clear ();
                        }
                    }
                }
				
            }
            Instantiate(projectile, projectileReleaseTransform.position, transform.rotation);
            
            enemiesInRange.Clear();
        }

        if(enemiesInRange.Count != 0)
        {
            StartCoroutine(Attack());
        }
        else
        {
            //heroAnimation.Play("run");
            heroAnimation.SetTrigger("Run");
            controller.Resume();
            isAttacking = false;           
        }
    }

    public static void DestorySpecialHeroes()
    {
        if(HeroDestroy != null)
        {
            HeroDestroy();
        }
    }
    void DestroyHero()
    {
        Destroy(gameObject);
        HeroDestroy -= DestroyHero;
    }

    public void PlayAttackSound()
    {
        playerAudio.Play();
    }
}
