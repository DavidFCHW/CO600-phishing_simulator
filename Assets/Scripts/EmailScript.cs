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
    // Index of currently selcted email
    private int currentlySelectedEmailIndex = 0;

    /*
     * Method called on initialisation
     */
    void Start () {
        // Link together bodies and previews
		for (int i = 0; i < emailPreviewArray.Length; i++)
        {
            Email email = new Email(emailPreviewArray[i], emailBodyArray[i], i, this);
            emails.Add(email);
            emailPreviewArray[i].setEmail(email);
            emailBodyArray[i].setEmail(email);
        }
        // Shuffle the list
        //emails = Shuffle(emails);
        // Re-assign the indexes
        for (int i = 0; i < emails.Count; i++)
        {
            emails[i].index = i;
        }
        // Position the emails on screen
        PositionEmailPreviewsScript(emails);
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
     * Move from email to email with up and down arrow keys
     */
    private void CheckIfArrow()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!(emails[currentlySelectedEmailIndex].index - 1 < emails.Count))
            {
                SelectEmail(emails[currentlySelectedEmailIndex].index - 1);
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!(emails[currentlySelectedEmailIndex].index + 1 > emails.Count))
            {
                SelectEmail(emails[currentlySelectedEmailIndex].index + 1);
            }
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
    private Vector3 showBodyScale = new Vector3(1, 1, 1);
    private Vector3 hideBodyScale = new Vector3(1, 0, 1);
    private Vector3 tinyPreviewScale = new Vector3(0.3f, 0.3f, 0.3f);
    private Vector3 normalPreviewScale = new Vector3(1, 1, 1);
    // Class References
    public int index;
    public EmailPreviewScript emailPreview;
    public EmailBodyScript emailBody;
    private EmailScript emailScript;
    // Variables
    // Dragging
    private float halfHeightSmall;    // The half height of the tiny preview
    private Vector3 originalPosition; // The original position of a preview
    // Hovering
    private Color32 previousColor;
    private bool isSelected = false;

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
