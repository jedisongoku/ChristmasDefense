using UnityEngine;
using System.Collections;

public class CutSceneController : MonoBehaviour
{

    public void StartLevel()
    {
        GameHUDManager.gameHudManager.menuHUD.gameObject.SetActive(false);
        GameHUDManager.gameHudManager.gameHUD.gameObject.SetActive(true);

        GameManager.gameManager.StartLevel();
        Camera.main.fieldOfView = 60;
        Camera.main.transform.position = GameManager.gameManager.cameraLocation.position;
        gameObject.SetActive(false);
    }

    public void ShowLevelHUD()
    {
        GameHUDManager.gameHudManager.GoToLevelMenu();
    }
}
