using UnityEngine;
using System.Collections;

public class CutSceneController : MonoBehaviour
{

    public void StartLevel()
    {
        GameHUDManager.gameHudManager.menuHUD.gameObject.SetActive(false);
        GameHUDManager.gameHudManager.gameHUD.gameObject.SetActive(true);
        if(GameManager.gameManager.level != 1)
        {
            GameManager.gameManager.StartLevel();
        }
        else
        {
            GameHUDManager.gameHudManager.ShowTutorialSkipPanel();
        }
        
        Camera.main.fieldOfView = 60;
        Camera.main.transform.position = GameManager.gameManager.cameraLocation.position;
        gameObject.SetActive(false);
    }

    public void ShowLevelHUD()
    {
        GameHUDManager.gameHudManager.GoToLevelMenu();
    }

    public void ShowLoadingHUD()
    {
        GameHUDManager.gameHudManager.ShowLoadingHUD();
    }

    public void HideLoadingHUD()
    {
        GameHUDManager.gameHudManager.HideLoadingHUD();
    }
}
