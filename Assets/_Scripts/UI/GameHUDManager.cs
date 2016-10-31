using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameHUDManager : MonoBehaviour
{
    public delegate void SelectLevelAction();
    public static event SelectLevelAction SetAllImages;

    public static GameHUDManager gameHudManager;

    [Header("MenuHUD")]
    public Transform menuHUD;
    public Transform gameHUD;
    public Transform mainMenu;
    public Transform levelMenu;
    public Transform levelPanel;
    public Transform shopPanel;
    public Transform loadingBar;
    public Transform sackOpenPanel;
    public Text sackOpenCounterText;
    public Text sackRewardText;
    public Text boostPointTextMenu;
    public Text lifeTextMenu;
    public Transform sackIndicator;
    public Text sackText;

    [Header("GameHUD")]
    public Transform heroesPanel;
    public Transform levelCompletePanel;
    public Transform pausePanel;
    public Transform buttonsPanel;
    public Transform heroInfoPanel;
    public Text resourceText;
    public Text lifeText;
    public Text waveText;
    public Text boostPointText;
    public Text scoreText;
    public Button tigerSpawnButton;
    public Button frogSpawnButton;
    public Button lizardSpawnButton;
    public Image levelCompleteImage;
    public Image heroInfoPanelImage;
    public Button nextButton;
    public Button UpgradeButton;
    

    //public Vector3 heroesPanelOffSet = new Vector3(-175, 75, 0);

    [Header("Tutorial")]
    public Transform tapHereTooltip;
    public Transform selectHeroTooltip;
    public Transform resourceTooltip;
    public Transform lifeTooltip;
    public Transform upgradeHeroTooltip;

    [Header("In-Game Shop")]
    public Button purchaseSackWithBoostPoint;
    public Button purchaseSackWithDollar;
    public Button purhcaseAddFree;


    [Header("Level")]
    public List<Transform> levels;

    [Header("UI")]
    public Button playPauseButton;
    public Button fastForwardButton;

    [Header("UI Sprite")]
    public Sprite pauseButtonImage;
    public Sprite playButtonImage;
    public Sprite fastForwardImage;
    public Sprite fastForwardCancelImage;
    public Sprite levelFailedImage;
    public Sprite levelComplete_Star1Image;
    public Sprite levelComplete_Star2Image;
    public Sprite levelComplete_Star3Image;
    public Sprite soundOnImage;
    public Sprite soundOffImage;
    public Sprite archerInfoPanelImage;
    public Sprite archerInfoPanelUpgradedImage;
    public Sprite spartanInfoPanelImage;
    public Sprite spartanInfoPanelUpgradedImage;
    public Sprite wizardInfoPanelImage;
    public Sprite wizardInfoPanelUpgradedImage;



    void Start()
    {
        gameHudManager = this;
        menuHUD.gameObject.SetActive(true);
        //GameManager.OnUIAction += SetText;
        //GameManager.OnUIAction += SetHeroAvailability;
    }

    public void GameHudUpdate()
    {
        SetText();
        SetHeroAvailability();
    }

    public void MenuHudUpdate()
    {
        boostPointTextMenu.text = Player.boostPoints.ToString();
        if(Player.sacks > 0)
        {
            sackIndicator.gameObject.SetActive(true);
        }
        else
        {
            sackIndicator.gameObject.SetActive(false);
        }
        lifeTextMenu.text = Player.life.ToString();
        sackText.text = Player.sacks.ToString();
        sackOpenCounterText.text = Player.sacks.ToString();
    }

    void SetText()
    {
        waveText.text = "Wave " + GameManager.gameManager.GetCurrentWave() + " of " + GameManager.gameManager.enemyListForCurrentLevel.Count;
        resourceText.text = Player.resource.ToString();
        lifeText.text = Player.life.ToString();
        
    }

    void SetHeroAvailability()
    {
        if(Player.resource >= GameManager.gameManager.tigerCost)
        {
            tigerSpawnButton.interactable = true;
        }
        else
        {
            tigerSpawnButton.interactable = false;
        }
        if (Player.resource >= GameManager.gameManager.frogCost)
        {
            frogSpawnButton.interactable = true;
        }
        else
        {
            frogSpawnButton.interactable = false;
        }
        if (Player.resource >= GameManager.gameManager.lizardCost)
        {
            lizardSpawnButton.interactable = true;
        }
        else
        {
            lizardSpawnButton.interactable = false;
        }
    }



    public void ShowHeroInfo(int id)
    {
        if(GameManager.gameManager.tutorialPhase_3)
        {
            upgradeHeroTooltip.gameObject.SetActive(true);
        }
        
        if (GameManager.gameManager.selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero.GetComponent<Hero>().isUpgraded)
        {
            UpgradeButton.gameObject.SetActive(false);
            switch (id)
            {
                case 1:
                    heroInfoPanelImage.sprite = archerInfoPanelUpgradedImage;
                    
                    break;
                case 2:
                    heroInfoPanelImage.sprite = spartanInfoPanelUpgradedImage;
                    break;
                case 3:
                    heroInfoPanelImage.sprite = wizardInfoPanelUpgradedImage;
                    break;
            }
        }
        else
        {
            UpgradeButton.gameObject.SetActive(true);
            UpgradeButton.interactable = false;
            switch (id)
            {
                case 1:
                    heroInfoPanelImage.sprite = archerInfoPanelImage;
                    if(Player.resource >= GameManager.gameManager.tigerUpgradeCost)
                    {
                        UpgradeButton.interactable = true;
                    }
                    break;
                case 2:
                    heroInfoPanelImage.sprite = spartanInfoPanelImage;
                    if (Player.resource >= GameManager.gameManager.frogUpgradeCost)
                    {
                        UpgradeButton.interactable = true;
                    }
                    break;
                case 3:
                    heroInfoPanelImage.sprite = wizardInfoPanelImage;
                    if (Player.resource >= GameManager.gameManager.lizardUpgradeCost)
                    {
                        UpgradeButton.interactable = true;
                    }
                    break;
            }
        }

        heroesPanel.gameObject.SetActive(false);
        heroInfoPanel.gameObject.SetActive(true);

    }

    public void HideHeroInfo()
    {
        if (GameManager.gameManager.tutorialPhase_3 && !GameManager.gameManager.tutorialPhase_2)
        {
            //GameManager.gameManager.tutorialHero.GetComponent<Hero>().ShowTutorialTooltip();
        }
        Invoke("HideHeroInfoDelayed", 0.1f);
        
    }

    void HideHeroInfoDelayed()
    {
        heroInfoPanel.gameObject.SetActive(false);
    }

    public void ShowHeroes()
    {
        HideHeroInfo();
        heroesPanel.gameObject.SetActive(true);
        //GameManager.gameManager.selectedSpawnPoint.GetComponent<HeroSpawnManager>().particleOnClick.gameObject.SetActive(true);
        GameHudUpdate();   
    }

    public void HideHeroes()
    {
        if (GameManager.gameManager.tutorialPhase_3)
        {
            upgradeHeroTooltip.gameObject.SetActive(true);
        }
        Invoke("HideHeroesDelayed", 0.1f);
    }

    void HideHeroesDelayed()
    {
        if (GameManager.gameManager.selectedSpawnPoint != null)
        {
            GameManager.gameManager.selectedSpawnPoint.GetComponent<HeroSpawnManager>().HideObjects();
        }

        heroesPanel.gameObject.SetActive(false);
    }

    public void GoToLevelMenu()
    {
        mainMenu.gameObject.SetActive(false);
        levelMenu.gameObject.SetActive(true);
        levelPanel.gameObject.SetActive(true);
        SetAllImages();
        MenuHudUpdate();
        //ShowAd(null);

        
    }

    public void ShowAd(string zone)
    {
        UnityAds.ads.ShowAd(zone);
    }

    public void SelectLevel(int level)
    {
        loadingBar.gameObject.SetActive(true);
        levelMenu.gameObject.SetActive(false);
        
        
        switch (level)
        {
            case 1:
                GameManager.gameManager.level = level;
                GameManager.gameManager.GetComponent<Level1>().enabled = true;
                break;
            case 2:
                GameManager.gameManager.level = level;
                GameManager.gameManager.GetComponent<Level2>().enabled = true;
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
                

        }

        
        levels[level - 1].gameObject.SetActive(true);


        //gameHUD.gameObject.SetActive(true);
        //GameManager.gameManager.StartLevel();

    }

    public void NextLevel()
    {
        HeroSpawnManager.DestroyAssignedHeroes();
        levelCompletePanel.gameObject.SetActive(false);
        levels[GameManager.gameManager.level - 1].gameObject.SetActive(false);
        menuHUD.gameObject.SetActive(true);
        SelectLevel(GameManager.gameManager.level + 1);
    }

    public void RestartLevel()
    {
        pausePanel.gameObject.SetActive(false);
        buttonsPanel.gameObject.SetActive(true);

        HeroSpawnManager.DestroyAssignedHeroes();
        Enemy.DestroyAllEnemies();
        levelCompletePanel.gameObject.SetActive(false);
        GameManager.gameManager.OnRestartLevel();
        GameManager.gameManager.isFastForward = false;
        fastForwardButton.image.sprite = fastForwardImage;
        playPauseButton.image.sprite = pauseButtonImage;
        Time.timeScale = 1;
        gameHUD.gameObject.SetActive(false);
        GameManager.gameManager.introCamera.transform.gameObject.SetActive(true);
        GameManager.gameManager.introCamera.GetComponent<Animator>().SetTrigger("Intro" + GameManager.gameManager.level);
        //GameManager.gameManager.StartLevel();

    }

    public void GoHome()
    {
        GameManager.gameManager.menuCamera.enabled = true;
        //GameManager.gameManager.background.gameObject.SetActive(true);
        HeroSpawnManager.DestroyAssignedHeroes();
        Enemy.DestroyAllEnemies();
        levelCompletePanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        buttonsPanel.gameObject.SetActive(true);
        GameManager.gameManager.OnGoHome();
        levels[GameManager.gameManager.level - 1].gameObject.SetActive(false);
        gameHUD.gameObject.SetActive(false);
        menuHUD.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);
        levelMenu.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    public void Resume()
    {
        pausePanel.gameObject.SetActive(false);
        buttonsPanel.gameObject.SetActive(true);
        playPauseButton.image.sprite = pauseButtonImage;
        fastForwardButton.image.sprite = fastForwardImage;
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        if(!GameManager.gameManager.isGamePaused)
        {
            Time.timeScale = 0;
            GameManager.gameManager.isGamePaused = true;
            playPauseButton.image.sprite = playButtonImage;
        }
        else
        {
            Time.timeScale = 1;
            GameManager.gameManager.isGamePaused = false;
            playPauseButton.image.sprite = pauseButtonImage;
        }
            
    }

    public void PauseMenu()
    {
        pausePanel.gameObject.SetActive(true);
        buttonsPanel.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void FastForward()
    {
        if (!GameManager.gameManager.isFastForward)
        {
            Time.timeScale = 2;
            GameManager.gameManager.isFastForward = true;
            fastForwardButton.image.sprite = fastForwardCancelImage;
        }
        else
        {
            Time.timeScale = 1;
            GameManager.gameManager.isFastForward = false;
            fastForwardButton.image.sprite = fastForwardImage;
        }
    }

    public void LevelComplete()
    {

        nextButton.interactable = true;
        switch (GameManager.gameManager.levelCompletedStars)
        {
            case 0:
                levelCompleteImage.sprite = levelFailedImage;
                nextButton.interactable = false;
                break;
            case 1:
                levelCompleteImage.sprite = levelComplete_Star1Image;
                break;
            case 2:
                levelCompleteImage.sprite = levelComplete_Star2Image;
                break;
            case 3:
                levelCompleteImage.sprite = levelComplete_Star3Image;
                break;

        }
        levelCompletePanel.gameObject.SetActive(true);
        levelCompletePanel.GetComponent<Animator>().SetTrigger("LevelComplete");

        if (GameManager.gameManager.levelCompletedStars != 0)
        {
            if(Player.levelScores[GameManager.gameManager.level] < Player.score)
            {
                Player.levelScores[GameManager.gameManager.level] = Player.score;
            }
            DataStore.Save();
            boostPointText.text = Player.resource.ToString();
            scoreText.text = Player.score.ToString();
            boostPointText.enabled = true;
        }
        else
        {
            boostPointText.enabled = false;
            scoreText.enabled = false;
            Player.life = GameManager.gameManager.levelInitialLife;
        }
        
        
    }

    public void ShowShop()
    {
        levelPanel.gameObject.SetActive(false);
        sackOpenPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(true);
    }

    public void HideShop()
    {
        levelPanel.gameObject.SetActive(true);
        shopPanel.gameObject.SetActive(false);
    }

    public void UpgradeHero()
    {
        GameManager.gameManager.selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero.GetComponent<Hero>().Upgrade();
        GameManager.gameManager.selectedSpawnPoint.GetComponent<HeroSpawnManager>().HideObjects();
        GameManager.gameManager.selectedSpawnPoint.GetComponentInChildren<ParticleSystem>().Play();
        heroInfoPanel.gameObject.SetActive(false);
        TutorialPhaseComplete(3);
        MouseController.isMouseOnUI = false;
    }

    public void PurchaseSackWithBoostPoints()
    {
        if(Player.boostPoints >= 500)
        {
            Player.boostPoints -= 500;
            Player.sacks++;
            DataStore.Save();
            MenuHudUpdate();
        }
    }

    public void ShowSackOpenPanel()
    {
        sackOpenPanel.gameObject.SetActive(true);
        levelPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(false);
        sackOpenCounterText.text = Player.sacks.ToString();
    }

    public void HideSackOpenPanel()
    {
        sackOpenPanel.gameObject.SetActive(false);
        levelPanel.gameObject.SetActive(true);
    }

    public void OpenSack()
    {
        if(Player.sacks > 0)
        {
            Player.sacks--;
            int roll = Random.Range(1, 1000);
            if(roll < 250)
            {
                //Boost 100
                sackRewardText.text = "Boost 100";
                Player.boostPoints += 100;
            }
            else if(roll < 400)
            {
                //Boost 250
                sackRewardText.text = "Boost 250";
                Player.boostPoints += 250;
            }
            else if(roll < 450)
            {
                //Boost 500
                sackRewardText.text = "Boost 500";
                Player.boostPoints += 500;
            }
            else if (roll < 750)
            {
                //Life
                sackRewardText.text = "Gift Box";
                Player.life++;
            }
            else if (roll <= 1000)
            {
                //Hero
                sackRewardText.text = "HERO";
            }
        }
        MenuHudUpdate();
        DataStore.Save();
    }

    public void TutorialPhaseComplete(int phase)
    {
        switch(phase)
        {
            case 1:
                if (GameManager.gameManager.tutorialPhase_1)
                {
                    Debug.Log("phase 1");
                    GameManager.gameManager.tutorialPhase_1 = false;
                    tapHereTooltip.gameObject.SetActive(false);
                    resourceTooltip.gameObject.SetActive(true);
                    lifeTooltip.gameObject.SetActive(true);
                    selectHeroTooltip.gameObject.SetActive(true);
                    GameManager.gameManager.tutorialPhase_2 = true;

                }
                break;
            case 2:
                if (GameManager.gameManager.tutorialPhase_2 && !GameManager.gameManager.tutorialPhase_1)
                {
                    Debug.Log("phase 2");
                    GameManager.gameManager.ResetWaveTimer();
                    GameManager.gameManager.isTutorial = false;
                    GameManager.gameManager.tutorialPhase_2 = false;
                    selectHeroTooltip.gameObject.SetActive(false);
                    resourceTooltip.gameObject.SetActive(false);
                    lifeTooltip.gameObject.SetActive(false);
                    GameManager.gameManager.tutorialPhase_3 = true;

                }
                
                break;
            case 3:
                if (GameManager.gameManager.tutorialPhase_3 && !GameManager.gameManager.tutorialPhase_2)
                {
                    Debug.Log("phase 3");
                    GameManager.gameManager.tutorialPhase_3 = false;
                    upgradeHeroTooltip.gameObject.SetActive(false);

                }
                break;
        }
    }
}
