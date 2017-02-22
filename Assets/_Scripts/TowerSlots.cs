using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerSlots : MonoBehaviour
{
    public static TowerSlots towerSlot;

    [Header("SelectedTowers")]
    public Button[] slotButtons;
    public Toggle[] spawnTowerToggles;
    public Sprite baseButtonImage;

    void Awake()
    {
        towerSlot = this;
        
    }

    public void UpdateSlots()
    {/*
        foreach(var button in slotButtons)
        {
            button.interactable = false;
            button.image.sprite = baseButtonImage;
        }*/
        
        for(int i = 0; i < slotButtons.Length; i++)
        {
            if(GameManager.selectedTowers.Count > i)
            {
                Debug.Log(slotButtons[i]);
                slotButtons[i].interactable = true;

                slotButtons[i].image.sprite = TowerSelect.towerSpriteList[GameManager.selectedTowers[i]];
                Debug.Log(GameManager.selectedTowers.Count);
            }
            else
            {
                slotButtons[i].interactable = false;
                slotButtons[i].image.sprite = baseButtonImage;
            }
            
        }
    }

    public void UpdateTowerToggles()
    {
        Debug.Log(GameManager.selectedTowers.Count);
        Debug.Log(spawnTowerToggles.Length);
        for (int i = 0; i < spawnTowerToggles.Length; i++)
        {
            if (GameManager.selectedTowers.Count > i)
            {
                Debug.Log("TOGGLES " + i);
                spawnTowerToggles[i].interactable = true;

                spawnTowerToggles[i].image.sprite = TowerSelect.towerSpriteList[GameManager.selectedTowers[i]];
                Debug.Log(GameManager.selectedTowers.Count);
            }
            else
            {
                spawnTowerToggles[i].interactable = false;
                spawnTowerToggles[i].image.sprite = baseButtonImage;
            }

        }
    }

    public void RemoveTower(int SlotID)
    {
        GameManager.gameManager.RemoveTower(SlotID);
        //slotButtons[SlotID].interactable = false;
        //slotButtons[SlotID].image.sprite = baseButtonImage;
        
    }
}
