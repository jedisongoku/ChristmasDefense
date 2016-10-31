using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour {

	void OnTriggerEnter(Collider hit)
    {
        if(hit.CompareTag("Enemy"))
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
