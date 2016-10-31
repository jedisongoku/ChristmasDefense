using UnityEngine;
using System.Collections;

public class ChristmasTree : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("Life Lost");
            GameManager.gameManager.LifeLost();
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other != null && other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Success();
        }
        
    }
}
