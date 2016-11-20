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
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private int currentDestination;
    private NavMeshAgent controller;
    //private Animation heroAnimation;
    private Animator heroAnimation;
    private int path = 0;
    private bool isAttacking = false;
    private bool isStarted = false;

    
    void Awake()
    {
        controller = GetComponent<NavMeshAgent>();
        //heroAnimation = GetComponent<Animation>();
        heroAnimation = GetComponent<Animator>();
        
    }

    void Start()
    {
        path = Mathf.CeilToInt(Random.Range(0, GameManager.gameManager.enemyDestination.Count));
        currentDestination = GameManager.gameManager.enemyDestination[path].Count - 2;
        HeroDestroy += DestroyHero;
        Invoke("StartHeroDelayed", 0.5f);
        //Debug.Log(currentDestination);
        //heroAnimation.Play("run");
    }

    public void StartHero()
    {
        isStarted = true;
        controller.SetDestination(GameManager.gameManager.enemyDestination[path][currentDestination][0].position);
        
        //spawnParticle.gameObject.SetActive(false);
    }

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

            if(!enemy.GetComponent<Enemy>().isDead)
            {
                controller.Stop();

                enemiesInRange.Add(enemy.gameObject);
                if (!isAttacking)
                {
                    StartCoroutine(Attack());
                }
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

        yield return new WaitForSeconds(0.5f);

        if(enemiesInRange.Count != 0)
        {
            foreach(var enemy in enemiesInRange)
            {
                //enemiesInRange.Remove(enemy);
                enemy.GetComponent<Enemy>().Dead();
                
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
}
