using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level12 : Level {

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
    public List<int> wave_26 = new List<int>();
    public List<int> wave_27 = new List<int>();
    public List<int> wave_28 = new List<int>();
    public List<int> wave_29 = new List<int>();
    public List<int> wave_30 = new List<int>();
    public List<int> wave_31 = new List<int>();
    public List<int> wave_32 = new List<int>();
    public List<int> wave_33 = new List<int>();
    public List<int> wave_34 = new List<int>();
    public List<int> wave_35 = new List<int>();
    public List<int> wave_36 = new List<int>();
    public List<int> wave_37 = new List<int>();
    public List<int> wave_38 = new List<int>();
    public List<int> wave_39 = new List<int>();
    public List<int> wave_40 = new List<int>();
    public List<int> wave_41 = new List<int>();
    public List<int> wave_42 = new List<int>();
    public List<int> wave_43 = new List<int>();
    public List<int> wave_44 = new List<int>();
    public List<int> wave_45 = new List<int>();

    [Header("Destinations Path 1")]
    public List<Transform> destination_1_1 = new List<Transform>();
    public List<Transform> destination_1_2 = new List<Transform>();
    public List<Transform> destination_1_3 = new List<Transform>();
    public List<Transform> destination_1_4 = new List<Transform>();
    public List<Transform> destination_1_5 = new List<Transform>();
    public List<Transform> destination_1_6 = new List<Transform>();


    [Header("Destinations Path 2")]
    public List<Transform> destination_2_1 = new List<Transform>();
    public List<Transform> destination_2_2 = new List<Transform>();
    public List<Transform> destination_2_3 = new List<Transform>();
    public List<Transform> destination_2_4 = new List<Transform>();
    public List<Transform> destination_2_5 = new List<Transform>();
    public List<Transform> destination_2_6 = new List<Transform>();


    [Header("Destinations All Paths")]
    public List<Transform> destination_All_1 = new List<Transform>();
    public List<Transform> destination_All_2 = new List<Transform>();
    public List<Transform> destination_All_3 = new List<Transform>();
    public List<Transform> destination_All_4 = new List<Transform>();
    public List<Transform> destination_All_5 = new List<Transform>();



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
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_26);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_27);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_28);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_29);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_30);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_31);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_32);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_33);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_34);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_35);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_36);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_37);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_38);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_39);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_40);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_41);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_42);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_43);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_44);
        GameManager.gameManager.enemyListForCurrentLevel.Add(wave_45);

        GameManager.gameManager.enemyDestination.Clear();
        path_1.Add(destination_1_1);
        path_1.Add(destination_1_2);
        path_1.Add(destination_1_3);
        path_1.Add(destination_1_4);
        path_1.Add(destination_1_5);
        path_1.Add(destination_1_6);
        path_1.Add(destination_All_1);
        path_1.Add(destination_All_2);
        path_1.Add(destination_All_3);
        path_1.Add(destination_All_4);
        path_1.Add(destination_All_5);
        GameManager.gameManager.enemyDestination.Add(0, path_1);

        path_2.Add(destination_2_1);
        path_2.Add(destination_2_2);
        path_2.Add(destination_2_3);
        path_2.Add(destination_2_4);
        path_2.Add(destination_2_5);
        path_2.Add(destination_2_6);
        path_2.Add(destination_All_1);
        path_2.Add(destination_All_2);
        path_2.Add(destination_All_3);
        path_2.Add(destination_All_4);
        path_2.Add(destination_All_5);

        GameManager.gameManager.enemyDestination.Add(1, path_2);

        GameManager.gameManager.spawnPoints = spawnPoints;
        GameManager.gameManager.specialHeroSpawnLocation = specialHeroSpawnPoint;
        GameManager.gameManager.spawnTimerMin = spawnTimerMin;
        GameManager.gameManager.spawnTimerMax = spawnTimerMax;
        GameManager.gameManager.levelInitialResource = startResource;
    }
}
