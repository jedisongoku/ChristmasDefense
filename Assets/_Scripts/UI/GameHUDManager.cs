using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Collections.Generic;

public class GameHUDManager : MonoBehaviour
{
    public delegate void SelectLevelAction();
    public static event SelectLevelAction SetAllImages;

    public static GameHUDManager gameHudManager;
    //public AudioSource audioTrack;
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
    public Transform miniGamePanel;
    public Transform infoPanel;
    public Transform informationPanel;
    public Transform heroesInformation;
    public Transform enemiesInformation;
    public Text MiniGameCounterText;
    public Text boostPointTextMenu;
    public Text lifeTextMenu;
    public Transform snowFlakeIndicator;
    public Text snowFlakeText;
    public Transform specialHeroIndicatorMenu;
    public Text specialHeroIndicatorTextMenu;
    public Text infoPanelText;
    public Transform spartanUnlocked;
    public Transform wizardUnlocked;
    public Transform normalModeLevels;
    public Transform hardModeLevels;
    public Transform normalModeIndicator;
    public Transform hardModeIndicator;
    public Sprite normalModeLevelBackground;
    public Sprite hardModeLevelBackround;

    [Header("GameHUD")]
    public Transform heroesPanel;
    public Transform levelCompletePanel;
    public Transform pausePanel;
    public Transform buttonsPanel;
    public Transform heroInfoPanel;
    public Transform specialHeroIndicator;
    public Transform TutorialSkipPanel;
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
    public Transform specialHeroTooltip;
    public Transform upgradeHeroTooltip;
    public Transform tutorialInfoPanelPhase_1;
    public Transform tutorialInfoPanelPhase_2;
    public Transform tutorialInfoPanelPhase_3;
    public Transform tutorialInfoPanelPhase_4;
    public Transform tutorialInfoPanelPhase_5;
    public Transform tutorialInfoPanelPhase_6;
    public GameObject tutorialselectedHeroSpawnPoint;

    [Header("In-Game Shop")]
    public Transform miniGameItemPanel;
    public Transform heroesItemPanel;
    public Transform adsItemPanel;
    public Button miniGameTabButton;
    public Button heroesTabButton;
    public Button adsTabButton;



    [Header("Level")]
    public List<Transform> levels;

    [Header("UI")]
    public Button playPauseButton;
    public Button fastForwardButton;
    public Button menuMusicButton;
    public Button gameMenuMusicButton;

    [Header("UI Sprite")]
    public Sprite pauseButtonImage;
    public Sprite playButtonImage;
    public Sprite fastForwardImage;
    public Sprite fastForwardCancelImage;
    public Sprite levelFailedImage;
    public Sprite levelComplete_Star1Image;
    public Sprite levelComplete_Star2Image;
    public Sprite levelComplete_Star3Image;
    public Sprite musicOnImage;
    public Sprite musicOffImage;
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

        Social.localUser.Authenticate(success => { if (success) { Debug.Log("==iOS GC authenticate OK"); } else { Debug.Log("==iOS GC authenticate Failed"); } });


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
        //UpgradeButton.gameObject.SetActive(true);
        //UpgradeButton.interactable = false;
        if(heroInfoId != 0)
        {
            if(GameManager.gameManager.selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero != null)
            {
                switch (heroInfoId)
                {
                    case 1:
                        heroInfoPanelImage.sprite = archerInfoPanelImage;
                        if (!GameManager.gameManager.selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero.GetComponent<Hero>().isUpgraded)
                        {
                            heroInfoPanelImage.sprite = archerInfoPanelImage;
                            UpgradeButton.gameObject.SetActive(true);
                            if (Player.resource >= GameManager.gameManager.tigerUpgradeCost)
                            {
                                UpgradeButton.interactable = true;

                            }
                            else
                            {
                                UpgradeButton.interactable = false;
                            }
                        }
                        else
                        {
                            heroInfoPanelImage.sprite = archerInfoPanelUpgradedImage;
                        }

                        break;
                    case 2:
                        //heroInfoPanelImage.sprite = spartanInfoPanelImage;
                        if (!GameManager.gameManager.selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero.GetComponent<Hero>().isUpgraded)
                        {
                            heroInfoPanelImage.sprite = spartanInfoPanelImage;
                            UpgradeButton.gameObject.SetActive(true);
                            if (Player.resource >= GameManager.gameManager.frogUpgradeCost)
                            {
                                UpgradeButton.interactable = true;
                                UpgradeButton.gameObject.SetActive(true);
                            }
                            else
                            {
                                UpgradeButton.interactable = false;
                            }
                        }
                        else
                        {
                            heroInfoPanelImage.sprite = spartanInfoPanelUpgradedImage;
                        }
                        break;
                    case 3:
                        //heroInfoPanelImage.sprite = wizardInfoPanelImage;
                        if (!GameManager.gameManager.selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero.GetComponent<Hero>().isUpgraded)
                        {
                            heroInfoPanelImage.sprite = wizardInfoPanelImage;
                            UpgradeButton.gameObject.SetActive(true);
                            if (Player.resource >= GameManager.gameManager.lizardUpgradeCost)
                            {
                                UpgradeButton.interactable = true;
                                UpgradeButton.gameObject.SetActive(true);
                            }
                            else
                            {
                                UpgradeButton.interactable = false;
                            }
                        }
                        else
                        {
                            heroInfoPanelImage.sprite = wizardInfoPanelUpgradedImage;
                        }
                        break;
                }
            }
            
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
        HideHardModeLevels();
        //ShowAd(null);


    }

    void HideHardModeLevels()
    {
        hardModeLevels.gameObject.SetActive(false);
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
        SoundManager.soundManager.SwitchSound(false);

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
        gameHUD.gameObject.SetActive(false);
        HeroSpawnManager.DestroyAssignedHeroes();
        SpecialHero.DestorySpecialHeroes();
        levelCompletePanel.gameObject.SetActive(false);
        levels[GameManager.gameManager.level - 1].gameObject.SetActive(false);

        menuHUD.gameObject.SetActive(true);
        SelectLevel(GameManager.gameManager.level + 1);
        SoundManager.soundManager.SwitchSound(false);

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
        //gameHUD.gameObject.SetActive(false);
        Camera.main.transform.position = GameManager.gameManager.cameraLocation.position;
        Camera.main.fieldOfView = 60;
        GameManager.gameManager.StartLevel();
        SoundManager.soundManager.backgroundAudioSource.volume = 1;
        //GameManager.gameManager.introCamera.transform.gameObject.SetActive(true);
        //GameManager.gameManager.introCamera.GetComponent<Animator>().SetTrigger("Intro" + GameManager.gameManager.level);
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
        mainMenu.gameObject.SetActive(false);
        levelMenu.gameObject.SetActive(true);
        Time.timeScale = 1;
        SoundManager.soundManager.SwitchSound(true);

    }

    public void Resume()
    {
        pausePanel.gameObject.SetActive(false);
        buttonsPanel.gameObject.SetActive(true);
        playPauseButton.image.sprite = pauseButtonImage;
        fastForwardButton.image.sprite = fastForwardImage;
        Time.timeScale = 1;
        SoundManager.soundManager.backgroundAudioSource.volume *= 2;
        GameManager.gameManager.isFastForward = false;
    }

    public void PauseGame()
    {
        if (!GameManager.gameManager.isGamePaused)
        {
            Time.timeScale = 0;
            GameManager.gameManager.isGamePaused = true;
            playPauseButton.image.sprite = playButtonImage;
            SoundManager.soundManager.backgroundAudioSource.volume /= 2;
        }
        else
        {
            Time.timeScale = 1;
            GameManager.gameManager.isGamePaused = false;
            playPauseButton.image.sprite = pauseButtonImage;
            SoundManager.soundManager.backgroundAudioSource.volume *= 2;
        }

    }

    public void PauseMenu()
    {
        pausePanel.gameObject.SetActive(true);
        buttonsPanel.gameObject.SetActive(false);
        Time.timeScale = 0;
        SoundManager.soundManager.backgroundAudioSource.volume /= 2;
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
        if(GameManager.gameManager.gameMode) // HARD MODE
        {
            Player.score += (Player.resource * 55 * 10) + (10000 * GameManager.gameManager.levelCompletedStars * GameManager.gameManager.levelCompletedStars);

            if (GameManager.gameManager.levelCompletedStars != 0)
            {
                if (Player.levelScoresHardMode[GameManager.gameManager.level] < Player.score)
                {
                    Player.levelScoresHardMode[GameManager.gameManager.level] = Player.score;
                }
                if (Player.completedLevelsHardMode[GameManager.gameManager.level] < GameManager.gameManager.levelCompletedStars)
                {
                    Player.completedLevelsHardMode[GameManager.gameManager.level] = GameManager.gameManager.levelCompletedStars;
                }
                DataStore.Save();
                boostPointText.text = Player.resource.ToString();
                scoreText.text = string.Format("{0:#,#}", Player.score);
                boostPointText.enabled = true;
            }
        }
        else // NORMAL MODE
        {
            Player.score += (Player.resource * 55) + (10000 * GameManager.gameManager.levelCompletedStars);

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
                scoreText.text = string.Format("{0:#,#}", Player.score);
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
        

        ReportScore();
        HideAllPanels();


    }

    void ReportScore()
    {
        if (Social.localUser.authenticated)
        {
            int totalScore = 0;
            if(GameManager.gameManager.gameMode)
            {
                foreach (var score in Player.levelScores)
                {
                    totalScore += score.Value;
                }
                Debug.Log("TOTAL SCORE: " + totalScore);
                Social.ReportScore(totalScore, "christmasdefensehardmodeleaderboard", success =>
                { if (success) { Debug.Log("==iOS GC report score ok: " + totalScore + "\n"); } else { Debug.Log("==iOS GC report score Failed: " + "christmasdefenseleaderboard" + "\n"); } });
            }
            else
            {
                foreach (var score in Player.levelScores)
                {
                    totalScore += score.Value;
                }
                Debug.Log("TOTAL SCORE: " + totalScore);
                Social.ReportScore(totalScore, "christmasdefenseleaderboard", success =>
                { if (success) { Debug.Log("==iOS GC report score ok: " + totalScore + "\n"); } else { Debug.Log("==iOS GC report score Failed: " + "christmasdefenseleaderboard" + "\n"); } });
            }
            
        }
        else
        {
            Debug.Log("==iOS GC can't report score, not authenticated\n");
        }
    }

    public void ShowShop()
    {
        levelPanel.gameObject.SetActive(false);
        miniGamePanel.gameObject.SetActive(false);
        informationPanel.gameObject.SetActive(false);
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
        if(GameManager.gameManager.selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero != null)
        {
            GameManager.gameManager.selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero.GetComponent<Hero>().Upgrade();
            GameManager.gameManager.selectedSpawnPoint.GetComponent<HeroSpawnManager>().HideObjects();
            GameManager.gameManager.selectedSpawnPoint.GetComponentInChildren<ParticleSystem>().Play();
            GameManager.gameManager.PlaySound();
            heroInfoPanel.gameObject.SetActive(false);
            MouseController.isMouseOnUI = false;

            if (GameManager.gameManager.tutorialPhase_5)
            {
                TutorialPhaseStart(5);
            }

        }
        
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
        miniGamePanel.gameObject.SetActive(true);
        levelPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(false);
        informationPanel.gameObject.SetActive(false);
        MiniGameCounterText.text = Player.snowFlakes.ToString();
        //MiniGameManager.miniGameManager.SetTheBoard();
    }

    public void HideMiniGamePanel()
    {
        miniGamePanel.gameObject.SetActive(false);
        levelPanel.gameObject.SetActive(true);
    }


    public void TutorialPhaseStart(int phase)
    {
        switch (phase)
        {
            case 1:
                Debug.Log("phase 1");
                GameManager.gameManager.tutorialPhase_1 = true;
                ShowInfoPanel(1);
                    

                /*
                GameManager.gameManager.tutorialPhase_1 = false;
                tapHereTooltip.gameObject.SetActive(false);
                resourceTooltip.gameObject.SetActive(true);
                lifeTooltip.gameObject.SetActive(true);
                specialHeroTooltip.gameObject.SetActive(true);
                selectHeroTooltip.gameObject.SetActive(true);
                GameManager.gameManager.tutorialPhase_2 = true;
                */
                break;
            case 2:

                Debug.Log("phase 2");
                ShowInfoPanel(2);
                /*
                GameManager.gameManager.ResetWaveTimer();
                GameManager.gameManager.isTutorial = false;
                GameManager.gameManager.tutorialPhase_2 = false;
                selectHeroTooltip.gameObject.SetActive(false);
                resourceTooltip.gameObject.SetActive(false);
                lifeTooltip.gameObject.SetActive(false);
                selectHeroTooltip.gameObject.SetActive(false);
				specialHeroTooltip.gameObject.SetActive (false);
                GameManager.gameManager.tutorialPhase_3 = true;
                */

                break;
            case 3:
                Debug.Log("phase 3");
                GameManager.gameManager.tutorialPhase_3 = true;
                ShowInfoPanel(3);
                

                break;
            case 4:
                Debug.Log("phase 4");
                ShowInfoPanel(4);

                break;
            case 5:
                Debug.Log("phase 5");
                ShowInfoPanel(5);
                break;
            case 6:
                Debug.Log("phase 6");
                GameManager.gameManager.tutorialPhase_6 = true;
                ShowInfoPanel(6);
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

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    /// <summary>
    /// 0 - Snowflake
    /// 8 - Unlock Hero
    /// 1 - Tutorial Phase 1
    /// 2 - Tutorial Phase 2
    /// 3 - Tutorial Phase 3
    /// 4 - Tutorial Phase 4
    /// 5 - Tutorial Phase 5
    /// 6 - Tutorial Phase 6

    /// </summary>
    /// <param name="select"></param>
    public void ShowInfoPanel(int select)
    {
        //infoPanel.localScale = new Vector3(0, 0, 0);
        MouseController.isMouseOnUI = true;

        switch (select)
        {
            case 0:
                infoPanelText.gameObject.SetActive(true);
                infoPanel.gameObject.SetActive(true);
                infoPanel.gameObject.GetComponent<Animator>().SetTrigger("Play");
                break;
            
            case 1:
                tutorialInfoPanelPhase_1.gameObject.SetActive(true);
                infoPanel.gameObject.SetActive(true);
                infoPanel.gameObject.GetComponent<Animator>().SetTrigger("Play");
                break;
            case 2:
                tutorialInfoPanelPhase_2.gameObject.SetActive(true);
                infoPanel.gameObject.SetActive(true);
                infoPanel.gameObject.GetComponent<Animator>().SetTrigger("Play");
                resourceTooltip.gameObject.SetActive(true);

                break;
            case 3:
                tutorialInfoPanelPhase_3.gameObject.SetActive(true);
                infoPanel.gameObject.SetActive(true);
                infoPanel.gameObject.GetComponent<Animator>().SetTrigger("Play");
                lifeTooltip.gameObject.SetActive(true);
                break;
            case 4:
                tutorialInfoPanelPhase_4.gameObject.SetActive(true);
                infoPanel.gameObject.SetActive(true);
                infoPanel.gameObject.GetComponent<Animator>().SetTrigger("Play");
                break;
            case 5:
                tutorialInfoPanelPhase_5.gameObject.SetActive(true);
                infoPanel.gameObject.SetActive(true);
                infoPanel.gameObject.GetComponent<Animator>().SetTrigger("Play");
                specialHeroTooltip.gameObject.SetActive(true);
                break;
            case 6:
                tutorialInfoPanelPhase_6.gameObject.SetActive(true);
                infoPanel.gameObject.SetActive(true);
                infoPanel.gameObject.GetComponent<Animator>().SetTrigger("Play");
                break;
            case 8:
                if (GameManager.gameManager.levelCompletedStars != 0)
                {
                    if (GameManager.gameManager.level == 1 && Player.completedLevels[GameManager.gameManager.level + 1] == -1)
                    {
                        wizardUnlocked.gameObject.SetActive(false);
                        infoPanelText.gameObject.SetActive(false);
                        spartanUnlocked.gameObject.SetActive(true);
                        infoPanel.gameObject.SetActive(true);
                        infoPanel.gameObject.GetComponent<Animator>().SetTrigger("Play");
                    }
                    else if (GameManager.gameManager.level == 2 && Player.completedLevels[GameManager.gameManager.level + 1] == -1)
                    {
                        spartanUnlocked.gameObject.SetActive(false);
                        infoPanelText.gameObject.SetActive(false);
                        wizardUnlocked.gameObject.SetActive(true);
                        infoPanel.gameObject.SetActive(true);
                        infoPanel.gameObject.GetComponent<Animator>().SetTrigger("Play");
                    }

                }
                break;
        }

        
        
    }

    public void HideInfoPanel()
    {
        infoPanel.gameObject.SetActive(false);
        infoPanelText.gameObject.SetActive(false);
        spartanUnlocked.gameObject.SetActive(false);
        wizardUnlocked.gameObject.SetActive(false);
        tutorialInfoPanelPhase_1.gameObject.SetActive(false);
        tutorialInfoPanelPhase_2.gameObject.SetActive(false);
        tutorialInfoPanelPhase_3.gameObject.SetActive(false);
        tutorialInfoPanelPhase_4.gameObject.SetActive(false);
        tutorialInfoPanelPhase_5.gameObject.SetActive(false);
        tutorialInfoPanelPhase_6.gameObject.SetActive(false);


        if (GameManager.gameManager.isTutorial)
        {
            if(GameManager.gameManager.tutorialPhase_1)
            {
                GameManager.gameManager.tutorialPhase_1 = false;
                GameManager.gameManager.tutorialPhase_2 = true;
                tapHereTooltip.gameObject.SetActive(true);
            }
            else if(GameManager.gameManager.tutorialPhase_2)
            {

                GameManager.gameManager.tutorialPhase_2 = false;
                //tapHereTooltip.gameObject.SetActive(false);
                resourceTooltip.gameObject.SetActive(false);
                selectHeroTooltip.gameObject.SetActive(true);

                
                
            }
            else if (GameManager.gameManager.tutorialPhase_3)
            {
                GameManager.gameManager.tutorialPhase_3 = false;
                GameManager.gameManager.tutorialPhase_4 = true;
                lifeTooltip.gameObject.SetActive(false);
                tutorialselectedHeroSpawnPoint = GameManager.gameManager.selectedSpawnPoint;
                tutorialselectedHeroSpawnPoint.GetComponent<HeroSpawnManager>().ShowTutorialTooltip();
                //upgradeHeroTooltip.gameObject.SetActive(true);
            }
            else if (GameManager.gameManager.tutorialPhase_4)
            {
                GameManager.gameManager.tutorialPhase_4 = false;
                GameManager.gameManager.tutorialPhase_5 = true;
                tutorialselectedHeroSpawnPoint.GetComponent<HeroSpawnManager>().HideTutorialTooltip();
                upgradeHeroTooltip.gameObject.SetActive(true);
            }
            else if (GameManager.gameManager.tutorialPhase_5)
            {
                GameManager.gameManager.tutorialPhase_5 = false;
                specialHeroTooltip.gameObject.SetActive(true);
                TutorialPhaseStart(6);
            }
            else if (GameManager.gameManager.tutorialPhase_6)
            {
                GameManager.gameManager.tutorialPhase_6 = false;
                selectHeroTooltip.gameObject.SetActive(false);
                specialHeroTooltip.gameObject.SetActive(false);
                GameManager.gameManager.ResetWaveTimer();
                GameManager.gameManager.isTutorial = false;
            }
            

        }
        MouseController.isMouseOnUI = false;
    }

    public void GoHomeFromLevelMenu()
    {
        levelMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void ShowHardModeLevels()
    {
        levelPanel.GetComponent<Image>().sprite = hardModeLevelBackround;
        normalModeLevels.gameObject.SetActive(false);
        normalModeIndicator.gameObject.SetActive(false);
        hardModeIndicator.gameObject.SetActive(true);
        hardModeLevels.gameObject.SetActive(true);
        GameManager.gameManager.gameMode = true;
    }

    public void ShowNormalModeLevels()
    {
        levelPanel.GetComponent<Image>().sprite = normalModeLevelBackground;
        hardModeIndicator.gameObject.SetActive(false);
        hardModeLevels.gameObject.SetActive(false);
        normalModeLevels.gameObject.SetActive(true);
        normalModeIndicator.gameObject.SetActive(true);
        GameManager.gameManager.gameMode = false;
        
    }
    public void ShowTutorialSkipPanel()
    {
		TutorialSkipPanel.localScale = new Vector3 (0, 0, 0);
        TutorialSkipPanel.gameObject.SetActive(true);
        TutorialSkipPanel.GetComponent<Animator>().SetTrigger("Play");

    }

    public void SkipTutorial(bool skip)
    {
        if(skip)
        {
            GameManager.gameManager.isTutorial = false;
        }
        else
        {
            GameManager.gameManager.isTutorial = true;
            TutorialPhaseStart(1);

        }
        GameManager.gameManager.StartLevel();
        TutorialSkipPanel.gameObject.SetActive(false);
        

    }

    public void HideAllPanels()
    {
        HideHeroes();
        HideHeroInfo();
    }

    public void ShowInformationPanel(bool panel)
    {
        miniGamePanel.gameObject.SetActive(false);
        levelPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(false);
        informationPanel.gameObject.SetActive(true);
        switch (panel)
        {
            case true:
                enemiesInformation.gameObject.SetActive(false);
                heroesInformation.gameObject.SetActive(true);
                break;
            case false:
                heroesInformation.gameObject.SetActive(false);
                enemiesInformation.gameObject.SetActive(true);
                
                break;
        }
    }

    public void HideInformationPanel()
    {
        informationPanel.gameObject.SetActive(false);
        levelPanel.gameObject.SetActive(true);
    }

    public void MusicOnOff()
    {
        if(SoundManager.soundManager.backgroundAudioSource.volume < 0.1f)
        {
            menuMusicButton.image.sprite = musicOnImage;
            gameMenuMusicButton.image.sprite = musicOnImage;
            SoundManager.soundManager.backgroundAudioSource.volume = 1;
            
        }
        else
        {
            menuMusicButton.image.sprite = musicOffImage;
            gameMenuMusicButton.image.sprite = musicOffImage;
            SoundManager.soundManager.backgroundAudioSource.volume = 0;
        }
        
    }

    public void OpenFacebook()
    {
        Application.OpenURL("https://www.facebook.com/ChristmasDefense");
    }


}
