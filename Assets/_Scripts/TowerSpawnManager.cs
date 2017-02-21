using UnityEngine;
using System.Collections;

public class TowerSpawnManager : MonoBehaviour {

    public delegate void TowerSpawnAction();
    public static event TowerSpawnAction TowerSpawn;
    public static event TowerSpawnAction TowerSelected;

    public Material towerBaseMaterial;
    public Material towerOutlineMaterial;
    public MeshRenderer skin;
    public GameObject assignedTower;

    private bool isOccupied = false;
    // Use this for initialization


    void Start ()
    {
        TowerSelected += ShowTowers;
        TowerSpawn += HideTowers;
	}

    public void OnTouched()
    {
        Debug.Log("TOUCH BASE");
        /*
        if(GameManager.gameManager.isTowerSelected)
        {
            if (!MouseController.isMouseOnUI)
            {
                if (!isOccupied)
                {
                    GameManager.gameManager.selectedSpawnPoint = gameObject;
                    GameManager.gameManager.SpawnTower();
                    TowerSelected -= ShowTowers;
                    HideAvailableTowers();
                    TowerSpawn -= HideTowers;
                }

            }
        }*/
        
        
    }

    void OnMouseDown()
    {
        Debug.Log("MOUSE DOWN");
        if (GameManager.gameManager.isTowerSelected)
        {
            if (!MouseController.isMouseOnUI)
            {
                if (!isOccupied)
                {
                    GameManager.gameManager.selectedSpawnPoint = gameObject;
                    GameManager.gameManager.SpawnTower();
                    TowerSelected -= ShowTowers;
                    HideAvailableTowers();
                    TowerSpawn -= HideTowers;
                }

            }
        }
    }

    public void ShowTowers()
    {
        if(!isOccupied)
        {
            skin.material = towerOutlineMaterial;
        }
        
    }

    public void HideTowers()
    {
        skin.material = towerBaseMaterial;
    }

    public static void ShowAvailableTowers()
    {
        if(TowerSelected != null)
        {
            TowerSelected();
        }
        
    }

    public static void HideAvailableTowers()
    {
        if (TowerSpawn != null)
        {
            TowerSpawn();
        }
    }

        
        
}
