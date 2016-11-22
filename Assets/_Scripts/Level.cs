using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

    [Header("Level")]
    public Transform[] spawnPoints;
    public Transform specialHeroSpawnPoint;
    public Transform cameraLocation;
    public Transform CameraMoveLimitTop;
    public Transform CameraMoveLimitBottom;
    public float spawnTimerMin;
    public float spawnTimerMax;
    public float hardModeSpawnTimerMin;
    public float hardModeSpawnTimerMax;
    public int startResource;
    public int hardModeStartResource;
}
