using UnityEngine;
using System.Collections;

public class SpecialHero : MonoBehaviour
{

    private int currentDestination;
    private NavMeshAgent controller;
    private Animator animator;


    void Awake()
    {
        controller = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentDestination = GameManager.gameManager.enemyDestination.Count - 2;
        controller.SetDestination(GameManager.gameManager.enemyDestination[currentDestination][0].position);
    }

    void Update()
    {

        if (currentDestination > 0 && !GameManager.gameManager.IsLevelEnded())
        {
            if (Vector3.Distance(transform.position, GameManager.gameManager.enemyDestination[currentDestination][0].position) <= 0.5f)
            {
                currentDestination--;
                controller.SetDestination(GameManager.gameManager.enemyDestination[currentDestination][0].position);
            }
        }
        else if(currentDestination == 0 && !GameManager.gameManager.IsLevelEnded())
        {
            
            if (Vector3.Distance(transform.position, GameManager.gameManager.enemyDestination[currentDestination][0].position) <= 0.5f)
            {
                currentDestination--;
                controller.SetDestination(GameManager.gameManager.spawnPoints[GameManager.gameManager.level - 1].position);
            }
        }
        else if(controller.velocity.magnitude < 0.5)
        {
            Instantiate(Resources.Load("Mine_Explosion"), transform.position - (transform.forward * 1), transform.rotation);
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            enemy.GetComponent<Enemy>().Dead();
        }
    }
}
