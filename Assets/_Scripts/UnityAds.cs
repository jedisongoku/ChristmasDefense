using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class UnityAds : MonoBehaviour {

    public static UnityAds ads;
    public string rewardZone;

    void Start()
    {
        ads = this;

        if(Application.platform == RuntimePlatform.Android)
        {
            Advertisement.Initialize("2964220", false);
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Advertisement.Initialize("2964221", false);
        }
        else
        {
            Advertisement.Initialize("2964221", false);
        }
        
    }

    public void ShowAd(string zone = "")
    {
        rewardZone = zone;
		/*
        if(string.Equals(zone, ""))
        {
            zone = null;
            rewardZone = null;
        }*/

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackHandler;

		Debug.Log ("Ad Free Status " + Player.adFree);
		Debug.Log ("Rewardzone Status " + rewardZone);
        if (GameManager.gameManager.level == 1 && PlayerPrefs.GetString("RateLevel1", "false") == "false")
        {
            RatingManager.Instance.ShowRatingPanel();
            PlayerPrefs.SetString("RateLevel1", "true");
        }
        else if (GameManager.gameManager.level == 6 && PlayerPrefs.GetString("RateLevel6", "false") == "false")
        {
            RatingManager.Instance.ShowRatingPanel();
            PlayerPrefs.SetString("RateLevel6", "true");
        }
        else
        {
            if (!Player.adFree || (Player.adFree && rewardZone.Equals("rewardedVideo")))
            {
                if (Advertisement.IsReady(zone))
                {
                    Advertisement.Show(zone, options);
                }
            }
        }

        
        

        
    }

    void AdCallbackHandler(ShowResult result)
    {
        switch(result)
        {
			case ShowResult.Finished:
                if(rewardZone == "video")
                {
                    GameHUDManager.gameHudManager.ShowInfoPanel(8);
                    
                }
                else
                {
                    Debug.Log("Reward Player");
                    GameHUDManager.gameHudManager.ShowInfoPanel(0);
                    Player.snowFlakes++;
                    GameHUDManager.gameHudManager.MenuHudUpdate();
                    Debug.Log("Snowflake added, make a panel for it :)");
                }
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad skipped");
                GameHUDManager.gameHudManager.ShowInfoPanel(8);
                break;
            case ShowResult.Failed:
                Debug.Log("Ad Failed");
                break;
        }
    }
}
