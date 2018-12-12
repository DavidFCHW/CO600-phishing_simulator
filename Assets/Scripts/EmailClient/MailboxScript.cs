using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MailboxScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // File constants
    private Color32 mailBoxHoverColor = new Color32(139, 139, 139, 71);
    private Color32 mailBoxNormalColor = new Color32(139, 139, 139, 0);
    // Variables
    private int counter = 0;
    public Text counterText;
    // The emails inside this mailbox
    private List<Email> emails;

    public void Start()
    {
        emails = new List<Email>();
    }

    public List<Email> GetEmails()
    {
        return emails;
    }

    public void addEmail(Email email)
    {
        emails.Add(email);
        IncrementCounter();
        PrintEmails();
    }

    public void PrintEmails()
    {
        //Debug.Log("Emails:");
        //foreach (Email email in emails)
        //{
        //    Debug.Log(email);
        //}
    }

    public void IncrementCounter()
    {
        counter++;
        counterText.text = counter.ToString();
    }

    /*
     * Change color on hover
     */
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = mailBoxHoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = mailBoxNormalColor;
    }
}
