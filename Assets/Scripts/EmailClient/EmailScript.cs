using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EmailScript : MonoBehaviour {
    private GameScript _gameScript;
    // List of emails
    private int[] _phishingEmailsIndexes;
    public EmailPreviewScript[] easyEmailPreviewArray;
    public EmailBodyScript[] easyEmailBodyArray;
    public EmailPreviewScript[] mediumEmailPreviewArray;
    public EmailBodyScript[] mediumEmailBodyArray;
    // MailBoxes
    public MailboxScript inbox;
    public MailboxScript archive;
    public MailboxScript trash;
    // Preview scrollview
    public GameObject previewScrollView;
    // Finished Panel
    public GameObject donePanel;
    // Index of currently selected email
    private Email _currentlySelectedEmail;
    private int _currentlySelectedEmailIndex = 0;
    // Current inbox
    private MailboxScript _currentMailbox;
    // Mailbox hovered on
    private MailboxScript _mailboxHoveredOn = null;
    // Sound effect
    public AudioSource swoosh;

    /*
     * Method called on initialisation
     */
    private void Awake() {
        // Make the preview scrollview the correct height
        RectTransform rectTrans = previewScrollView.GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(rectTrans.sizeDelta.x, easyEmailPreviewArray[0].GetComponent<RectTransform>().rect.height * easyEmailPreviewArray.Length);
        // Set all emails inactive
        foreach (EmailPreviewScript preview in easyEmailPreviewArray) preview.gameObject.SetActive(false);
        foreach (EmailPreviewScript preview in mediumEmailPreviewArray) preview.gameObject.SetActive(false);
        foreach (EmailBodyScript body in easyEmailBodyArray) body.gameObject.SetActive(false);
        foreach (EmailBodyScript body in mediumEmailBodyArray) body.gameObject.SetActive(false);
        // Set the emailscript for all inboxes
        inbox.SetEmailScript(this);
        inbox.InitialiseEmailList();
        archive.SetEmailScript(this);
        archive.InitialiseEmailList();
        trash.SetEmailScript(this);
        trash.InitialiseEmailList();
        // Set current mailbox as inbox
        _currentMailbox = inbox;
        // Set finished panel inactive
        donePanel.SetActive(false);
        // Initialise emails
        if (StaticClass.GetCurrentLevel() == 1) InitialiseEasyEmails();
        else if (StaticClass.GetCurrentLevel() == 2) InitialiseMediumEmails();
        // Shuffle the list
        _currentMailbox.ShuffleEmails();
        // Select current mailbox
        _currentMailbox.Select();
    }

    /*
     * Links the emails together and whatnot
     */
     public void InitialiseEasyEmails()
    {
        // Link together bodies and previews
        for (int i = 0; i < easyEmailPreviewArray.Length; i++)
        {
            // Create email object with preview, body, index and emailScript reference
            Email email = new Email(easyEmailPreviewArray[i], easyEmailBodyArray[i], i, this);
            // Set isPhis
            if (i == 1 || i == 2 || i == 8 || i == 9)
            {
                email.isPhish = true;
            }
            // Give email object reference to the body and preview script
            easyEmailPreviewArray[i].SetEmail(email);
            easyEmailBodyArray[i].SetEmail(email);
            // Initialise said email
            email.Initialise();
            // Add the email to inbox
            _currentMailbox.AddEmail(email);
        }
    }

    /*
     * Links the emails together and whatnot
     */
    public void InitialiseMediumEmails()
    {
        // Link together bodies and previews
        for (int i = 0; i < mediumEmailPreviewArray.Length; i++)
        {
            // Create email object with preview, body, index and emailScript reference
            Email email = new Email(mediumEmailPreviewArray[i], mediumEmailBodyArray[i], i, this);
            // Set isPhis
            if (i == 8 || i == 9 || i == 10 || i == 11)
            {
                email.isPhish = true;
            }
            // Give email object reference to the body and preview script
            mediumEmailPreviewArray[i].SetEmail(email);
            mediumEmailBodyArray[i].SetEmail(email);
            // Initialise said email
            email.Initialise();
            // Add the email to inbox
            _currentMailbox.AddEmail(email);
        }
    }

    public void SetGameScript(GameScript gameScript)
    {
        _gameScript = gameScript;
    }

    /*
     * Reset to try again
     */
     public void ResetEmails()
    {
        // Set finished panel inactive
        donePanel.SetActive(false);
        // Move every mail to inbox
        foreach (Email mail in trash.GetEmails())
        {
            // Add to inbox
            inbox.AddEmail(mail);
        }
        foreach (Email mail in archive.GetEmails())
        {
            // Add to inbox
            inbox.AddEmail(mail);
        }
        // Clear other mailboxes
        trash.InitialiseEmailList();
        archive.InitialiseEmailList();
        // Set all emails inactive
        foreach (Email mail in inbox.GetEmails())
        {
            mail.Reset();
        }
        // Set current mailbox as inbox
        _currentMailbox = inbox;
        // Shuffle the list
        _currentMailbox.ShuffleEmails();
        // Select current mailbox
        _currentMailbox.Select();
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
            if (_currentlySelectedEmailIndex - 1 >= 0)
            {
                _currentMailbox.GetEmails()[_currentlySelectedEmailIndex - 1].Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_currentlySelectedEmailIndex + 1 < _currentMailbox.GetEmails().Count)
            {
                _currentMailbox.GetEmails()[_currentlySelectedEmailIndex + 1].Select();
            }
        }
    }

    /*
     * Set the index of the currently selected email
     * Called when another email is selected
     */
    public void SetSelectedEmail(Email email)
    {
        UnselectEmail(_currentlySelectedEmail);
        _currentlySelectedEmail = email;
        _currentlySelectedEmailIndex = email.index;
    }

    /*
     * Button to end the game is pressed
     */
    public void CheckButtonClicked()
    {
        donePanel.SetActive(false);
        _gameScript.FinishedSortingEmails();
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
        var totalEmailsInt = inbox.GetEmails().Count + trash.GetEmails().Count + archive.GetEmails().Count;
        var phishingEmailsInt = 0;      // The number of phishing emails
        var sortedEmailsInt = 0;        // The number of emails out of inbox total
        var phishingEmailsTrashed = 0;  // The number of phishing emails trashed
        var legitEmailsTrashed = 0;     // The number of Legit emails trashed
        var phishingEmailsArchived = 0; // The number of Phishing emails archived
        var legitEmailsArchived = 0;    // The number of Legit emails archived

        foreach (var mail in inbox.GetEmails()) if (mail.isPhish) phishingEmailsInt++;
        foreach (var mail in trash.GetEmails())
        {
            sortedEmailsInt++;
            if (mail.isPhish)
            {
                phishingEmailsInt++;
                phishingEmailsTrashed++;
            }
            else
            {
                legitEmailsTrashed++;
            }
        }
        foreach (var mail in archive.GetEmails())
        {
            sortedEmailsInt++;
            if (mail.isPhish)
            {
                phishingEmailsArchived++;
                phishingEmailsInt++;
            }
            else
            {
                legitEmailsArchived++;
            }
        }
        return new[] { totalEmailsInt, phishingEmailsInt, sortedEmailsInt, phishingEmailsTrashed, legitEmailsTrashed, phishingEmailsArchived, legitEmailsArchived };
    }

    /*
     * Tag emails based on whether or not they were correct
     */
    public void TagEmails()
    {
        foreach (Email mail in inbox.GetEmails())
        {
            mail.SetEditable(false);
            //mail.TagAsNeutral();
            mail.TagAsIncorrect();
        }
        foreach (Email mail in trash.GetEmails())
        {
            mail.SetEditable(false);
            if (mail.isPhish)
            {
                mail.TagAsCorrect();
            }
            else
            {
                mail.TagAsIncorrect();
            }
        }
        foreach (Email mail in archive.GetEmails())
        {
            mail.SetEditable(false);
            if (mail.isPhish)
            {
                mail.TagAsIncorrect();
            }
            else
            {
                mail.TagAsCorrect();
            }
        }
    }

    public void SetCurrentMailbox(MailboxScript newCurrentMailbox)
    {
        // Unselect currently selected email (otherwise it stays selected in the other mailbox)
        UnselectEmail(_currentlySelectedEmail);
        // Unselect mailbox
        _currentMailbox.Unselect();
        // Keep the ref to the newly selceted mailbox
        _currentMailbox = newCurrentMailbox;
        // We don't have to call select as it's called in mailboxscript onclick()
    }

    public void RemoveEmailFromCurrentMailbox(Email email)
    {
        _currentMailbox.RemoveEmail(email);
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
        _mailboxHoveredOn = mailboxHoveredOn;
    }

    /*
     * Getter for mailboxHoveredOn
     */
    public MailboxScript GetMailboxHoveredOn()
    {
        return _mailboxHoveredOn;
    }

    /*
     * Getter for current mailbox
     */
     public MailboxScript GetCurrentMailbox()
    {
        return _currentMailbox;
    }
}

/*
 * Class that links and email preview to its body
 */
public class Email
{
    // All Value flags
    // Color Flags
    private readonly Color32 _previewClickedOnColor = new Color32(255, 255, 255, 255);
    private readonly Color32 _previewNormalColor = new Color32(255, 255, 255, 100);
    private readonly Color32 _previewHoverColor = new Color32(233, 0, 85, 100);
    private readonly Color32 _previewHoverFeedbackColor = new Color32(255, 255, 255, 255);

    private readonly Color32 _correctColor = new Color32(0, 255, 0, 255);
    private readonly Color32 _correctColorLighter = new Color32(156, 255, 156, 255);

    private readonly Color32 _incorrectColor = new Color32(255, 0, 0, 255);
    private readonly Color32 _incorrectColorLighter = new Color32(255, 139, 139, 255);

    private readonly Color32 _neutralColor = new Color32(0, 144, 255, 255);
    private readonly Color32 _neutralColorLighter = new Color32(90, 182, 253, 255);
    // Scale flags
    private readonly Vector3 _tinyPreviewScale = new Vector3(0.3f, 0.3f, 0.3f);
    private readonly Vector3 _normalPreviewScale = new Vector3(1, 1, 1);
    // Class References
    public int index;
    public bool isPhish;
    public EmailPreviewScript emailPreview;
    public EmailBodyScript emailBody;
    private EmailScript emailScript;
    // Variables
    private bool _unread = true;
    private bool _editable = true; // We can move the mail around and whatnot, blocks dragging and setting as read
    private Color32 _previewClickedOnColorUsed; // Used instead of previewClickedOnColor
    private Color32 _previewNormalColorUsed; // Used instead of previewNormalColor
    private Color32 _previewHoverColorUsed; // Used instead of previewHoverColor

    private bool _showingFeedback;
    // Dragging
    private float halfHeightSmall;      // The half height of the tiny preview
    private Vector3 beforeDragPosition; // The original position of a preview
    // Hovering on preview
    private Color32 previousColor;
    private bool isSelected;
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
        _previewClickedOnColorUsed = _previewClickedOnColor;
        _previewNormalColorUsed = _previewNormalColor;
        _previewHoverColorUsed = _previewHoverColor;
        GameObject gameObject;
        halfHeightSmall = (gameObject = emailPreview.gameObject).GetComponent<RectTransform>().rect.height / 6;
        originalPreviewPosition = gameObject.transform.localPosition;
        // Hide feedback for now
        emailBody.HideFeedback();
        // Make the text bold (cause unread)
        SetUnread();
    }

    /*
     * Show an email as unread
     */
    private void SetUnread()
    {
        _unread = true;
        emailPreview.SetDisplayUnread();
    }

    /*
     * Show an email as read
     */
    private void SetRead()
    {
        _unread = false;
        emailPreview.SetDisplayRead();
    }

    /*
     * Select this email
     * Clicked when preview is clicked on
     */
    public void Select()
    {
        if (_unread && _editable)
        {
            SetRead();
        }
        emailScript.SetSelectedEmail(this);
        // Set selected colour
        emailPreview.gameObject.GetComponent<Image>().color = _previewClickedOnColorUsed;
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
        emailPreview.gameObject.GetComponent<Image>().color = _previewNormalColorUsed;
        // Hide body
        emailBody.gameObject.SetActive(false);
        isSelected = false;
    }

    /*
     * Tag as correct on game end
     */
     public void TagAsCorrect()
    {
        // Change preview color to not mess up on hover
        _previewNormalColorUsed = _correctColorLighter;
        _previewClickedOnColorUsed = _correctColor;
        // Change preview hover color
        _previewHoverColorUsed = _previewHoverFeedbackColor;
        // Show feedback panel
        emailBody.ShowPositiveFeedback();
        // Change preview and body color
        Tag();
    }

    /*
     * Tag as incorrect on game end
     */
    public void TagAsIncorrect()
    {
        // Change preview color to not mess up on hover
        _previewNormalColorUsed = _incorrectColorLighter;
        _previewClickedOnColorUsed = _incorrectColor;
        // Change preview hover color
        _previewHoverColorUsed = _previewHoverFeedbackColor;
        // Show feedback panel
        emailBody.ShowNegativeFeedback();
        // Change preview and body color
        Tag();
    }

    /*
     * Tag as incorrect on game end
     */
    private void TagAsNeutral()
    {
        // Change preview color to not mess up on hover
        _previewNormalColorUsed = _neutralColorLighter;
        _previewClickedOnColorUsed = _neutralColor;
        // Change preview hover color
        _previewHoverColorUsed = _previewHoverFeedbackColor;
        // Show feedback panel
        emailBody.ShowNegativeFeedback();
        // Change preview and body color
        Tag();
    }

    /*
     * Put back to normal
     */
    private void TagAsUntagged()
    {
        // Change preview color to not mess up on hover
        _previewNormalColorUsed = _previewNormalColor;
        _previewClickedOnColorUsed = _previewClickedOnColor;
        // Change preview hover color
        _previewHoverColorUsed = _previewHoverColor;
        // Change preview and body color
        Tag();
    }

    /*
     * Called on try again
     */
     public void Reset()
    {
        emailPreview.gameObject.SetActive(false);
        emailBody.gameObject.SetActive(false);
        // Set editable
        SetEditable(true);
        // Reset colors
        TagAsUntagged();
        // Unselect
        Unselect();
        // Mark as unread
        if (!_unread) SetUnread();
        // Hide feedback
        emailBody.HideFeedback();
    }

    /*
     * Common code between all three tag functions
     */
    private void Tag()
    {
        // Change preview color
        emailPreview.GetComponent<Image>().color = isSelected ? _previewClickedOnColorUsed : _previewNormalColorUsed;
    }

    /*
     * Drag object methods
     */
    public void OnPreviewDrag()
    {
        if (!_editable) return;
        // Recalculate position
        emailPreview.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + halfHeightSmall, 0);
    }

    public void OnBeginPreviewDrag()
    {
        if (!_editable) return;
        // Make the preview small
        var gameObject = emailPreview.gameObject;
        gameObject.transform.localScale = _tinyPreviewScale;
        // Keep track of the original position
        beforeDragPosition = gameObject.transform.position;
    }

    public void OnEndPreviewDrag()
    {
        if (!_editable) return;
        // Return the scale to normal
        emailPreview.gameObject.transform.localScale = _normalPreviewScale;
        // Check if dropped in a mailbox
        var mailboxHoveredOn = emailScript.GetMailboxHoveredOn();
        if (mailboxHoveredOn && emailScript.GetCurrentMailbox() != mailboxHoveredOn)
        {
            // Play swoosh sound
            emailScript.swoosh.Play();
            // Add email to specific mailbox
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
    public void OnPointerEnterPreview()
    {
        emailPreview.gameObject.GetComponent<Image>().color = _previewHoverColorUsed;
    }

    public void OnPointerExitPreview()
    {
        emailPreview.gameObject.GetComponent<Image>().color = isSelected ? _previewClickedOnColorUsed : _previewNormalColorUsed;
    }

    /*
     * Reset preview position
     */
    public void ResetPreviewPosition()
    {
        emailPreview.gameObject.transform.localPosition = originalPreviewPosition;
    }

    /*
     * Block dragging and setting unread/read
     */
     public void SetEditable(bool editable)
    {
        _editable = editable;
    }
}
