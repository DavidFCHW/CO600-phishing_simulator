using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EmailScript : MonoBehaviour {
    // List of emails
    private int[] phishingEmailsIndexes;
    public EmailPreviewScript[] emailPreviewArray;
    public EmailBodyScript[] emailBodyArray;
    // MailBoxes
    public MailboxScript inbox;
    public MailboxScript archive;
    public MailboxScript trash;
    // Timer
    public TimerScript timer;
    // Finished Panel
    public GameObject finishedPanel;
    // Index of currently selcted email
    private Email currentlySelectedEmail;
    private int currentlySelectedEmailIndex = 0;
    // Current inbox
    private MailboxScript currentMailbox;
    // Mailbox hovered on
    private MailboxScript mailboxHoveredOn = null;

    /*
     * Method called on initialisation
     */
    void Start () {
        // Set the emailscript for all inboxes
        inbox.SetEmailScript(this);
        inbox.InitialiseEmailList();
        archive.SetEmailScript(this);
        archive.InitialiseEmailList();
        trash.SetEmailScript(this);
        trash.InitialiseEmailList();
        // Set current mailbox as inbox
        currentMailbox = inbox;
        // Set finished panel inactive
        finishedPanel.SetActive(false);
        // Set all email previews inactive
        foreach (EmailPreviewScript prev in emailPreviewArray) {
            prev.gameObject.SetActive(false);
        }
        // Set all email bodies inactive
        foreach (EmailBodyScript body in emailBodyArray)
        {
            body.gameObject.SetActive(false);
        }
        // Link together bodies and previews
        for (int i = 0; i < emailPreviewArray.Length; i++)
        {
            Email email = new Email(emailPreviewArray[i], emailBodyArray[i], i, this);
            if (i==2 || i==3)
            {
                email.isPhish = true;
            }
            emailPreviewArray[i].SetEmail(email);
            emailBodyArray[i].SetEmail(email);
            currentMailbox.AddEmail(email);
        }
        // Shuffle the list
        currentMailbox.ShuffleEmails();
        // Select current mailbox
        currentMailbox.Select();
    }

    /*
     * Method called once per frame
     */
    private void Update()
    {
        CheckIfArrow();
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
                currentMailbox.GetEmails()[currentlySelectedEmailIndex - 1].Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentlySelectedEmailIndex + 1 < currentMailbox.GetEmails().Count)
            {
                currentMailbox.GetEmails()[currentlySelectedEmailIndex + 1].Select();
            }
        }
    }

    /*
     * Set the index of the currently selected email
     * Called when another email is selected
     */
    public void SetSelectedEmail(Email email)
    {
        UnselectEmail(currentlySelectedEmail);
        currentlySelectedEmail = email;
        currentlySelectedEmailIndex = email.index;
    }

    /*
     * Button to end the game is pressed
     */
    public void CheckButtonClicked()
    {
        CheckCorrectEmails();
    }

    /*
     * Unselect email
     */
     public void UnselectEmail(Email email)
    {
        if (email != null) email.Unselect();
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

    public void SetCurrentMailbox(MailboxScript newCurrentMailbox)
    {
        // Unselect currently selected email (otherwise it stays selected in the other mailbox)
        UnselectEmail(currentlySelectedEmail);
        // Unselect mailbox
        currentMailbox.Unselect();
        // Keep the ref to the newly selceted mailbox
        currentMailbox = newCurrentMailbox;
        // We don't have to call select as it's called in mailboxscript onclick()
    }

    public void RemoveEmailFromCurrentMailbox(Email email)
    {
        currentMailbox.RemoveEmail(email);
        // Check if there are any more emails in the inbox
        if (inbox.GetEmails().Count <= 0)
        {
            finishedPanel.SetActive(true);
        }
    }

    /*
     * Set mailbox hovered on
     */
    public void SetHoveredOn(MailboxScript mailboxHoveredOn)
    {
        this.mailboxHoveredOn = mailboxHoveredOn;
    }

    /*
     * Getter for mailboxHoveredOn
     */
    public MailboxScript GetMailboxHoveredOn()
    {
        return mailboxHoveredOn;
    }

    /*
     * Getter for current mailbox
     */
     public MailboxScript GetCurrentMailbox()
    {
        return currentMailbox;
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
    // Scale flags
    //private Vector3 showBodyScale = new Vector3(1, 1, 1);
    //private Vector3 hideBodyScale = new Vector3(1, 0, 1);
    //private Vector3 hidePreviewScale = new Vector3(1, 0, 1);
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
    private float halfHeightSmall;      // The half height of the tiny preview
    private Vector3 beforeDragPosition; // The original position of a preview
    // Hovering on preview
    private Color32 previousColor;
    private bool isSelected = false;
    // Preview placements
    public Vector3 originalPreviewPosition; // The position of the preview before anything happens

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
        originalPreviewPosition = emailPreview.gameObject.transform.localPosition;
    }

    /*
     * Select this email
     * Clicked when preview is clicked on
     */
    public void Select()
    {
        emailScript.SetSelectedEmail(this);
        // Set selected colour
        ChangeColor(emailPreview.gameObject, previewClickedOnColor);
        // Show body
        emailBody.gameObject.SetActive(true);
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
        emailBody.gameObject.SetActive(false);
        isSelected = false;
    }

    /*
     * Drag object methods
     */
    public void OnPreviewDrag(PointerEventData eventData)
    {
        // Recalculate position
        emailPreview.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + halfHeightSmall, 0);
    }

    public void OnBeginPreviewDrag(PointerEventData eventData)
    {
        // Make the preview small
        ChangeScale(emailPreview.gameObject, tinyPreviewScale);
        // Keep track of the original position
        beforeDragPosition = emailPreview.gameObject.transform.position;
    }

    public void OnEndPreviewDrag(PointerEventData eventData)
    {
        // Return the scale to normal
        ChangeScale(emailPreview.gameObject, normalPreviewScale);
        // Check if dropped in a mailbox
        MailboxScript mailboxHoveredOn = emailScript.GetMailboxHoveredOn();
        if (mailboxHoveredOn && emailScript.GetCurrentMailbox() != mailboxHoveredOn)
        {
            mailboxHoveredOn.AddEmail(this);
            // Hide body
            //ChangeScale(emailBody.gameObject, hideBodyScale);
            emailBody.gameObject.SetActive(false);
            // Hide preview
            //ChangeScale(emailPreview.gameObject, hidePreviewScale);
            emailPreview.gameObject.SetActive(false);
            // Remove from List
            emailScript.RemoveEmailFromCurrentMailbox(this);
        }
        else
        {
            // Return to original position
            emailPreview.gameObject.transform.position = beforeDragPosition;
        }
    }

    /*
     * Hover over preview
     */
    public void OnPointerEnterPreview(PointerEventData eventData)
    {
        ChangeColor(emailPreview.gameObject, previewHoverColor);
    }

    public void OnPointerExitPreview(PointerEventData eventData)
    {
        ChangeColor(emailPreview.gameObject, (isSelected ? previewClickedOnColor : previewNormalColor));
    }

    /*
     * Reset preview position
     */
    public void ResetPreviewPosition()
    {
        emailPreview.gameObject.transform.localPosition = originalPreviewPosition;
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
}
