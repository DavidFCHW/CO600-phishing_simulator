using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailBodyScript : MonoBehaviour {

    private Email _email;
    [SerializeField] private GameObject mainContent;
    [SerializeField] private GameObject mainContentWithFeedback;
    public SenderScript senderPanel;
    public GameObject positiveFeedback;
    public GameObject negativeFeedback;

    public void SetEmail(Email email)
    {
        _email = email;
    }

    /*
     * Hide the feedback
     */
     public void HideFeedback()
    {
        positiveFeedback.SetActive(false);
        negativeFeedback.SetActive(false);
    }

    public void ShowPositiveFeedback()
    {
        positiveFeedback.SetActive(true);
    }

    public void ShowNegativeFeedback()
    {
        negativeFeedback.SetActive(true);
    }

    public void DisplayBody()
    {
        mainContent.SetActive(true);
        mainContentWithFeedback.SetActive(false);
    }
    
    public void DisplayFeedback()
    {
        mainContent.SetActive(false);
        mainContentWithFeedback.SetActive(true);
    }
}
