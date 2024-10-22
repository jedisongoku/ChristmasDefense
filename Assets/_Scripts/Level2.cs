﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level2 : Level
{
    /*
    [Header("Level")]
    public Transform[] spawnPoints;
    public Transform specialHeroSpawnPoint;
    public Transform cameraLocation;
    public Transform CameraMoveLimitTop;
    public Transform CameraMoveLimitBottom;
    public float spawnTimerMin;
    public float spawnTimerMax;*/

    [Header("Waves")]
    public List<int> wave_1 = new List<int>();
    public List<int> wave_2 = new List<int>();
    public List<int> wave_3 = new List<int>();
    public List<int> wave_4 = new List<int>();
    public List<int> wave_5 = new List<int>();
    public List<int> wave_6 = new List<int>();
    public List<int> wave_7 = new List<int>();
    public List<int> wave_8 = new List<int>();
    public List<int> wave_9 = new List<int>();
    public List<int> wave_10 = new List<int>();

    [Header("Destinations")]
    public List<Transform> destination_1 = new List<Transform>();
    public List<Transform> destination_2 = new List<Transform>();
    public List<Transform> destination_3 = new List<Transform>();
    public List<Transform> destination_4 = new List<Transform>();
    public List<Transform> destination_5 = new List<Transform>();
    public List<Transform> destination_6 = new List<Transform>();
    public List<Transform> destination_7 = new List<Transform>();

    private List<List<Transform>> path_1 = new List<List<Transform>>();

    void OnEnable()
    {
        GameManager.gameManager.cameraLocation = cameraLocation;
        TouchController.touchController.CameraMoveLimitTop = CameraMoveLimitTop;
        TouchController.touchController.CameraMoveLimitBottom = CameraMoveLimitBottom;

        GameManager.gameManager.enemyListForCurrentLevel.Clear();
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_1);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_2);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_3);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_4);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_5);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_6);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_7);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_8);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_9);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_10);

        Debug.Log(GameManager.gameManager.enemyListForCurrentLevel.Count);

        GameManager.gameManager.enemyDestination.Clear();
		path_1.Clear ();
        path_1.Add(destination_1);
        path_1.Add(destination_2);
        path_1.Add(destination_3);
        path_1.Add(destination_4);
        path_1.Add(destination_5);
        path_1.Add(destination_6);
        path_1.Add(destination_7);
        GameManager.gameManager.enemyDestination.Add(0, path_1);

        Debug.Log("Game Mode: " + GameManager.gameManager.gameMode);
        if (GameManager.gameManager.gameMode)
        {
            GameManager.gameManager.spawnTimerMin = hardModeSpawnTimerMin;
            GameManager.gameManager.spawnTimerMax = hardModeSpawnTimerMax;
            GameManager.gameManager.levelInitialResource = hardModeStartResource;
        }
        else
        {
            GameManager.gameManager.spawnTimerMin = spawnTimerMin;
            GameManager.gameManager.spawnTimerMax = spawnTimerMax;
            GameManager.gameManager.levelInitialResource = startResource;
        }


        GameManager.gameManager.spawnPoints = spawnPoints;
        GameManager.gameManager.specialHeroSpawnLocation = specialHeroSpawnPoint;

        Player.resource = GameManager.gameManager.levelInitialResource;
        GameHUDManager.gameHudManager.GameHudUpdate();



    }
}
