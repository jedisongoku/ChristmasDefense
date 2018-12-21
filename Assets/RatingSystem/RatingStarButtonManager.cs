using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles behavior for rating star buttons
/// 
/// Ruben Sanchez
/// 12/21/18
/// </summary>
public class RatingStarButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;

    [SerializeField] private Button submitButton;

    [SerializeField] private int index;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ActivateUpToIndex);
    }

    private void OnEnable()
    {
        submitButton.gameObject.SetActive(false);
        DeactivateUpToIndex(objects.Length - 1);
    }

    public void ActivateUpToIndex()
    {
        if (objects.Length == 0 || index >= objects.Length)
            return;

        submitButton.gameObject.SetActive(true);

        // deactivate all the stars first
        DeactivateUpToIndex(objects.Length - 1);

        // activate up to this index
        for (int i = 0; i <= index; i++)
        {
            objects[i].SetActive(true);
        }

        // update the listener on the submit button
        UpdateSubmitButton();
    }

    private void DeactivateUpToIndex(int index)
    {
        if (objects.Length == 0 || index >= objects.Length)
            return;

        for (int i = 0; i <= index; i++)
        {
            objects[i].SetActive(false);
        }
    }

    private void UpdateSubmitButton()
    {
        // update the current star amount on the Rating Manager
        RatingManager.Instance.StarAmount = index + 1;

        // update the button listener
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(RatingManager.Instance.HandleRating);
    }
}
