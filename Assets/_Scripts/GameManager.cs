using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

    //public delegate void UIInfoAction();
    //public static event UIInfoAction OnUIAction;

    public static GameManager gameManager;

    [Header("Menu")]
    public Transform background;
    public Camera menuCamera;
    public Camera introCamera;

    [Header("Gameplay")]
    public Dictionary<int, List<List<Transform>>> enemyDestination = new Dictionary<int, List<List<Transform>>>();

    public List<List<int>> enemyListForCurrentLevel = new List<List<int>>();
    public List<int> enemyListForCurrentWave = new List<int>();

    public GameObject activeLevel;
    public int levelInitialResource;
    public int levelInitialLife;
    public int levelInitialSpecialHero;
    public int levelCompletedStars;

    public List<GameObject> heroes;
    public List<GameObject> enemies;
    public GameObject selectedSpawnPoint;
    public GameObject tutorialHero;
    //public Dictionary<int, List<Transform>> spawnPoints = new Dictionary<int, List<Transform>>();
    public Transform[] spawnPoints;
    public Transform cameraLocation;
    public Transform specialHeroSpawnLocation;

    public int level;
    public float levelStartTimer;
    public float waveStartTimer;
    public float spawnTimerMin;
    public float spawnTimerMax;
    public float healthMultiplier = 1;
    //public int randomVariableMax;

    [Header("Heroes")]
    public int tigerCost;
    public int frogCost;
    public int lizardCost;
    public int tigerUpgradeCost;
    public int frogUpgradeCost;
    public int lizardUpgradeCost;

    public bool canSpawnHero = false;
    public bool isGamePaused = false;
    public bool isFastForward = false;

    public bool canSelectSpawnPoint = true;


    [Header("Tutorial")]
    public bool isTutorial = false;
    public bool tutorialPhase_1 = false;
    public bool tutorialPhase_2 = false;
    public bool tutorialPhase_3 = false;

    //private Dictionary<int, Dictionary<int, List<int>>> enemySpawnList = new Dictionary<int, Dictionary<int, List<int>>>();
    private float levelTimer = 0f;
    private float waveTimer = 0f;
    private float spawnTimer = 0f;
    private int enemyCount = 0; // how many enemies total will spawn on wave
    private int enemiesLeft = 0;
    private int enemiesSpawned = 0; // how many enemies are already spawned
    private int currentWave = 0;

    private bool isWaveStarted = false;
    private bool isLevelStarted = false;
    private bool isLevelEnded = false;
    
    
    

    private Vector3 spawnOffset = new Vector3(0, 0.1f, 0);

    void Awake()
    {
        gameManager = this;

    }

    void Start()
    {
        DataStore.Load();
    }

    void Update()
    {
        levelTimer += Time.deltaTime;
        waveTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;
    }

    public void OnRestartLevel()
    {
        Player.life = levelInitialLife;
        Player.specialHero = levelInitialSpecialHero;
        DataStore.Save();
        StopAllCoroutines();

    }

    public void OnGoHome()
    {
        if(!isLevelEnded)
        {
            Player.life = levelInitialLife;
            Player.specialHero = levelInitialSpecialHero;
        }  
        StopAllCoroutines();
    }

    public void StartLevel()
    {
        Debug.Log(isTutorial);
        //background.gameObject.SetActive(false);
        //menuCamera.enabled = false;

        Time.timeScale = 1;
        levelTimer = 0;
        waveTimer = 0;
        spawnTimer = 0;
        currentWave = 0;
        healthMultiplier = 1;

        isWaveStarted = false;
        isLevelStarted = false;
        isLevelEnded = false;

        Player.score = 0;
        Player.resource = levelInitialResource;
        levelInitialLife = Player.life;
        levelInitialSpecialHero = Player.specialHero;
        

        //Camera.main.transform.position = cameraLocations[level - 1].position;
        //specialHeroSpawnLocation = enemyDestination[0][enemyDestination[0].Count - 1][0];

        if (isTutorial && level == 1)
        {
            GameHUDManager.gameHudManager.tapHereTooltip.gameObject.SetActive(true);
            tutorialPhase_1 = true;
        }
        
        SetEnemiesForNextWave();
        StartCoroutine(RunGame());

        GameHUDManager.gameHudManager.GameHudUpdate();
        GameHUDManager.gameHudManager.SetSpecialHeroIndicator();
        GameHUDManager.gameHudManager.ActivateHeroes();
    }

    IEnumerator RunGame()
    {
        //Debug.Log("RUNGAME");

        if(Input.GetKeyDown("space"))
        {
            currentWave++;
        }

        if (levelTimer >= levelStartTimer && !isLevelStarted)
        {
            
            isLevelStarted = true;
        }

        if (waveTimer >= waveStartTimer && !isWaveStarted && !isTutorial)
        {
            isWaveStarted = true;
            GameHUDManager.gameHudManager.GameHudUpdate();
            //OnUIAction();
            StartCoroutine(SpawnEnemy());
        }

        if (enemiesLeft == 0 && isWaveStarted)
        {
            currentWave++;
            if (currentWave < enemyListForCurrentLevel.Count)
            {
                Debug.Log("Next Wave Will Start Shortly...");

                if (GetCurrentWave() % 5 == 0 || GetCurrentWave() == enemyListForCurrentLevel.Count)
                {
                    Debug.Log("MULTIPLLLYYYYYYYYYYYYY");
                    healthMultiplier *= 1.5f;

                }

                //GameHUDManager.gameHudManager.GameHudUpdate();

                SetEnemiesForNextWave();

            }
            else
            {
                LevelEnd();
            }

        }

        yield return new WaitForSeconds(0);

        if(!isLevelEnded)
        {
            StartCoroutine(RunGame());
        }
        
    }

    public void SpawnHero(int hero)
    {

        //HideHeroes();
        MouseController.isMouseOnUI = false;
        canSpawnHero = false;
        GameHUDManager.gameHudManager.HideHeroes();
        //selectedSpawnPoint.GetComponent<HeroSpawnManager>().particleAfterSpawn.gameObject.SetActive(true);
        selectedSpawnPoint.GetComponentInChildren<ParticleSystem>().Play();
        
        if(tutorialPhase_2)
        {
            GameHUDManager.gameHudManager.TutorialPhaseComplete(2);
        }

        if (hero == 1 && Player.resource >= tigerCost)
        {
            Player.resource -= tigerCost;
            selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero = Instantiate(heroes[0], selectedSpawnPoint.transform.position + spawnOffset, Quaternion.identity) as GameObject;
            selectedSpawnPoint.GetComponent<HeroSpawnManager>().radius = selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero.GetComponent<Hero>().radius;
            GameHUDManager.gameHudManager.GameHudUpdate();
        }
        else if (hero == 2 && Player.resource >= frogCost)
        {
            Player.resource -= frogCost;
            selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero = Instantiate(heroes[1], selectedSpawnPoint.transform.position + spawnOffset, Quaternion.identity) as GameObject;
            selectedSpawnPoint.GetComponent<HeroSpawnManager>().radius = selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero.GetComponent<Hero>().radius;
            GameHUDManager.gameHudManager.GameHudUpdate();
        }
        else if (hero == 3 && Player.resource >= lizardCost)
        {
            Player.resource -= lizardCost;
            selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero = Instantiate(heroes[2], selectedSpawnPoint.transform.position + spawnOffset, Quaternion.identity) as GameObject;
            selectedSpawnPoint.GetComponent<HeroSpawnManager>().radius = selectedSpawnPoint.GetComponent<HeroSpawnManager>().assignedHero.GetComponent<Hero>().radius;
            GameHUDManager.gameHudManager.GameHudUpdate();
        }

        if (tutorialPhase_3 && Player.resource <= 150)
        {
            tutorialHero = selectedSpawnPoint;
            tutorialHero.GetComponent<HeroSpawnManager>().ShowTutorialTooltip();
        }
    }


    IEnumerator SpawnEnemy()
    {
        int path = Mathf.CeilToInt(Random.Range(0, spawnPoints.Length));
        GameObject enemy = Instantiate(enemies[enemyListForCurrentWave[enemiesSpawned]], spawnPoints[path].position, spawnPoints[path].rotation) as GameObject;
        enemy.GetComponent<Enemy>().enemyPath = path;
        enemiesSpawned++;

        yield return new WaitForSeconds(Random.Range(spawnTimerMin, spawnTimerMax));

        if(enemiesSpawned < enemyCount)
        {
            //Debug.Log(enemiesSpawned);
            StartCoroutine(SpawnEnemy());
        }
        
    }


    void SetEnemiesForNextWave()
    {
        enemyCount = 0;
        enemiesSpawned = 0;
        enemiesLeft = 0;
        enemyListForCurrentWave.Clear();
        

        for (int j = 0; j < enemyListForCurrentLevel[currentWave].Count; j++)
        {
            int count = enemyListForCurrentLevel[currentWave][j];
            enemyCount += count;

            for (int i = 0; i < count; i++)
            {
                enemyListForCurrentWave.Add(j);
            }
        }

        enemiesLeft = enemyCount;
        ShuffleTheWave();
        waveTimer = 0;

        //Debug.Log("Enemy Count " + enemyCount);
    }

    void ShuffleTheWave()
    {
        for (int i = 0; i < enemyListForCurrentWave.Count; i++)
        {
            int temp = enemyListForCurrentWave[i];
            int r = Random.Range(i, enemyListForCurrentWave.Count);
            enemyListForCurrentWave[i] = enemyListForCurrentWave[r];
            enemyListForCurrentWave[r] = temp;
        }
        //Debug.Log("Shuffled");

        isWaveStarted = false;
        
    }

    public void EnemyDead()
    {
        enemiesLeft--;
    }

    public int GetCurrentWave()
    {
        return currentWave + 1;
    }

    public void LevelEnd()
    {
        Debug.Log("Level End");
        StopAllCoroutines();
        isLevelEnded = true;

        if (isFastForward)
        {
            GameHUDManager.gameHudManager.FastForward();
        }
        if ((float)Player.life / (float)levelInitialLife > .8f)
        {
            levelCompletedStars = 3;
        }
        else if ((float)Player.life / (float)levelInitialLife > .45f)
        {
            levelCompletedStars = 2;
        }
        else if ((float)Player.life / (float)levelInitialLife == 0)
        {
            levelCompletedStars = 0;
        }
        else
        {
            levelCompletedStars = 1;
        }

        if (levelCompletedStars > Player.completedLevels[level])
        {
            Player.completedLevels[level] = levelCompletedStars;
        }

        if(levelCompletedStars != 0)
        {
            Player.boostPoints += Player.resource;
        }
        
        GameHUDManager.gameHudManager.LevelComplete();
        
    }
        
    public void LifeLost(int lost)
    {
        if(!isLevelEnded)
        {
            if(Player.life - lost >= 0)
            {
                Player.life -= lost;
            }
            else
            {
                Player.life = 0;
            }
            
            GameHUDManager.gameHudManager.GameHudUpdate();
            if (Player.life == 0)
            {
                LevelEnd();
            }
        }
        
        //OnUIAction();
    }

    public bool IsLevelEnded()
    {
        return isLevelEnded;
    }

    public void ResetWaveTimer()
    {
        waveTimer = 0;
    }

    public void SpawnSpecialHero()
    {
        Player.specialHero--;
        GameHUDManager.gameHudManager.SetSpecialHeroIndicator();
        Instantiate(heroes[3], specialHeroSpawnLocation.position, specialHeroSpawnLocation.rotation);
        
    }

    

}