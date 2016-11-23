using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hero : MonoBehaviour {

    public int heroID;
    public int heroDamage;
    public float heroAttackSpeed;
    public float heroAttackRadius;
    public int upgradeCost;
    public bool isUpgraded = false;

    public GameObject projectile;
    public GameObject projectileUpgraded;
    public Transform projectileReleaseTransform;
    public Transform radius;
    public SkinnedMeshRenderer skin;
    public SkinnedMeshRenderer tigerProjectileSkin;
    public MeshRenderer projectileSkin;
    public Material upgradeSkinMaterial;
    public AudioClip baseProjectileSound;
    public AudioClip upgradedProjectileSound;


    private List<GameObject> enemiesInRange = new List<GameObject>();
    private Animator playerAnimation;
    private AudioSource playerAudio;
    private float attackTimer = 0f;
    private bool isFirstShot = true;
    
 
    void Awake()
    {
        GetComponent<SphereCollider>().radius = heroAttackRadius;
        playerAnimation = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        radius.parent = GameManager.gameManager.selectedSpawnPoint.transform;

        //transform.rotation = Quaternion.identity;
        //playerAttackSpeed = 1 / playerAttackSpeed;
    }

    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            //Debug.Log(enemy.gameObject);
            enemiesInRange.Add(enemy.gameObject);
        }
    }

    void OnTriggerExit(Collider enemy)
    {
        if(enemy.CompareTag("Enemy"))
        {
            //Debug.Log("Enemy Removed");
            RemoveEnemy(enemy.gameObject);
            
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if(!GameManager.gameManager.IsLevelEnded())
        {
            if (enemiesInRange.Count != 0)
            {
                if (enemiesInRange[0].GetComponent<Enemy>() != null)
                {
                    if (enemiesInRange[0].GetComponent<Enemy>().isDead)
                    {
                        RemoveEnemy(enemiesInRange[0]);
                    }
                    else
                    {
                        transform.LookAt(enemiesInRange[0].transform.position);
                        Attack();
                    }
                }


            }
        }
        
        
    }

    void Attack()
    {
        if(attackTimer >= heroAttackSpeed || isFirstShot)
        {
            isFirstShot = false;
            attackTimer = 0f;
            
            if(enemiesInRange[0].GetComponent<Enemy>() != null)
            {
                if (!enemiesInRange[0].GetComponent<Enemy>().isDead)
                {
                    if(isUpgraded)
                    {
                        playerAnimation.SetTrigger("Attack2");
                    }
                    else
                    {
                        playerAnimation.SetTrigger("Attack");
                    }
                    

                    
                    //Call Take Damage in AttackParticle
                    //enemiesInRange[0].GetComponent<Enemy>().TakeDamage(heroDamage, playerDebuff, gameObject);
                }
            }
            else
            {
                enemiesInRange.Remove(enemiesInRange[0]);
            }
            
        }
    }

    public void AttackParticle()
    {
        if (enemiesInRange.Count > 0)
        {
            if(heroID != 3)
            {
                if (!isUpgraded)
                {
                    Instantiate(projectile, projectileReleaseTransform.position, transform.rotation);
                    enemiesInRange[0].GetComponent<Enemy>().TakeDamage(heroDamage, false, gameObject);
                }
                else
                {
                    Instantiate(projectileUpgraded, projectileReleaseTransform.position, transform.rotation);
                    enemiesInRange[0].GetComponent<Enemy>().TakeDamage(heroDamage, true, gameObject);
                }
            }
            else
            {
                if (!isUpgraded)
                {
                    Instantiate(projectile, projectileReleaseTransform.position, transform.rotation);
                    enemiesInRange[0].GetComponent<Enemy>().TakeDamage(heroDamage, false, gameObject);
                }
                else
                {
                    Collider[] enemies = Physics.OverlapSphere(gameObject.transform.position, heroAttackRadius, LayerMask.GetMask("Enemy"));

                    foreach (var enemy in enemies)
                    {
                        if (enemy.GetComponent<Enemy>() != null)
                        {
                            if (!enemy.GetComponent<Enemy>().isDead)
                            {
                                Instantiate(projectileUpgraded, enemy.transform.position + (enemy.transform.forward * 1), transform.rotation);
                                enemy.GetComponent<Enemy>().TakeDamage(heroDamage, false, gameObject);

                            }
                        }

                    }
                }
            }
            
            /*
            switch (heroID)
            {
                case 1:
                    
                    if(!isUpgraded)
                    {
                        Instantiate(Resources.Load("Archer_Projectile"), projectileReleaseTransform.position, transform.rotation);
                        enemiesInRange[0].GetComponent<Enemy>().TakeDamage(heroDamage, false, gameObject);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Archer_UpgradedProjectile"), projectileReleaseTransform.position, transform.rotation);
                        enemiesInRange[0].GetComponent<Enemy>().TakeDamage(heroDamage, true, gameObject);
                    }
                    break;
                case 2:
                    
                    if (!isUpgraded)
                    {
                        Instantiate(Resources.Load("Spartan_Projectile"), projectileReleaseTransform.position, transform.rotation);
                        enemiesInRange[0].GetComponent<Enemy>().TakeDamage(heroDamage, false, gameObject);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Spartan_UpgradedProjectile"), projectileReleaseTransform.position, transform.rotation);
                        enemiesInRange[0].GetComponent<Enemy>().TakeDamage(heroDamage, false, gameObject);
                    }
                    break;
                case 3:
                    
                    if (!isUpgraded)
                    {
                        Instantiate(Resources.Load("Wizard_Projectile"), projectileReleaseTransform.position, transform.rotation);
                        enemiesInRange[0].GetComponent<Enemy>().TakeDamage(heroDamage, false, gameObject);
                    }
                    else
                    {
                        Collider[] enemies = Physics.OverlapSphere(gameObject.transform.position, heroAttackRadius, LayerMask.GetMask("Enemy"));
                        
                        foreach (var enemy in enemies)
                        {
                            if(enemy.GetComponent<Enemy>() != null)
                            {
                                if(!enemy.GetComponent<Enemy>().isDead)
                                {
                                    Instantiate(Resources.Load("Wizard_UpgradedProjectile"), enemy.transform.position + (enemy.transform.forward * 1), transform.rotation);
                                    enemy.GetComponent<Enemy>().TakeDamage(heroDamage, false, gameObject);

                                }
                            }
                            
                        }
                    }
                    break;

            }
            */
        }
    }

    public void PlayAttackSound()
    {
        playerAudio.Play();
    }

    public void Upgrade()
    {
        Player.resource -= upgradeCost;
        isUpgraded = true;
        skin.material = upgradeSkinMaterial;
        playerAudio.clip = upgradedProjectileSound;
        GameHUDManager.gameHudManager.GameHudUpdate();

        switch(heroID)
        {
            case 1:
                tigerProjectileSkin.material = upgradeSkinMaterial;
                break;
            case 2:
                heroAttackSpeed *= 0.6f;
                projectileSkin.material = upgradeSkinMaterial;
                break;
            case 3:
                heroDamage = (int)(heroDamage * 1.2f);
                projectileSkin.material = upgradeSkinMaterial;
                break;
        }
    }


}
