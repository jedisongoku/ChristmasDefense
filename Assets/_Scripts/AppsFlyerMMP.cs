using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppsFlyerMMP : MonoBehaviour {

    void Start()
    {
        // For detailed logging
        //AppsFlyer.setIsDebug (true);
        AppsFlyer.setAppsFlyerKey("aTYJZVwsYCTz8BbnbrDbxL");
#if UNITY_IOS
        //Mandatory - set your AppsFlyer’s Developer key.
        
        //Mandatory - set your apple app ID
        //NOTE: You should enter the number only and not the "ID" prefix
        AppsFlyer.setAppID ("YOUR_APP_ID_HERE");
        AppsFlyer.trackAppLaunch ();
#elif UNITY_ANDROID
        //Mandatory - set your Android package name
        AppsFlyer.setAppID("com.belizard.farmdefense");
        //Mandatory - set your AppsFlyer’s Developer key.
        AppsFlyer.init("aTYJZVwsYCTz8BbnbrDbxL");

        //AppsFlyer.setCustomerUserID("659231");

        //For getting the conversion data in Android, you need to this listener.
        //AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks");

#endif
    }
    
    public static void LevelCompleted()
    {

        Dictionary<string, string> levelCompleted = new Dictionary<string, string>();
        levelCompleted.Add("level_completed", "1");
        AppsFlyer.trackRichEvent("level_completed", levelCompleted);

    }

    public static void LevelCompleted_1Star()
    {

        Dictionary<string, string> levelCompleted = new Dictionary<string, string>();
        levelCompleted.Add("level_completed", "1");
        AppsFlyer.trackRichEvent("level_completed", levelCompleted);

    }
    public static void LevelCompleted_2Star()
    {

        Dictionary<string, string> levelCompleted = new Dictionary<string, string>();
        levelCompleted.Add("level_completed", "1");
        AppsFlyer.trackRichEvent("level_completed", levelCompleted);

    }
    public static void LevelCompleted_3Star()
    {

        Dictionary<string, string> levelCompleted = new Dictionary<string, string>();
        levelCompleted.Add("level_completed", "1");
        AppsFlyer.trackRichEvent("level_completed", levelCompleted);

    }

}
