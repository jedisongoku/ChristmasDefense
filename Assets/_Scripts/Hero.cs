﻿using UnityEngine;
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
    public Transform wizardUpgradeParticle;


    private List<GameObject> enemiesInRange = new List<GameObject>();
    private Animator playerAnimation;
    private AudioSource playerAudio;
    private float attackTimer = 0f;
    private bool isFirstShot = true;
    private float autoClearEnemyListTimer = 0f;
    
 
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
        autoClearEnemyListTimer = 0f;
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
            if (heroID == 3)
            {
                wizardUpgradeParticle.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        autoClearEnemyListTimer += Time.deltaTime;

        if(autoClearEnemyListTimer >= 5f)
        {
            autoClearEnemyListTimer = 0f;
            enemiesInRange.Clear();
        }

        if(!GameManager.gameManager.IsLevelEnded())
        {
            if (enemiesInRange.Count != 0)
            {

                //if (enemiesInRange[0].GetComponent<Enemy>() != null)
                if (enemiesInRange[0] != null)
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
                    else
                    {
                        RemoveEnemy(enemiesInRange[0]);
                        Debug.Log("Remove Enemy that is already dead");
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
            
            if(enemiesInRange[0] != null)
            {
                if (!enemiesInRange[0].GetComponent<Enemy>().isDead)
                {
                    if(!enemiesInRange[0].GetComponent<Enemy>().isSuccess)
                    {
                        if (isUpgraded)
                        {
                            playerAnimation.SetTrigger("Attack2");
                            if(heroID == 3)
                            {
                                wizardUpgradeParticle.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            playerAnimation.SetTrigger("Attack");
                        }
                    }
                    else
                    {
						playerAnimation.Stop ();
                        if(heroID == 3)
                        {
                            wizardUpgradeParticle.gameObject.SetActive(false);
                        }
                        RemoveEnemy(enemiesInRange[0]);
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
                    
                    if (enemiesInRange[0] != null)
                    {
						GameObject particle = Instantiate(projectile, projectileReleaseTransform.position, transform.rotation) as GameObject;
						particle.GetComponent<FX_Mover>().SetTarget(enemiesInRange[0]);
                        enemiesInRange[0].GetComponent<Enemy>().TakeDamage(heroDamage, false, gameObject);
                    }
					else
					{
						enemiesInRange.Remove(enemiesInRange[0]);
					}
                }
                else
                {
                    
                    if (enemiesInRange[0] != null)
                    {
						GameObject particle = Instantiate(projectileUpgraded, projectileReleaseTransform.position, transform.rotation) as GameObject;
						particle.GetComponent<FX_Mover>().SetTarget(enemiesInRange[0]);
                        if(heroID == 1)
                        {
                            enemiesInRange[0].GetComponent<Enemy>().TakeDamage(heroDamage, true, gameObject);
                        }
                        else
                        {
                            enemiesInRange[0].GetComponent<Enemy>().TakeDamage(heroDamage, false, gameObject);
                        }
                    }
					else
					{
						enemiesInRange.Remove(enemiesInRange[0]);
					}
                    
                }
            }
            else
            {
                if (!isUpgraded)
                {
                    if (enemiesInRange[0] != null)
                    {
						GameObject particle = Instantiate(projectile, projectileReleaseTransform.position, transform.rotation) as GameObject;
						particle.GetComponent<FX_Mover>().SetTarget(enemiesInRange[0]);
                        enemiesInRange[0].GetComponent<Enemy>().TakeDamage(heroDamage, false, gameObject);
                    }
					else
					{
						enemiesInRange.Remove(enemiesInRange[0]);
					}
                }
                else
                {
                    Collider[] enemies = Physics.OverlapSphere(gameObject.transform.position, heroAttackRadius, LayerMask.GetMask("Enemy"));

                    foreach (var enemy in enemies)
                    {
                        //if (enemy.GetComponent<Enemy>() != null)
                        if (enemy != null)
                        {
                            if (!enemy.GetComponent<Enemy>().isDead)
                            {
                                Instantiate(projectileUpgraded, enemy.transform.position + (enemy.transform.forward * 1), transform.rotation);
                                enemy.GetComponent<Enemy>().TakeDamage((int)(heroDamage*1.25), false, gameObject);

                            }
                        }
						else
						{
							enemiesInRange.Remove(enemiesInRange[0]);
						}

                    }
                    wizardUpgradeParticle.gameObject.SetActive(false);
                }
            }
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
