using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TowerSelect : MonoBehaviour {

    public static Dictionary<int, TowerSelect> towerList = new Dictionary<int, TowerSelect>();
    public static Dictionary<int, Sprite> towerSpriteList = new Dictionary<int, Sprite>();

    [Header("Tower List")]
    public int towerID;
    public Sprite towerImage;
    public Transform disabledImage;
    public Transform purchaseButton;
    public Transform towerSelectedImage;
    public Button towerButton;

    void Awake ()
    {
        if(!towerList.ContainsKey(towerID))
        {
            towerList.Add(towerID, this);
            towerSpriteList.Add(towerID, towerImage);
        }

	    if(Player.unlockedTowers[towerID])
        {
            disabledImage.gameObject.SetActive(false);
            purchaseButton.gameObject.SetActive(false);
            towerButton.interactable = true;
        }
	}

    public void SelectTower()
    {
        towerSelectedImage.gameObject.SetActive(true);
        towerButton.interactable = false;
        GameManager.gameManager.AddTower(towerID);
        Debug.Log("Tower Selected");
    }

    public void DeselectTower()
    {
        towerSelectedImage.gameObject.SetActive(false);
        towerButton.interactable = true;
        Debug.Log("Tower Deselected");
    }
	
}
