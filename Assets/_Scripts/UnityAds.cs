using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class UnityAds : MonoBehaviour {

    public static UnityAds ads;

    void Start()
    {
        ads = this;
        Advertisement.Initialize("1179838", false);
    }

    public void ShowAd(string zone = "")
    {
        if(string.Equals(zone, ""))
        {
            zone = null;
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
                Debug.Log("Reward Player");
                GameHUDManager.gameHudManager.ShowInfoPanel();
                Player.snowFlakes++;
                GameHUDManager.gameHudManager.MenuHudUpdate();
                Debug.Log("Snowflake added, make a panel for it :)");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad skipped");
                break;
            case ShowResult.Failed:
                Debug.Log("Ad Failed");
                break;
        }
    }
}
