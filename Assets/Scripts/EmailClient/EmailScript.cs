using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EmailScript : MonoBehaviour {
    private GameScript gameScript;
    // List of emails
    private int[] phishingEmailsIndexes;
    public EmailPreviewScript[] emailPreviewArray;
    public EmailBodyScript[] emailBodyArray;
    // MailBoxes
    public MailboxScript inbox;
    public MailboxScript archive;
    public MailboxScript trash;
    // Preview scrollview
    public GameObject previewScrollView;
    // Finished Panel
    public GameObject donePanel;
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
    void Awake() {
        // Make the preview scrollview the correct height
        RectTransform rectTrans = previewScrollView.GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(rectTrans.sizeDelta.x, emailPreviewArray[0].GetComponent<RectTransform>().rect.height * emailPreviewArray.Length);
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
        donePanel.SetActive(false);
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
            // Create email object with preview, body, index and emailscript reference
            Email email = new Email(emailPreviewArray[i], emailBodyArray[i], i, this);
            // Set isPhis
            if (i==2 || i==3)
            {
                email.isPhish = true;
            }
            // Give email object reference to the body and preview script
            emailPreviewArray[i].SetEmail(email);
            emailBodyArray[i].SetEmail(email);
            // Initialise said email
            email.Initialise();
            // Add the email to inbox
            currentMailbox.AddEmail(email);
        }
        // Shuffle the list
        currentMailbox.ShuffleEmails();
        // Select current mailbox
        currentMailbox.Select();
    }

    public void SetGameScript(GameScript gameScript)
    {
        this.gameScript = gameScript;
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
        Destroy(donePanel);
        gameScript.FinishedSortingEmails();
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
    public int[] CheckEmails()
    {
        int totalEmailsInt = inbox.GetEmails().Count + trash.GetEmails().Count + archive.GetEmails().Count;
        int phishingEmailsInt = 0;
        int sortedEmailsInt = 0;
        int correctlyIdentifiedInt = 0;
        int wronglyTrashedInt = 0;

        foreach (Email mail in inbox.GetEmails())
        {
            mail.TagAsNeutral();
            if (mail.isPhish) phishingEmailsInt++;
        }
        foreach (Email mail in trash.GetEmails())
        {
            sortedEmailsInt++;
            if (mail.isPhish)
            {
                mail.TagAsCorrect();
                phishingEmailsInt++;
                correctlyIdentifiedInt++;
            }
            else
            {
                mail.TagAsIncorrect();
                wronglyTrashedInt++;
            }
        }
        foreach (Email mail in archive.GetEmails())
        {
            sortedEmailsInt++;
            if (mail.isPhish)
            {
                mail.TagAsIncorrect();
                phishingEmailsInt++;
            }
            else
            {
                mail.TagAsCorrect();
            }
        }
        return new int[] { totalEmailsInt, phishingEmailsInt, sortedEmailsInt, correctlyIdentifiedInt, wronglyTrashedInt };
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
            donePanel.SetActive(true);
        }
        else
        {
            donePanel.SetActive(false);
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

    private Color32 correctColor = new Color32(0, 255, 0, 255);
    private Color32 correctColorLighter = new Color32(156, 255, 156, 255);

    private Color32 incorrectColor = new Color32(255, 0, 0, 255);
    private Color32 incorrectColorLighter = new Color32(255, 139, 139, 255);

    private Color32 neutralColor = new Color32(0, 144, 255, 255);
    private Color32 neutralColorLighter = new Color32(90, 182, 253, 255);
    // Scale flags
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
    private bool unread = true;
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
    public void Initialise()
    {
        halfHeightSmall = emailPreview.gameObject.GetComponent<RectTransform>().rect.height / 6;
        originalPreviewPosition = emailPreview.gameObject.transform.localPosition;
        // Make the text bold (cause unread)
        emailPreview.SetDisplayUnread();
    }

    /*
     * Select this email
     * Clicked when preview is clicked on
     */
    public void Select()
    {
        if (unread)
        {
            unread = false;
            emailPreview.SetDisplayRead();
        }
        emailScript.SetSelectedEmail(this);
        // Set selected colour
        emailPreview.gameObject.GetComponent<Image>().color = previewClickedOnColor;
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
        emailPreview.gameObject.GetComponent<Image>().color = previewNormalColor;
        // Hide body
        emailBody.gameObject.SetActive(false);
        isSelected = false;
    }

    /*
     * Tag as correct on game end
     */
     public void TagAsCorrect()
    {
        // Make body green
        emailBody.GetComponent<Image>().color = correctColorLighter;
        // Make preview green
        emailPreview.GetComponent<Image>().color = correctColor;
        // Change preview color to not mess up on hover
        previewNormalColor = correctColor;
        previewClickedOnColor = correctColorLighter;
    }

    /*
     * Tag as incorrect on game end
     */
    public void TagAsIncorrect()
    {
        // Make body red
        emailBody.GetComponent<Image>().color = incorrectColorLighter;
        // Make preview red
        emailPreview.GetComponent<Image>().color = incorrectColor;
        // Change preview color to not mess up on hover
        previewNormalColor = incorrectColor;
        previewClickedOnColor = incorrectColorLighter;
    }

    /*
     * Tag as incorrect on game end
     */
    public void TagAsNeutral()
    {
        // Make body red
        emailBody.GetComponent<Image>().color = neutralColorLighter;
        // Make preview red
        emailPreview.GetComponent<Image>().color = neutralColor;
        // Change preview color to not mess up on hover
        previewNormalColor = neutralColor;
        previewClickedOnColor = neutralColorLighter;
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
        emailPreview.gameObject.transform.localScale = tinyPreviewScale;
        // Keep track of the original position
        beforeDragPosition = emailPreview.gameObject.transform.position;
    }

    public void OnEndPreviewDrag(PointerEventData eventData)
    {
        // Return the scale to normal
        emailPreview.gameObject.transform.localScale = normalPreviewScale;
        // Check if dropped in a mailbox
        MailboxScript mailboxHoveredOn = emailScript.GetMailboxHoveredOn();
        if (mailboxHoveredOn && emailScript.GetCurrentMailbox() != mailboxHoveredOn)
        {
            mailboxHoveredOn.AddEmail(this);
            // Hide body
            emailBody.gameObject.SetActive(false);
            // Hide preview
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
        emailPreview.gameObject.GetComponent<Image>().color = previewHoverColor;
    }

    public void OnPointerExitPreview(PointerEventData eventData)
    {
        emailPreview.gameObject.GetComponent<Image>().color = (isSelected ? previewClickedOnColor : previewNormalColor);
    }

    /*
     * Reset preview position
     */
    public void ResetPreviewPosition()
    {
        emailPreview.gameObject.transform.localPosition = originalPreviewPosition;
    }
}
