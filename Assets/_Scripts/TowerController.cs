using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerController : Tower
{
    
    //private Animator playerAnimation;
    private AudioSource playerAudio;
    private float attackTimer = 0f;
    private bool isFirstShot = true;
    private float autoClearEnemyListTimer = 0f;

    [Header("IGT")]
    public Transform towerBase;
    public float turretLookAtAngle_X;

    public delegate void TowerAction();
    public static event TowerAction DestroyTowers;

    void Awake()
    {
        GetComponent<SphereCollider>().radius = towerAttackRadius;
        //playerAnimation = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        radius.parent = GameManager.gameManager.selectedSpawnPoint.transform;

        DestroyTowers += DestroyTower;

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
        if (enemy.CompareTag("Enemy"))
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
            if (towerID == 3)
            {
                //wizardUpgradeParticle.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        autoClearEnemyListTimer += Time.deltaTime;

        if (autoClearEnemyListTimer >= 5f)
        {
            autoClearEnemyListTimer = 0f;
            enemiesInRange.Clear();
        }

        if (!GameManager.gameManager.IsLevelEnded())
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
                            /*towerBase.LookAt(enemiesInRange[0].transform.position);
                            towerBase.rotation = Quaternion.Euler(new Vector3(0, towerBase.rotation.y, 0));*/
                            //transform.LookAt(enemiesInRange[0].transform.position);
                            if (towerID != 3)
                            {
                                towerBase.LookAt(enemiesInRange[0].transform.position);
                                Vector3 eulerAngles = towerBase.rotation.eulerAngles;
                                eulerAngles = new Vector3(turretLookAtAngle_X, eulerAngles.y, 0);
                                towerBase.rotation = Quaternion.Euler(eulerAngles);
                            }


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
        if (attackTimer >= towerAttackSpeed || isFirstShot)
        {
            isFirstShot = false;
            attackTimer = 0f;

            if (enemiesInRange[0] != null)
            {
                if (!enemiesInRange[0].GetComponent<Enemy>().isDead)
                {
                    if (!enemiesInRange[0].GetComponent<Enemy>().isSuccess)
                    {/*
                        if (isUpgraded)
                        {
                            playerAnimation.SetTrigger("Attack2");
                            if (towerID == 3)
                            {
                                //wizardUpgradeParticle.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            playerAnimation.SetTrigger("Attack");
                        }*/
                        SelectAttack();
                    }
                    else
                    {/*
                        playerAnimation.Stop();
                        if (towerID == 3)
                        {
                            //wizardUpgradeParticle.gameObject.SetActive(false);
                        }*/
                        RemoveEnemy(enemiesInRange[0]);
                    }
                    //Call Take Damage in AttackParticle
                    //enemiesInRange[0].GetComponent<Enemy>().TakeDamage(towerDamage, playerDebuff, gameObject);
                }
            }
            else
            {
                enemiesInRange.Remove(enemiesInRange[0]);
            }

        }
    }

    public void SelectAttack()
    {
        if (enemiesInRange.Count > 0)
        {
            if (enemiesInRange[0] != null)
            {
                Invoke(towerAttack.ToString(), 0);
            }
            else
            {
                enemiesInRange.Remove(enemiesInRange[0]);
            }


            if (towerID != 3)
            {
               
                /*
                if (!isUpgraded)
                {

                    
                }
                else
                {

                    if (enemiesInRange[0] != null)
                    {
                        GameObject particle = Instantiate(projectileUpgraded, projectileReleaseTransform.position, transform.rotation) as GameObject;
                        particle.GetComponent<FX_Mover>().SetTarget(enemiesInRange[0]);
                        if (towerID == 1)
                        {
                            enemiesInRange[0].GetComponent<Enemy>().TakeDamage(towerDamage, true, gameObject);
                        }
                        else
                        {
                            enemiesInRange[0].GetComponent<Enemy>().TakeDamage(towerDamage, false, gameObject);
                        }
                    }
                    else
                    {
                        enemiesInRange.Remove(enemiesInRange[0]);
                    }

                }*/
            }
            /*
            else
            {
                if (!isUpgraded)
                {
                    if (enemiesInRange[0] != null)
                    {
                        GameObject particle = Instantiate(projectile, projectileReleaseTransform.position, transform.rotation) as GameObject;
                        particle.GetComponent<FX_Mover>().SetTarget(enemiesInRange[0]);
                        enemiesInRange[0].GetComponent<Enemy>().TakeDamage(towerDamage, false, gameObject);
                    }
                    else
                    {
                        enemiesInRange.Remove(enemiesInRange[0]);
                    }
                }
                else
                {
                    Collider[] enemies = Physics.OverlapSphere(gameObject.transform.position, towerAttackRadius, LayerMask.GetMask("Enemy"));

                    foreach (var enemy in enemies)
                    {
                        //if (enemy.GetComponent<Enemy>() != null)
                        if (enemy != null)
                        {
                            if (!enemy.GetComponent<Enemy>().isDead)
                            {
                                Instantiate(projectileUpgraded, enemy.transform.position + (enemy.transform.forward * 1), transform.rotation);
                                enemy.GetComponent<Enemy>().TakeDamage((int)(towerDamage * 1.25), false, gameObject);

                            }
                        }
                        else
                        {
                            enemiesInRange.Remove(enemiesInRange[0]);
                        }

                    }
                    //wizardUpgradeParticle.gameObject.SetActive(false);
                }
            }*/
        }
    }

    public void PlayAttackSound()
    {
        playerAudio.Play();
    }

    /*
    public void Upgrade()
    {
        Player.resource -= upgradeCost;
        isUpgraded = true;
        skin.material = upgradeSkinMaterial;
        playerAudio.clip = upgradedProjectileSound;
        GameHUDManager.gameHudManager.GameHudUpdate();

        switch (towerID)
        {
            case 1:
                tigerProjectileSkin.material = upgradeSkinMaterial;
                break;
            case 2:
                towerAttackSpeed *= 0.6f;
                projectileSkin.material = upgradeSkinMaterial;
                break;
            case 3:
                towerDamage = (int)(towerDamage * 1.2f);
                projectileSkin.material = upgradeSkinMaterial;
                break;
        }
    }*/


    /// <summary>
    /// IGT FUNCTIONS
    /// </summary>

    void OnMouseDown()
    {
        Debug.Log("SHOW TOWER STATS");

    }

    public static void DestroyAssignedTowers()
    {
        if (DestroyTowers != null)
        {
            DestroyTowers();
        }
    }

    void DestroyTower()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        DestroyTowers -= DestroyTower;
    }

}
