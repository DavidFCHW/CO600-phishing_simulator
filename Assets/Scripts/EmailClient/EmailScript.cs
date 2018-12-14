using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EmailScript : MonoBehaviour {
    // List of emails
    List<Email> emails = new List<Email>();
    public EmailPreviewScript[] emailPreviewArray;
    public EmailBodyScript[] emailBodyArray;
    // MailBoxes
    public MailboxScript inbox;
    public MailboxScript archive;
    public MailboxScript trash;
    // Finished Panel
    public GameObject finishedPanel;
    // Index of currently selcted email
    private int currentlySelectedEmailIndex = 0;

    /*
     * Method called on initialisation
     */
    void Start () {
        // Set finished panel inactive
        finishedPanel.SetActive(false);
        // Link together bodies and previews
		for (int i = 0; i < emailPreviewArray.Length; i++)
        {
            Email email = new Email(emailPreviewArray[i], emailBodyArray[i], i, this);
            emails.Add(email);
            emailPreviewArray[i].setEmail(email);
            emailBodyArray[i].setEmail(email);
        }
        // Assign the isPhishing variables
        AssignIsPhishing();
        // Shuffle the list
        emails = Shuffle(emails);
        // Re-assign the indexes
        AssignEmailIndexes();
        // Position the emails on screen
        PositionEmailPreviewsScript(emails);
    }

    /*
     * Sets every mail as phishing or legit
     */
     void AssignIsPhishing()
    {
        emails[2].isPhish = true;
        emails[3].isPhish = true;
    }

    /*
     * Assigns indexes to the emails in the order in which they appear on screen
     */
    private void AssignEmailIndexes()
    {
        for (int i = 0; i < emails.Count; i++)
        {
            emails[i].index = i;
        }
    }

    /*
     * Method called once per frame
     */
    private void Update()
    {
        CheckIfArrow();
    }

    /*
     * Positions the emails on top of eachother in the window
     */
    private void PositionEmailPreviewsScript(List<Email> emailList)
    {
        float distanceToTop = 0;
        for (int i = 0; i < emailList.Count; i++)
        {
            emailList[i].emailPreview.gameObject.transform.localPosition = new Vector3(
                emailList[i].emailPreview.gameObject.transform.localPosition.x,
                emailList[i].emailPreview.gameObject.transform.localPosition.y - distanceToTop,
                emailList[i].emailPreview.gameObject.transform.localPosition.z
            );
            distanceToTop += emailList[i].emailPreview.gameObject.GetComponent<RectTransform>().rect.height;
        }
    }

    /*
     * Reposition emails when one preview has been removed
     */
    private void RePositionEmailPreviewsScript(List<Email> emailList, int index)
    {
        for (int i = index; i < emailList.Count; i++)
        {
            emailList[i].emailPreview.gameObject.transform.localPosition = new Vector3(
                emailList[i].emailPreview.gameObject.transform.localPosition.x,
                emailList[i].emailPreview.gameObject.transform.localPosition.y + emailList[i].emailPreview.gameObject.GetComponent<RectTransform>().rect.height,
                emailList[i].emailPreview.gameObject.transform.localPosition.z
            );
        }
    }

    /*
     * Move from email to email with up and down arrow keys
     */
    private void CheckIfArrow()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentlySelectedEmailIndex - 1 >= 0)
            {
                SelectEmail(emails[currentlySelectedEmailIndex - 1].index);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentlySelectedEmailIndex + 1 < emails.Count)
            {
                SelectEmail(emails[currentlySelectedEmailIndex + 1].index);
            }
        }
    }

    /*
     * Remove an email from the list of emails
     */
     public void RemoveEmail(Email emailToRemove)
    {
        // Remove email from array
        emails.Remove(emailToRemove);
        // Remove email from view
        emailToRemove.emailPreview.gameObject.SetActive(false);
        //emailToRemove.emailPreview.transform.localScale;

        // Check if there are any more emails
        if (emails.Count <= 0)
        {
            finishedPanel.SetActive(true);
        }
        else
        {
            // Re-assign the indexes
            AssignEmailIndexes();
            // Re-position the emails on screen
            RePositionEmailPreviewsScript(emails, emailToRemove.index);
        }
    }

    /*
     * Set a specific email as selected and unselect the previously selected email
     */
    private void SelectEmail(int index)
    {
        emails[currentlySelectedEmailIndex].Unselect();
        emails[index].Select();
        currentlySelectedEmailIndex = index;
    }

    /*
     * Set the index of the currently selected email
     */
    public void SetSelectedEmail(int index)
    {
        emails[currentlySelectedEmailIndex].Unselect();
        currentlySelectedEmailIndex = index;
    }

    /*
     * Shuffle a list's items
     */
    private List<T> Shuffle<T>(List<T> list)
    {
        System.Random rnd = new System.Random();
        int[] arr = new int[3];
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    /*
     * Button to end the game is pressed
     */
    public void CheckButtonClicked()
    {
        CheckCorrectEmails();
    }

    /*
     * Check if the mail was in the correct inbox
     */
     private void CheckCorrectEmails()
    {
        int correct = 0;
        int incorrect = 0;
        foreach (Email mail in trash.GetEmails())
        {
            if (mail.isPhish)
            {
                correct++;
            }
            else
            {
                incorrect++;
            }
        }
        foreach (Email mail in archive.GetEmails())
        {
            if (!mail.isPhish)
            {
                correct++;
            }
            else
            {
                incorrect++;
            }
        }
        Debug.Log("Correct emails: " + correct);
        Debug.Log("Incorrect emails: " + incorrect);
    }
}

/*
 * Class that links and email preview to its body
 */
public class Email
{
    // All Value flags
    // Color Flags
    private Color32 previewClickedOnColor = new Color32(255, 255, 255, 255);
    private Color32 previewNormalColor = new Color32(255, 255, 255, 100);
    private Color32 previewHoverColor = new Color32(233, 0, 85, 100);
    private Color32 mailboxPreviewHoverColor = new Color32(233, 0, 85, 71);
    private Color32 mailboxNormalColor = new Color32(233, 0, 85, 0);
    // Scale flags
    private Vector3 showBodyScale = new Vector3(1, 1, 1);
    private Vector3 hideBodyScale = new Vector3(1, 0, 1);
    private Vector3 hidePreviewScale = new Vector3(1, 0, 1);
    private Vector3 tinyPreviewScale = new Vector3(0.3f, 0.3f, 0.3f);
    private Vector3 normalPreviewScale = new Vector3(1, 1, 1);
    // Class References
    public int index;
    public bool isPhish = false;
    public bool isCorrect = true; // Used at the end to see if in correct inbox
    public EmailPreviewScript emailPreview;
    public EmailBodyScript emailBody;
    private EmailScript emailScript;
    // Variables
    // Dragging
    private float halfHeightSmall;    // The half height of the tiny preview
    private Vector3 originalPosition; // The original position of a preview
    // Hovering on preview
    private Color32 previousColor;
    private bool isSelected = false;
    // Hovering on mailbox
    private MailboxScript mailboxHoveredOn = null;

    /*
     * Constructor
     */
    public Email(EmailPreviewScript emailPreview, EmailBodyScript emailBody, int index, EmailScript emailScript)
    {
        this.emailBody = emailBody;
        this.emailPreview = emailPreview;
        this.index = index;
        this.emailScript = emailScript;
    }

    /*
     * Initialise values
     */
    public void Start()
    {
        halfHeightSmall = emailPreview.gameObject.GetComponent<RectTransform>().rect.height / 6;
    }

    /*
     * Select this email
     */
    public void Select()
    {
        emailScript.SetSelectedEmail(index);
        // Set selected colour
        ChangeColor(emailPreview.gameObject, previewClickedOnColor);
        // Show body
        ChangeScale(emailBody.gameObject, showBodyScale);
        isSelected = true;
    }

    /*
     * Unselect this email
     */
    public void Unselect()
    {
        // Reset to normal colour
        ChangeColor(emailPreview.gameObject, previewNormalColor);
        // Hide body
        ChangeScale(emailBody.gameObject, hideBodyScale);
        isSelected = false;
    }

    /*
     * Drag object methods
     */
    public void OnPreviewDrag(PointerEventData eventData)
    {
        // Recalculate position
        emailPreview.GetComponent<Rigidbody2D>().position = new Vector3(Input.mousePosition.x, Input.mousePosition.y+ + halfHeightSmall, 0);
    }

    public void OnBeginPreviewDrag(PointerEventData eventData)
    {
        // Make the preview small
        ChangeScale(emailPreview.gameObject, tinyPreviewScale);
        // Keep track of the original position
        originalPosition = GetPosition(emailPreview.gameObject);
    }

    public void OnEndPreviewDrag(PointerEventData eventData)
    {
        // Return the scale to normal
        ChangeScale(emailPreview.gameObject, normalPreviewScale);
        // Return to original position
        emailPreview.gameObject.GetComponent<Rigidbody2D>().position = originalPosition;
    }

    /*
     * Hover over preview methods
     */
    public void OnPointerEnterPreview(PointerEventData eventData)
    {
        ChangeColor(emailPreview.gameObject, previewHoverColor);
    }
    
    public void OnPointerExitPreview(PointerEventData eventData)
    {
        if (isSelected)
        {
            ChangeColor(emailPreview.gameObject, previewClickedOnColor);
        }
        else
        {
            ChangeColor(emailPreview.gameObject, previewNormalColor);
        }
    }

    /*
     * Move preview to inbox methods
     */
    public void OnPreviewEnterMailbox(Collider2D collision)
    {
        mailboxHoveredOn = collision.gameObject.GetComponent<MailboxScript>();
        if (mailboxHoveredOn != null)
        {
            ChangeColor(mailboxHoveredOn.gameObject, mailboxPreviewHoverColor);
        }
    }

    public void OnPreviewExitMailbox(Collider2D collision)
    {
        if (mailboxHoveredOn != null)
        {
            ChangeColor(mailboxHoveredOn.gameObject, mailboxNormalColor);
            mailboxHoveredOn = null;
        }
    }

    public void OnEmailPreviewRelease(PointerEventData eventData)
    {
        if (mailboxHoveredOn != null)
        {
            mailboxHoveredOn.addEmail(this);
            // Hide body
            ChangeScale(emailBody.gameObject, hideBodyScale);
            // Hide preview
            ChangeScale(emailPreview.gameObject, hidePreviewScale);
            // Remove from List
            emailScript.RemoveEmail(this);
        }
    }

    /*
     * Helper methods
     */
    private void ChangeColor(GameObject gameobject, Color32 newColor)
    {
        gameobject.GetComponent<Image>().color = newColor;
    }

    private void ChangeScale(GameObject gameobject, Vector3 newScale)
    {
        gameobject.transform.localScale = newScale;
    }

    private Vector3 GetPosition(GameObject gameObject)
    {
        return new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
