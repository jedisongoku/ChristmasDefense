﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Level7 : Level
{

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
    public List<int> wave_11 = new List<int>();
    public List<int> wave_12 = new List<int>();
    public List<int> wave_13 = new List<int>();
    public List<int> wave_14 = new List<int>();
    public List<int> wave_15 = new List<int>();
    public List<int> wave_16 = new List<int>();
    public List<int> wave_17 = new List<int>();
    public List<int> wave_18 = new List<int>();
    public List<int> wave_19 = new List<int>();
    public List<int> wave_20 = new List<int>();
    public List<int> wave_21 = new List<int>();
    public List<int> wave_22 = new List<int>();
    public List<int> wave_23 = new List<int>();
    public List<int> wave_24 = new List<int>();
    public List<int> wave_25 = new List<int>();

    [Header("Destinations Path 1")]
    public List<Transform> destination_1_1 = new List<Transform>();
    public List<Transform> destination_1_2 = new List<Transform>();


    [Header("Destinations Path 2")]
    public List<Transform> destination_2_1 = new List<Transform>();
    public List<Transform> destination_2_2 = new List<Transform>();


    [Header("Destinations All Paths")]
    public List<Transform> destination_All_1 = new List<Transform>();
    public List<Transform> destination_All_2 = new List<Transform>();
    public List<Transform> destination_All_3 = new List<Transform>();
    public List<Transform> destination_All_4 = new List<Transform>();
    public List<Transform> destination_All_5 = new List<Transform>();
    public List<Transform> destination_All_6 = new List<Transform>();
    public List<Transform> destination_All_7 = new List<Transform>();
    public List<Transform> destination_All_8 = new List<Transform>();
    public List<Transform> destination_All_9 = new List<Transform>();



    private List<List<Transform>> path_1 = new List<List<Transform>>();
    private List<List<Transform>> path_2 = new List<List<Transform>>();

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
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_11);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_12);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_13);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_14);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_15);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_16);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_17);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_18);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_19);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_20);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_21);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_22);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_23);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_24);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_25);

        GameManager.gameManager.enemyDestination.Clear();
		path_1.Clear ();
        path_1.Add(destination_1_1);
        path_1.Add(destination_1_2);
        path_1.Add(destination_All_1);
        path_1.Add(destination_All_2);
        path_1.Add(destination_All_3);
        path_1.Add(destination_All_4);
        path_1.Add(destination_All_5);
        path_1.Add(destination_All_6);
        path_1.Add(destination_All_7);
        path_1.Add(destination_All_8);
        path_1.Add(destination_All_9);
        GameManager.gameManager.enemyDestination.Add(0, path_1);
		path_2.Clear ();
        path_2.Add(destination_2_1);
        path_2.Add(destination_2_2);
        path_2.Add(destination_All_1);
        path_2.Add(destination_All_2);
        path_2.Add(destination_All_3);
        path_2.Add(destination_All_4);
        path_2.Add(destination_All_5);
        path_2.Add(destination_All_6);
        path_2.Add(destination_All_7);
        path_2.Add(destination_All_8);
        path_2.Add(destination_All_9);
        GameManager.gameManager.enemyDestination.Add(1, path_2);

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
