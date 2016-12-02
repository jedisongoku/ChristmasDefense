using UnityEngine;
using System.Collections;

public class ChristmasTree : MonoBehaviour {

    public AudioSource track;

	void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Enemy"))
        {
            Debug.Log("Life Lost");
            Handheld.Vibrate();
            if ((GameManager.gameManager.level == 3 && GameManager.gameManager.GetCurrentWave() == 15) || 
                (GameManager.gameManager.level == 5 && GameManager.gameManager.GetCurrentWave() == 20) || 
                (GameManager.gameManager.level == 7 && GameManager.gameManager.GetCurrentWave() == 25))
            {
                GameManager.gameManager.LifeLost(5);
            }
            else
            {
                GameManager.gameManager.LifeLost(1);
            }
            track.Play();
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
