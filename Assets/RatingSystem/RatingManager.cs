using UnityEngine;

/// <summary>
/// Handles game rating, pops Rating and response panels, fires off event on rating
/// 
/// Ruben Sanchez
/// 12/19/18
/// </summary>
public class RatingManager : MonoBehaviour
{
    public static RatingManager Instance;

    public delegate void Rating(int starAmount);
    public event Rating OnRating;

    public int StarAmount;

    [SerializeField] private GameObject ratePanel;
    [SerializeField] private GameObject lowRatingResponsePanel;
    [SerializeField] private GameObject highRatingResponsePanel;
    [SerializeField] private int highRatingThreshold = 4;

    [SerializeField] private GameObject[] objectsToDeactivate;

    private string storeLink;

    private void Awake()
    {
        storeLink = "https://play.google.com/store/apps/details?id=" + Application.identifier;
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }

    public void ShowRatingPanel()
    {
        foreach (var o in objectsToDeactivate)
            o.SetActive(false);

        ratePanel.SetActive(true);
    }

    public void HandleRating()
    {
        if (StarAmount >= highRatingThreshold)
            highRatingResponsePanel.SetActive(true);

        else
            lowRatingResponsePanel.SetActive(true);

        if (OnRating != null)
            OnRating(StarAmount);
    }

    public void OpenStoreLink()
    {
        Application.OpenURL(storeLink);
        AppsFlyerMMP.VisitStoreToRate();
    }
}
