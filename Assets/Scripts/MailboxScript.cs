using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MailboxScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public HashSet<GameObject> emails;

    public void Start()
    {
        emails = new HashSet<GameObject>();
    }

    //public HashSet<GameObject> getEmails() {
    //    return emails;
    //}

    public void addEmail(GameObject email)
    {
        emails.Add(email);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color32(139, 139, 139, 71);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color32(139, 139, 139, 0);
    }
}
