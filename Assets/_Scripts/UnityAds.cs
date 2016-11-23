using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class UnityAds : MonoBehaviour {

    public static UnityAds ads;
    public string rewardZone;

    void Start()
    {
        ads = this;
        Advertisement.Initialize("1179838", false);
    }

    public void ShowAd(string zone = "")
    {
        rewardZone = zone;
        if(string.Equals(zone, ""))
        {
            zone = null;
            rewardZone = null;
        }

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackHandler;

        if (Advertisement.IsReady(zone))
        {
            Advertisement.Show(zone, options);
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
