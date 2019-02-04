using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailBodyScript : MonoBehaviour {

    private Email email;
    public string sender;
    public string address;
    public Text senderBox;
    public GameObject positiveFeedback;
    public GameObject negativeFeedback;

    public void SetEmail(Email email)
    {
        this.email = email;
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
}
