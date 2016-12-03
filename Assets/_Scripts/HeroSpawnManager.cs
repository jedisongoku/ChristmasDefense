using UnityEngine;
using System.Collections;

public class HeroSpawnManager : MonoBehaviour
{
    public delegate void HeroSpawnAction();
    public static event HeroSpawnAction HeroSpawn; // All spawn managers subscribed to this
    public static event HeroSpawnAction DestroyHeroes;

    public GameObject assignedHero;
    public Transform particleOnClick;
    public Transform particleAfterSpawn;
    public Transform radius;
    public Transform tapHeroTooltip;

    private static GameObject previousSpawnLocation;

    void Start()
    {
        HeroSpawn += HideObjects;
        DestroyHeroes += DestroyHero;
    }

    void OnDestroy()
    {
        HeroSpawn -= HideObjects;
    }
    
    public void OnTouched()
    {
        if(GameManager.gameManager.tutorialPhase_2)
        {
            GameHUDManager.gameHudManager.TutorialPhaseStart(2);
        }

        
        if (!MouseController.isMouseOnUI)
        {

            GameManager.gameManager.selectedSpawnPoint = gameObject;
            //Invoke("SetSpawnPoint", 0);

            if (HeroSpawn != null)
            {
                HeroSpawn();
            }

            if (assignedHero == null)
            {
                GameHUDManager.gameHudManager.ShowHeroes();
                particleOnClick.gameObject.SetActive(true);

            }
            else
            {

                Debug.Log("Show player updates here" + GameManager.gameManager.tutorialPhase_4);
                GameHUDManager.gameHudManager.ShowHeroInfo(assignedHero.GetComponent<Hero>().heroID);
                radius.gameObject.SetActive(true);
                if (GameManager.gameManager.tutorialPhase_4)
                {
                    GameHUDManager.gameHudManager.TutorialPhaseStart(4);
                }
                if(tapHeroTooltip.gameObject.activeInHierarchy)
                {
                    tapHeroTooltip.gameObject.SetActive(false);
                }
            }
            
            
        }
        //GameHUDManager.gameHudManager.HideInfoPanel();


    }

    void SetSpawnPoint()
    {
        GameManager.gameManager.selectedSpawnPoint = gameObject;
    }

    void HideHeroPanels()
    {
        GameHUDManager.gameHudManager.HideHeroes();
    }

    public void HideObjects()
    {

        particleOnClick.gameObject.SetActive(false);
        //particleAfterSpawn.gameObject.SetActive(false);
        if(radius !=null)
        {
            radius.gameObject.SetActive(false);
        }
        
    }

    public static void DestroyAssignedHeroes()
    {
        if(DestroyHeroes != null)
        {
            DestroyHeroes();
        }
        
    }

    public void DestroyHero()
    {
        Destroy(assignedHero);

    }

    public void ShowTutorialTooltip()
    {
        tapHeroTooltip.parent = GameManager.gameManager.selectedSpawnPoint.transform;
        tapHeroTooltip.gameObject.SetActive(true);
    }

    public void HideTutorialTooltip()
    {

        tapHeroTooltip.gameObject.SetActive(false);
    }

}
