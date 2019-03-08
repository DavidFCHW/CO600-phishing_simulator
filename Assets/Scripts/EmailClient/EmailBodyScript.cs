using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailBodyScript : MonoBehaviour {

    private Email _email;
    [SerializeField] private GameObject mainContent;
    [SerializeField] private GameObject mainContentWithFeedback;
    [SerializeField] private SenderScript senderPanel;
    [SerializeField] private GameObject objectTextBox;
    [SerializeField] private GameObject objectWithFeedback;
    [SerializeField] private GameObject positiveFeedback;
    [SerializeField] private GameObject negativeFeedback;

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
        DisplayFeedback(false);
    }

    public void ShowPositiveFeedback()
    {
        positiveFeedback.SetActive(true);
        DisplayFeedback(true);
    }

    public void ShowNegativeFeedback()
    {
        negativeFeedback.SetActive(true);
        DisplayFeedback(true);
    }
    
    private void DisplayFeedback(bool displayFeedback)
    {
        // Set object to feedback object
        objectTextBox.SetActive(!displayFeedback);
        objectWithFeedback.SetActive(displayFeedback);
        // Set body to feedback body
        mainContent.SetActive(!displayFeedback);
        mainContentWithFeedback.SetActive(displayFeedback);
        // Set the sender to sender with feedback and block it
        if (displayFeedback) BlockSenderPanel();
        else UnBlockSenderPanel();
    }

    public void UnBlockSenderPanel()
    {
        senderPanel.UnBlock();
    }

    public void BlockSenderPanel()
    {
        senderPanel.UnBlock();
    }
}
