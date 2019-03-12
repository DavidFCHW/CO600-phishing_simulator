using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EmailPreviewScript : MonoBehaviour,
    IDragHandler, IBeginDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler
{

    // Associated email object, links preview to body
    private Email email;
    // Read indicator
    public GameObject readIndicator;
    // Text
    public Text sender;
    public Text subject;

    /*
     * Called by the EmailScript, keeps a reference to the associated email object
     */
    public void SetEmail(Email e)
    {
        email = e;
    }

    /*
     * Function called when preview is clicked
     */
    public void OnClick()
    {
        email.Select();
    }

    /*
     * Drag object methods
     */
    public void OnDrag(PointerEventData eventData)
    {
        email.OnPreviewDrag();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        email.OnBeginPreviewDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        email.OnEndPreviewDrag();
    }

    /*
     * Hover over preview methods
     */
    public void OnPointerExit(PointerEventData eventData)
    {
        email.OnPointerExitPreview();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        email.OnPointerEnterPreview();
    }

    public void SetDisplayUnread()
    {
        // Bold subject
        subject.text = "<b>" + subject.text + "</b>";
        // Bold sender
        sender.text = "<b>" + sender.text + "</b>";
        // Make blue dot appear
        readIndicator.SetActive(true);
    }

    public void SetDisplayRead()
    {
        // Un-bold sender
        sender.text = sender.text.Substring(3, sender.text.Length - 7);
        // Un-bold subject
        subject.text = subject.text.Substring(3, subject.text.Length - 7);
        // Make blue dot disappear
        readIndicator.SetActive(false);
    }
}
