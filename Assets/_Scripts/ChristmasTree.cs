using UnityEngine;
using System.Collections;

public class ChristmasTree : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Enemy"))
        {
            Debug.Log("Life Lost");
            if (GameManager.gameManager.level == 3 && GameManager.gameManager.GetCurrentWave() == 15)
            {
                GameManager.gameManager.LifeLost(5);
            }
            else
            {
                GameManager.gameManager.LifeLost(1);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other != null && other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Success();
        }

    }
}
