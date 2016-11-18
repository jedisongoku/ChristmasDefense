using UnityEngine;
using System.Collections;

public class ExplosionDamage : MonoBehaviour {


    void Awake()
    {
        transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
    }
	void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Enemy"))
        {
            //Debug.Log("YOOOO");
            other.GetComponent<Enemy>().SlowEnemy();
            other.GetComponent<Enemy>().TakeDamage(0, false, null); //just to activate hornet's dodge
        }
    }
}
