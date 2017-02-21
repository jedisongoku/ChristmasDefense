using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

    public int towerID;
    public int towerDamage;
    public float towerAttackSpeed;
    public float towerAttackRadius;
    //public int upgradeCost;
    public enum towerAttacks { Acid_Attack, Plasma_Attack, Ice_Attack }; // List of tower attacks
    public towerAttacks towerAttack; // Attacks for the selected tower
    //public bool isUpgraded = false;

    protected List<GameObject> enemiesInRange = new List<GameObject>();

    public GameObject projectile;
    //public GameObject projectileUpgraded;
    public Transform projectileReleaseTransform;
    public Transform radius;
    //public SkinnedMeshRenderer skin;
    //public SkinnedMeshRenderer tigerProjectileSkin;
    //public MeshRenderer projectileSkin;
    //public Material upgradeSkinMaterial;
    public AudioClip ProjectileSound;
    //public AudioClip upgradedProjectileSound;



    void Acid_Attack()
    {
        GameObject particle = Instantiate(projectile, projectileReleaseTransform.position, transform.rotation) as GameObject;
        particle.GetComponent<FX_Mover>().SetTarget(enemiesInRange[0]);
        enemiesInRange[0].GetComponent<Enemy>().TakeDamage(towerDamage, false, gameObject);
    }

    void Plasma_Attack()
    {
        GameObject particle = Instantiate(projectile, projectileReleaseTransform.position, transform.rotation) as GameObject;
        particle.GetComponent<FX_Mover>().SetTarget(enemiesInRange[0]);
        enemiesInRange[0].GetComponent<Enemy>().TakeDamage(towerDamage, false, gameObject);
    }

    void Ice_Attack()
    {

    }
}
