using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Loading : MonoBehaviour
{
    public int loadingTime;
    public Image loadingBar;
    private float loadingTimer;
    

    void OnEnable()
    {
        loadingTimer = 0;
        StartCoroutine(Load());

    }

    IEnumerator Load()
    {
        loadingTimer += Time.deltaTime;
        loadingBar.fillAmount = loadingTimer / (float)loadingTime;

        yield return new WaitForSeconds(0);

        if(loadingTimer < loadingTime)
        {
            StartCoroutine(Load());
        }
        else
        {
            GameManager.gameManager.menuCamera.enabled = false;
            //Camera.main.transform.position = GameManager.gameManager.cameraLocations[GameManager.gameManager.level - 1].position;
            GameManager.gameManager.introCamera.transform.gameObject.SetActive(true);
            GameManager.gameManager.introCamera.GetComponent<Animator>().SetTrigger("Intro" + GameManager.gameManager.level);
            gameObject.SetActive(false);
            
            //gameObject.transform.parent.gameObject.SetActive(false);
            /*
            GameHUDManager.gameHudManager.menuHUD.gameObject.SetActive(false);
            GameHUDManager.gameHudManager.gameHUD.gameObject.SetActive(true);
            GameManager.gameManager.StartLevel();*/
        }
    }

}
