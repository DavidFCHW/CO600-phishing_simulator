using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmailPreviewScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    // Associated email object, links preview to body
    private Email email;

    /*
     * Called by the EmailScript, keeps a reference to the associated email object
     */
    public void setEmail(Email email)
    {
        this.email = email;
    }

    /*
     * Function called when preview is clicked
     */
    public void onClick()
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
