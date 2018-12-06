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
    private int counter = 0;
    public Text counterText;

    public HashSet<GameObject> emails;

    public void Start()
    {
        emails = new HashSet<GameObject>();
    }

    public void addEmail(GameObject email)
    {
        emails.Add(email);
        incrementCounter();
        printEmails();
    }

    public void printEmails()
    {
        Debug.Log("Emails:");
        foreach (GameObject email in emails)
        {
            Debug.Log(email.name);
        }
    }

    public void incrementCounter()
    {
        counter++;
        counterText.text = counter.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = mailBoxHoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = mailBoxNormalColor;
    }
}
