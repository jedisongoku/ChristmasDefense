using UnityEngine;
using System.Collections;

public class Farm : MonoBehaviour
{
    void OnTriggerEnter(Collider enemy)
    {
        Debug.Log("FARM TRIGGERED BY" + enemy.gameObject);
        if (enemy.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("IN FARM");
            enemy.gameObject.SetActive(false);
        }
    }
	

}
