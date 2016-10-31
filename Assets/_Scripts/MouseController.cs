using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

    public static bool isMouseOnUI = false;

    public float smoothing;
    public int cameraOffSet;
    public int zoomSpeed;
    public Transform[] CameraMoveLimitTop;
    public Transform[] CameraMoveLimitBottom;

    private Vector3 initialMousePosition = Vector3.zero;
    //private Vector3 heroesPanelOffSet = new Vector3(-100, 50, 0);
    private RaycastHit hit;
    private int level;

	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    if (Input.GetAxis("Mouse ScrollWheel") > 0 && GetComponent<Camera>().fieldOfView > 30)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, GetComponent<Camera>().fieldOfView - zoomSpeed, Time.deltaTime * smoothing);
            Debug.Log("ZOOM IN");
            /*
            if (GameManager.gameManager.selectedSpawnPoint != null)
            {
                GameHUDManager.gameHudManager.heroesPanel.transform.position = Camera.main.WorldToScreenPoint(GameManager.gameManager.selectedSpawnPoint.transform.position) + heroesPanelOffSet;
                if (GameHUDManager.gameHudManager.heroesPanel.transform.position.x + GameHUDManager.gameHudManager.heroesPanel.GetComponent<RectTransform>().rect.width > Camera.main.pixelWidth)
                {
                    GameHUDManager.gameHudManager.heroesPanel.transform.position = new Vector3(GameHUDManager.gameHudManager.heroesPanel.transform.position.x - ((GameHUDManager.gameHudManager.heroesPanel.transform.position.x + 
                        GameHUDManager.gameHudManager.heroesPanel.GetComponent<RectTransform>().rect.width) - Camera.main.pixelWidth), GameHUDManager.gameHudManager.heroesPanel.transform.position.y, GameHUDManager.gameHudManager.heroesPanel.transform.position.z);
                }
                else if (GameHUDManager.gameHudManager.heroesPanel.transform.position.x < 0)
                {
                    GameHUDManager.gameHudManager.heroesPanel.transform.position = new Vector3(0, GameHUDManager.gameHudManager.heroesPanel.transform.position.y, GameHUDManager.gameHudManager.heroesPanel.transform.position.z);
                }

                if (GameHUDManager.gameHudManager.heroesPanel.transform.position.y - GameHUDManager.gameHudManager.heroesPanel.GetComponent<RectTransform>().rect.height < 0)
                {
                    GameHUDManager.gameHudManager.heroesPanel.transform.position = new Vector3(GameHUDManager.gameHudManager.heroesPanel.transform.position.x, GameHUDManager.gameHudManager.heroesPanel.transform.position.y + (GameHUDManager.gameHudManager.heroesPanel.GetComponent<RectTransform>().rect.height - GameHUDManager.gameHudManager.heroesPanel.transform.position.y), GameHUDManager.gameHudManager.heroesPanel.transform.position.z);
                }
                else if (GameHUDManager.gameHudManager.heroesPanel.transform.position.y > Camera.main.pixelHeight)
                {

                    GameHUDManager.gameHudManager.heroesPanel.transform.position = new Vector3(GameHUDManager.gameHudManager.heroesPanel.transform.position.x, Camera.main.pixelHeight, GameHUDManager.gameHudManager.heroesPanel.transform.position.z);
                }
            }*/
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && GetComponent<Camera>().fieldOfView < 90)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, GetComponent<Camera>().fieldOfView + zoomSpeed, Time.deltaTime * smoothing);
            Debug.Log("ZOOM OUT");

            /*
            if (GameManager.gameManager.selectedSpawnPoint != null)
            {
                GameHUDManager.gameHudManager.heroesPanel.transform.position = Camera.main.WorldToScreenPoint(GameManager.gameManager.selectedSpawnPoint.transform.position) + heroesPanelOffSet;
                if (GameHUDManager.gameHudManager.heroesPanel.transform.position.x + GameHUDManager.gameHudManager.heroesPanel.GetComponent<RectTransform>().rect.width > Camera.main.pixelWidth)
                {
                    GameHUDManager.gameHudManager.heroesPanel.transform.position = new Vector3(GameHUDManager.gameHudManager.heroesPanel.transform.position.x - ((GameHUDManager.gameHudManager.heroesPanel.transform.position.x + 
                        GameHUDManager.gameHudManager.heroesPanel.GetComponent<RectTransform>().rect.width) - Camera.main.pixelWidth), GameHUDManager.gameHudManager.heroesPanel.transform.position.y, GameHUDManager.gameHudManager.heroesPanel.transform.position.z);
                }
                else if (GameHUDManager.gameHudManager.heroesPanel.transform.position.x < 0)
                {
                    GameHUDManager.gameHudManager.heroesPanel.transform.position = new Vector3(0, GameHUDManager.gameHudManager.heroesPanel.transform.position.y, GameHUDManager.gameHudManager.heroesPanel.transform.position.z);
                }

                if (GameHUDManager.gameHudManager.heroesPanel.transform.position.y - GameHUDManager.gameHudManager.heroesPanel.GetComponent<RectTransform>().rect.height < 0)
                {
                    GameHUDManager.gameHudManager.heroesPanel.transform.position = new Vector3(GameHUDManager.gameHudManager.heroesPanel.transform.position.x, GameHUDManager.gameHudManager.heroesPanel.transform.position.y + (GameHUDManager.gameHudManager.heroesPanel.GetComponent<RectTransform>().rect.height - GameHUDManager.gameHudManager.heroesPanel.transform.position.y), GameHUDManager.gameHudManager.heroesPanel.transform.position.z);
                }
                else if (GameHUDManager.gameHudManager.heroesPanel.transform.position.y > Camera.main.pixelHeight)
                {

                    GameHUDManager.gameHudManager.heroesPanel.transform.position = new Vector3(GameHUDManager.gameHudManager.heroesPanel.transform.position.x, Camera.main.pixelHeight, GameHUDManager.gameHudManager.heroesPanel.transform.position.z);
                }
            }*/
        }

        if(Input.GetMouseButtonDown(0))
        {
            initialMousePosition = Input.mousePosition;
            

                if (!isMouseOnUI)
                {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 1000))
                {
                    Debug.Log(hit.transform.name);
                    if (!hit.transform.CompareTag("SpawnLocation"))
                    {
                        Invoke("HideHeroPanels", 0);
                        //HideHeroPanels();
                        GameHUDManager.gameHudManager.TutorialPhaseComplete(2);
                        
                    }
                }
            }



        }
        
        if (Input.GetMouseButton(0) && initialMousePosition != Vector3.zero)
        {
            if(initialMousePosition.x > Input.mousePosition.x + cameraOffSet && transform.position.x < CameraMoveLimitTop[GameManager.gameManager.level - 1].position.x)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(cameraOffSet, 0, 0), Time.deltaTime * smoothing);
            }
            if (initialMousePosition.x < Input.mousePosition.x - cameraOffSet && transform.position.x > CameraMoveLimitBottom[GameManager.gameManager.level - 1].position.x)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(cameraOffSet, 0, 0), Time.deltaTime * smoothing);
            }
            if (initialMousePosition.y > Input.mousePosition.y + cameraOffSet && transform.position.z < CameraMoveLimitTop[GameManager.gameManager.level - 1].position.z)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0, cameraOffSet), Time.deltaTime * smoothing);
            }
            if (initialMousePosition.y < Input.mousePosition.y - cameraOffSet && transform.position.z > CameraMoveLimitBottom[GameManager.gameManager.level - 1].position.z)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(0, 0, cameraOffSet), Time.deltaTime * smoothing);
            }
            /*
            if(GameManager.gameManager.selectedSpawnPoint != null)
            {
                GameHUDManager.gameHudManager.heroesPanel.transform.position = Camera.main.WorldToScreenPoint(GameManager.gameManager.selectedSpawnPoint.transform.position) + heroesPanelOffSet;
                if (GameHUDManager.gameHudManager.heroesPanel.transform.position.x + GameHUDManager.gameHudManager.heroesPanel.GetComponent<RectTransform>().rect.width > Camera.main.pixelWidth)
                {
                    GameHUDManager.gameHudManager.heroesPanel.transform.position = new Vector3(GameHUDManager.gameHudManager.heroesPanel.transform.position.x - ((GameHUDManager.gameHudManager.heroesPanel.transform.position.x + 
                        GameHUDManager.gameHudManager.heroesPanel.GetComponent<RectTransform>().rect.width) - Camera.main.pixelWidth), GameHUDManager.gameHudManager.heroesPanel.transform.position.y, GameHUDManager.gameHudManager.heroesPanel.transform.position.z);
                }
                else if(GameHUDManager.gameHudManager.heroesPanel.transform.position.x < 0)
                {
                    GameHUDManager.gameHudManager.heroesPanel.transform.position = new Vector3(0, GameHUDManager.gameHudManager.heroesPanel.transform.position.y, GameHUDManager.gameHudManager.heroesPanel.transform.position.z);
                }

                if (GameHUDManager.gameHudManager.heroesPanel.transform.position.y - GameHUDManager.gameHudManager.heroesPanel.GetComponent<RectTransform>().rect.height < 0)
                {
                    GameHUDManager.gameHudManager.heroesPanel.transform.position = new Vector3(GameHUDManager.gameHudManager.heroesPanel.transform.position.x, GameHUDManager.gameHudManager.heroesPanel.transform.position.y + (GameHUDManager.gameHudManager.heroesPanel.GetComponent<RectTransform>().rect.height - GameHUDManager.gameHudManager.heroesPanel.transform.position.y), GameHUDManager.gameHudManager.heroesPanel.transform.position.z);
                }
                else if (GameHUDManager.gameHudManager.heroesPanel.transform.position.y > Camera.main.pixelHeight)
                {

                    GameHUDManager.gameHudManager.heroesPanel.transform.position = new Vector3(GameHUDManager.gameHudManager.heroesPanel.transform.position.x, Camera.main.pixelHeight, GameHUDManager.gameHudManager.heroesPanel.transform.position.z);
                }
            }*/
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            initialMousePosition = Vector3.zero;
        }


    }

    void HideHeroPanels()
    {
        GameHUDManager.gameHudManager.HideHeroes();
        GameHUDManager.gameHudManager.HideHeroInfo();
    }
}
