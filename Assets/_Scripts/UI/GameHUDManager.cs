using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameHUDManager : MonoBehaviour
{
    public delegate void SelectLevelAction();
    public static event SelectLevelAction SetAllImages;

    public static GameHUDManager gameHudManager;
	public Text fps;

    [Header("MenuHUD")]
    public Transform menuHUD;
    public Transform gameHUD;
    public Transform loadingHUD;
    public Transform mainMenu;
    public Transform levelMenu;
    public Transform levelPanel;
    public Transform shopPanel;
    public Transform loadingBar;
    public Transform MiniGamePanel;
    public Text MiniGameCounterText;
    public Text boostPointTextMenu;
    public Text lifeTextMenu;
    public Transform snowFlakeIndicator;
    public Text snowFlakeText;
    public Transform specialHeroIndicatorMenu;
    public Text specialHeroIndicatorTextMenu;

    [Header("GameHUD")]
    public Transform heroesPanel;
    public Transform levelCompletePanel;
    public Transform pausePanel;
    public Transform buttonsPanel;
    public Transform heroInfoPanel;
    public Transform specialHeroIndicator;
    public Text resourceText;
    public Text lifeText;
    public Text waveText;
    public Text boostPointText;
    public Text scoreText;
    public Text specialHeroIndicatorText;
    public Button tigerSpawnButton;
    public Button frogSpawnButton;
    public Button lizardSpawnButton;
    public Button specialHeroSpawnButton;
    public Image levelCompleteImage;
    public Image heroInfoPanelImage;
    public Button nextButton;
    public Button UpgradeButton;
    public int heroInfoId;


    //public Vector3 heroesPanelOffSet = new Vector3(-175, 75, 0);

    [Header("Tutorial")]
    public Transform tapHereTooltip;
    public Transform selectHeroTooltip;
    public Transform resourceTooltip;
    public Transform lifeTooltip;
    public Transform upgradeHeroTooltip;

    [Header("In-Game Shop")]
    public Transform miniGameItemPanel;
    public Transform heroesItemPanel;
    public Transform adsItemPanel;
    public Button miniGameTabButton;
    public Button heroesTabButton;
    public Button adsTabButton;
    public Button purchaseSnowFlake_500BP;
    public Button purchaseSnowFlake_99C;
    public Button purchaseSnowFlake_299C;
    public Button purchaseWarrior_99C;
    public Button purchaseWarrior_499C;
    public Button purhcaseAddFree_99C;


    [Header("Level")]
    public List<Transform> levels;
    public List<GameObject> levelPrefabs;

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

        SetSpecialHeroIndicator();
		StartCoroutine (Fps ());
        //GameManager.OnUIAction += SetText;
        //GameManager.OnUIAction += SetHeroAvailability;
    }

    public void GameHudUpdate()
    {
        SetText();
        SetHeroAvailability();
        if (heroInfoPanel.gameObject.activeInHierarchy)
        {
            setUpgradeAvailability();
        }
    }

    public void MenuHudUpdate()
    {
        boostPointTextMenu.text = Player.boostPoints.ToString();
        if (Player.snowFlakes > 0)
        {
            snowFlakeIndicator.gameObject.SetActive(true);
        }
        else
        {
            snowFlakeIndicator.gameObject.SetActive(false);
        }
        if (Player.specialHero > 0)
        {
            specialHeroIndicatorMenu.gameObject.SetActive(true);
        }
        else
        {
            specialHeroIndicatorMenu.gameObject.SetActive(false);
        }
        lifeTextMenu.text = Player.life.ToString();
        snowFlakeText.text = Player.snowFlakes.ToString();
        specialHeroIndicatorTextMenu.text = Player.specialHero.ToString();
        MiniGameCounterText.text = Player.snowFlakes.ToString();
    }

    void SetText()
    {
        waveText.text = "Wave " + GameManager.gameManager.GetCurrentWave() + " of " + GameManager.gameManager.enemyListForCurrentLevel.Count;
        resourceText.text = Player.resource.ToString();
        lifeText.text = Player.life.ToString();

    }

    void SetHeroAvailability()
    {
        if (Player.resource >= GameManager.gameManager.tigerCost)
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
        heroInfoId = id;
        if (GameManager.gameManager.tutorialPhase_3)
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
            setUpgradeAvailability();
        }

        heroesPanel.gameObject.SetActive(false);
        heroInfoPanel.gameObject.SetActive(true);

    }

    void setUpgradeAvailability()
    {
        UpgradeButton.gameObject.SetActive(true);
        UpgradeButton.interactable = false;
        switch (heroInfoId)
        {
            case 1:
                heroInfoPanelImage.sprite = archerInfoPanelImage;
                if (Player.resource >= GameManager.gameManager.tigerUpgradeCost)
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
        PlayLoadLevelIntro();

        switch (level)
        {
            case 1:

                break;
            case 2:

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


        GameManager.gameManager.level = level;
        levels[level - 1].gameObject.SetActive(true);

        //gameHUD.gameObject.SetActive(true);
        //GameManager.gameManager.StartLevel();

    }

    IEnumerator DelayedStartLevel(int level)
    {

        yield return new WaitForSeconds(0.1f);


    }

    public void NextLevel()
    {
        loadingHUD.gameObject.SetActive(true);
        HeroSpawnManager.DestroyAssignedHeroes();
        SpecialHero.DestorySpecialHeroes();
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
        SpecialHero.DestorySpecialHeroes();
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
        SpecialHero.DestorySpecialHeroes();
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
        if (!GameManager.gameManager.isGamePaused)
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
            if (Player.levelScores[GameManager.gameManager.level] < Player.score)
            {
                Player.levelScores[GameManager.gameManager.level] = Player.score;
            }
            if (Player.completedLevels[GameManager.gameManager.level] < GameManager.gameManager.levelCompletedStars)
            {
                Player.completedLevels[GameManager.gameManager.level] = GameManager.gameManager.levelCompletedStars;
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
            Player.specialHero = GameManager.gameManager.levelInitialSpecialHero;
        }


    }

    public void ShowShop()
    {
        levelPanel.gameObject.SetActive(false);
        MiniGamePanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(true);
        ItemShopTabClicked(0);
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

    public void PurchaseSnowFlakesWithBoostPoints()
    {
        if (Player.boostPoints >= 500)
        {
            Player.boostPoints -= 500;
            Player.snowFlakes++;
            DataStore.Save();
            MenuHudUpdate();
        }
    }

    public void ShowLoadingHUD()
    {
        loadingHUD.gameObject.SetActive(true);
    }

    public void HideLoadingHUD()
    {
        loadingHUD.gameObject.SetActive(false);
    }

    public void ShowMiniGamePanel()
    {
        MiniGamePanel.gameObject.SetActive(true);
        levelPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(false);
        MiniGameCounterText.text = Player.snowFlakes.ToString();
        //MiniGameManager.miniGameManager.SetTheBoard();
    }

    public void HideMiniGamePanel()
    {
        MiniGamePanel.gameObject.SetActive(false);
        levelPanel.gameObject.SetActive(true);
    }


    public void TutorialPhaseComplete(int phase)
    {
        switch (phase)
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

    public void PlayMenuIntro()
    {
        GameManager.gameManager.menuCamera.enabled = false;
        mainMenu.gameObject.SetActive(false);
        GameManager.gameManager.introCamera.transform.gameObject.SetActive(true);
        GameManager.gameManager.introCamera.GetComponent<Animator>().SetTrigger("MenuLevel");
        /*
        foreach (var level in levels)
        {
            level.gameObject.SetActive(false);
        }*/
    }

    void PlayLoadLevelIntro()
    {
        GameManager.gameManager.introCamera.GetComponent<Animator>().SetTrigger("LoadLevel");
    }

    public void SetSpecialHeroIndicator()
    {
        if (Player.specialHero == 0)
        {
            specialHeroIndicator.gameObject.SetActive(false);
            specialHeroSpawnButton.interactable = false;
        }
        else
        {
            specialHeroIndicatorText.text = Player.specialHero.ToString();
            specialHeroIndicator.gameObject.SetActive(true);
            specialHeroSpawnButton.interactable = true;

        }
    }

    public void ItemShopTabClicked(int id)
    {
        switch (id)
        {
            case 0:
                miniGameTabButton.interactable = false;
                heroesTabButton.interactable = true;
                adsTabButton.interactable = true;
                miniGameItemPanel.gameObject.SetActive(true);
                heroesItemPanel.gameObject.SetActive(false);
                adsItemPanel.gameObject.SetActive(false);
                break;
            case 1:
                miniGameTabButton.interactable = true;
                heroesTabButton.interactable = false;
                adsTabButton.interactable = true;
                heroesItemPanel.gameObject.SetActive(true);
                miniGameItemPanel.gameObject.SetActive(false);
                adsItemPanel.gameObject.SetActive(false);
                break;
            case 2:
                miniGameTabButton.interactable = true;
                heroesTabButton.interactable = true;
                adsTabButton.interactable = false;
                adsItemPanel.gameObject.SetActive(true);
                miniGameItemPanel.gameObject.SetActive(false);
                heroesItemPanel.gameObject.SetActive(false);
                break;
        }


    }

    public void ActivateHeroes()
    {

        if (Player.completedLevels[1] > 0)
        {
            frogSpawnButton.gameObject.SetActive(true);
        }
        if(Player.completedLevels[2] > 0)
        {
            lizardSpawnButton.gameObject.SetActive(true);
        }
    }

	IEnumerator Fps()
	{
		fps.text = "fps: " + 1.0f / Time.deltaTime;

		yield return new WaitForSeconds (.5f);

		StartCoroutine (Fps ());
	}

}
