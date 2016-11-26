using UnityEngine;
using System.Collections;

namespace MagicalFX
{
	public class FX_HitSpawner : MonoBehaviour
	{


		public GameObject FXSpawn;
		public bool DestoyOnHit = false;
		public bool FixRotation = false;
		public float LifeTimeAfterHit = 1;
		public float LifeTime = 0;

        private Vector3 hitLocation;
        private GameObject hitTarget;
	
		void Start ()
		{
		
		}
	
		void Spawn ()
		{
			if (FXSpawn != null) {
				Quaternion rotate = this.transform.rotation;
				if (!FixRotation)
					rotate = FXSpawn.transform.rotation;
                GameObject fx = (GameObject)GameObject.Instantiate(FXSpawn, /*this.transform.position*/ hitTarget.transform.position, rotate);
				if (LifeTime > 0)
					GameObject.Destroy (fx.gameObject, LifeTime);
			}
			if (DestoyOnHit) {
			
				GameObject.Destroy (this.gameObject, LifeTimeAfterHit);
				if (this.gameObject.GetComponent<Collider>())
					this.gameObject.GetComponent<Collider>().enabled = false;

			}
		}
	
		void OnTriggerEnter (Collider other)
		{
            if(other.CompareTag("Enemy"))
            {
                hitLocation = other.transform.position;
                hitTarget = other.gameObject;
                Invoke("Spawn", 0.03f);
            }
            
		}
	
		void OnCollisionEnter (Collision collision)
		{
            if (collision.collider.CompareTag("Enemy"))
            {
                Invoke("Spawn", 0.03f);
            }
        }
	}
}