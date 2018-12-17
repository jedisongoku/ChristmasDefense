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
        AppsFlyer.setAppID("com.binaryfishgames.christmasdefense");
        //Mandatory - set your AppsFlyer’s Developer key.
        AppsFlyer.init("aTYJZVwsYCTz8BbnbrDbxL", "AppsFlyerTrackerCallbacks");

        //AppsFlyer.setCustomerUserID("659231");

        //For getting the conversion data in Android, you need to this listener.
        //AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks");

#endif
    }

    public static void LevelCompletedNormal(int level)
    {
        Dictionary<string, string> levelCompleted = new Dictionary<string, string>();
        levelCompleted.Add("level" + level.ToString() + "_completed_normal", "1");
        AppsFlyer.trackRichEvent("level" + level.ToString() + "_completed_normal", levelCompleted);

        print("AppsFlyer Normal Mode Level " + level.ToString() + "Event");
    }

    public static void LevelRestarted()
    {
        Dictionary<string, string> levelRestarted = new Dictionary<string, string>();
        levelRestarted.Add("level_restarted", "1");
        AppsFlyer.trackRichEvent("level_restarted", levelRestarted);

        print("AppsFlyer Level Restarted Event");
    }

    public static void LevelCompletedHard(int level)
    {
        Dictionary<string, string> levelCompleted = new Dictionary<string, string>();
        levelCompleted.Add("level" + level.ToString() + "_completed_hard", "1");
        AppsFlyer.trackRichEvent("level" + level.ToString() + "_completed_hard", levelCompleted);

        print("AppsFlyer Hard Mode Level " + level.ToString() + "Event");
    }

    public static void PurchaseSnowflakeCurrency()
    {
        Dictionary<string, string> purchaseSnowflakeCurrency = new Dictionary<string, string>();
        purchaseSnowflakeCurrency.Add("purchase_snowflake_currency", "1");
        AppsFlyer.trackRichEvent("purchase_snowflake_currency", purchaseSnowflakeCurrency);

        print("AppsFlyerMMP: Purchase Snowflake Currency Event");
    }

    public static void MiniGamePlayed()
    {
        Dictionary<string, string> miniGamePlayed = new Dictionary<string, string>();
        miniGamePlayed.Add("minigame_played", "1");
        AppsFlyer.trackRichEvent("minigame_played", miniGamePlayed);

        print("AppsFlyerMMP: Minigame Played Event");
    }

    public static void WarriorUsed()
    {
        Dictionary<string, string> warriorUsed = new Dictionary<string, string>();
        warriorUsed.Add("warrior_used", "1");
        AppsFlyer.trackRichEvent("warrior_used", warriorUsed);

        print("AppsFlyerMMP: Warrior Used Event");
    }

    public static void RewardedAdWatched()
    {
        Dictionary<string, string> rewardedAdWatched = new Dictionary<string, string>();
        rewardedAdWatched.Add("rewarded_ad", "1");
        AppsFlyer.trackRichEvent("rewarded_ad", rewardedAdWatched);

        print("AppsFlyerMMP: Rewarded Ad Event");
    }

    public static void InAppPurchase5Snowflakes()
    {
        Dictionary<string, string> purchaseEvent = new Dictionary<string, string>();
        purchaseEvent.Add("af_currency", "USD");
        purchaseEvent.Add("af_revenue", "0.99");
        purchaseEvent.Add("af_quantity", "1");
        AppsFlyer.trackRichEvent("iap_5snowflakes", purchaseEvent);
        Debug.Log("AppsFlyerMMP: 5 Snowflakes IAP");
    }

    public static void InAppPurchase25Snowflakes()
    {
        Dictionary<string, string> purchaseEvent = new Dictionary<string, string>();
        purchaseEvent.Add("af_currency", "USD");
        purchaseEvent.Add("af_revenue", "2.99");
        purchaseEvent.Add("af_quantity", "1");
        AppsFlyer.trackRichEvent("iap_25snowflakes", purchaseEvent);
        Debug.Log("AppsFlyerMMP: 25 Snowflakes IAP");
    }

    public static void InAppPurchase3Warriors()
    {
        Dictionary<string, string> purchaseEvent = new Dictionary<string, string>();
        purchaseEvent.Add("af_currency", "USD");
        purchaseEvent.Add("af_revenue", "0.99");
        purchaseEvent.Add("af_quantity", "1");
        AppsFlyer.trackRichEvent("iap_3warriors", purchaseEvent);
        Debug.Log("AppsFlyerMMP: 3 heroes IAP");
    }

    public static void InAppPurchase20Warriors()
    {
        Dictionary<string, string> purchaseEvent = new Dictionary<string, string>();
        purchaseEvent.Add("af_currency", "USD");
        purchaseEvent.Add("af_revenue", "4.99");
        purchaseEvent.Add("af_quantity", "1");
        AppsFlyer.trackRichEvent("iap_20warriors", purchaseEvent);
        Debug.Log("AppsFlyerMMP: 20 heroes IAP");
    }

    public static void InAppPurchaseRemoveAds()
    {
        Dictionary<string, string> purchaseEvent = new Dictionary<string, string>();
        purchaseEvent.Add("af_currency", "USD");
        purchaseEvent.Add("af_revenue", "0.99");
        purchaseEvent.Add("af_quantity", "1");
        AppsFlyer.trackRichEvent("iap_removeads", purchaseEvent);
        Debug.Log("AppsFlyerMMP: Remove Ads IAP");
    }
}
