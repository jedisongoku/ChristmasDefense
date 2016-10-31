using UnityEngine;
using System.Collections;

public class ExplosionDamage : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("YOOOO");
            other.GetComponent<Enemy>().SlowEnemy();
        }
    }
}
