using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    public static int resource;
    public static int life = 10;
    public static int boostPoints = 0;
    public static int snowFlakes = 0;
    public static int score = 0;
    public static int specialHero = 0;
    public static Dictionary<int, int> completedLevels = new Dictionary<int, int>();
    public static Dictionary<int, int> levelScores = new Dictionary<int, int>();
    public static Dictionary<int, int> completedLevelsHardMode = new Dictionary<int, int>();
    public static Dictionary<int, int> levelScoresHardMode = new Dictionary<int, int>();
    public static bool tutorial = false;
    public static bool adFree = false;
    public static bool gameMode = false;
    public static float soundVolume;
    public static float fxVolume;
    public static bool christmasGift = true;
    public static bool rated = false;
}
