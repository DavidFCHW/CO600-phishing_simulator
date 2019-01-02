using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailBodyScript : MonoBehaviour {

    private Email email;
    public string sender;
    public string address;
    public Text senderBox;

    public void SetEmail(Email email)
    {
        this.email = email;
    }
}
