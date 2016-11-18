using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataStore : MonoBehaviour{


    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo18.dat");

        PlayerData data = new PlayerData();
        data.life = Player.life;
        data.boostPoints += Player.boostPoints;
        data.completedLevels = Player.completedLevels;
        data.levelScores = Player.levelScores;
        data.snowFlakes = Player.snowFlakes;
        data.specialHero = Player.specialHero;

        bf.Serialize(file, data);
        file.Close();
        
    }

    public static void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo18.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo18.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            Player.life = data.life;
            Player.boostPoints = data.boostPoints;
            Player.completedLevels = data.completedLevels;
            Player.levelScores = data.levelScores;
            Player.snowFlakes = data.snowFlakes;
            Player.specialHero = data.specialHero;

        }
        else
        {
            Player.specialHero = 3;
            Player.snowFlakes = 10; // dont forget to remove this or reduce the number
            Player.completedLevels.Add(1, 0);
            Player.levelScores.Add(1, 0);
            for(int i = 2; i <= 12; i++)
            {
                Player.completedLevels.Add(i, -1);
                Player.levelScores.Add(i, 0);
            }
        }
    }
}

[Serializable]
class PlayerData
{
    public int life;
    public int boostPoints;
    public int snowFlakes;
    public int specialHero;
    public Dictionary<int, int> completedLevels;
    public Dictionary<int, int> levelScores;
}
