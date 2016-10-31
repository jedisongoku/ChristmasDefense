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
        Camera.main.transform.position = GameManager.gameManager.cameraLocations[GameManager.gameManager.level - 1].position;
        gameObject.SetActive(false);
    }
}
