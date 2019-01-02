using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmailPreviewScript : MonoBehaviour,
    IDragHandler, IBeginDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler
{

    // Associated email object, links preview to body
    private Email email;
    // Read indicator
    public GameObject readIndicator;

    /*
     * Called by the EmailScript, keeps a reference to the associated email object
     */
    public void SetEmail(Email email)
    {
        this.email = email;
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
        email.OnPreviewDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        email.OnBeginPreviewDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        email.OnEndPreviewDrag(eventData);
    }

    /*
     * Hover over preview methods
     */
    public void OnPointerExit(PointerEventData eventData)
    {
        email.OnPointerExitPreview(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        email.OnPointerEnterPreview(eventData);
    }
}
