using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level2 : MonoBehaviour {

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
    public List<Transform> destination_8 = new List<Transform>();



    void OnEnable()
    {

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

        GameManager.gameManager.enemyDestination.Clear();
        GameManager.gameManager.enemyDestination.Add(destination_1);
        GameManager.gameManager.enemyDestination.Add(destination_2);
        GameManager.gameManager.enemyDestination.Add(destination_3);
        GameManager.gameManager.enemyDestination.Add(destination_4);
        GameManager.gameManager.enemyDestination.Add(destination_5);
        GameManager.gameManager.enemyDestination.Add(destination_6);
        GameManager.gameManager.enemyDestination.Add(destination_7);
        GameManager.gameManager.enemyDestination.Add(destination_8);

    }
}
