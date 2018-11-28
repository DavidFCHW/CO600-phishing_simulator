using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    //private Vector3 offset;
    private Vector3 originalPosition;
    public MailboxScript inbox;
    public MailboxScript sent;
    public MailboxScript trash;
    private MailboxScript hoveringOn = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        //offset = new Vector3(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y);
        //originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (hoveringOn != null)
        {
            hoveringOn.addEmail(this.gameObject);
            Destroy(gameObject);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = new Vector3(Input.mousePosition.x - offset.x, Input.mousePosition.y - offset.y, 0);
        this.GetComponent<Rigidbody2D>().position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        //offset = new Vector3(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y);
        originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Rigidbody2D>().position = originalPosition;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals(trash.name))
        {
            hoveringOn = trash;
            trash.gameObject.GetComponent<Image>().color = new Color32(233, 0, 85, 71);
        }
        else if (collision.gameObject.name.Equals(inbox.name))
        {
            hoveringOn = inbox;
            inbox.gameObject.GetComponent<Image>().color = new Color32(233, 0, 85, 71);
        }
        else if (collision.gameObject.name.Equals(sent.name))
        {
            hoveringOn = sent;
            sent.gameObject.GetComponent<Image>().color = new Color32(233, 0, 85, 71);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (hoveringOn != null)
        {
            hoveringOn.gameObject.GetComponent<Image>().color = new Color32(233, 0, 85, 0);
        }
        hoveringOn = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("HERE 1");
        gameObject.GetComponent<Image>().color = new Color32(233, 0, 85, 100);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("HERE 2");
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
    }
}