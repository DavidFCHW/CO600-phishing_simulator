using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, 
    IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler, 
    IPointerEnterHandler, IPointerExitHandler
{
    // File constants
    private Color32 normalColor = new Color32(255, 255, 255, 100);
    private Color32 hoverColor = new Color32(233, 0, 85, 100);
    private Color32 emailClickedOnColor = new Color32(255, 255, 255, 255);
    private Color32 mailBoxHoverEnterColor = new Color32(233, 0, 85, 71);
    private Color32 mailBoxHoverExitColor = new Color32(233, 0, 85, 0);
    private Vector3 tinyPreviewScale = new Vector3(0.3f, 0.3f, 0.3f);
    private Vector3 normalPreviewScale = new Vector3(1, 1, 1);

    //private Vector3 offset;
    private Vector3 originalPosition;
    private float halfHeightSmall;
    public MailboxScript inbox;
    public MailboxScript sent;
    public MailboxScript trash;
    private MailboxScript hoveringOn = null;
    private Color32 previousColor;

    private void Awake()
    {
        halfHeightSmall = gameObject.GetComponent<RectTransform>().rect.height / 6;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //if (hoveringOn != null)
        //{
        //    hoveringOn.addEmail(this.gameObject);
        //    Destroy(gameObject);
        //}
    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = new Vector3(Input.mousePosition.x - offset.x, Input.mousePosition.y - offset.y, 0);
        this.GetComponent<Rigidbody2D>().position = new Vector3(Input.mousePosition.x, Input.mousePosition.y+ + halfHeightSmall, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.localScale = tinyPreviewScale;
        //if (!halfHeightSmallSet)
        //{
        //    halfHeightSmall = gameObject.GetComponent<RectTransform>().rect.height/2;
        //}
        //offset = new Vector3(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y);
        originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localScale = normalPreviewScale;
        GetComponent<Rigidbody2D>().position = originalPosition;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals(trash.name))
        {
            hoveringOn = trash;
            trash.gameObject.GetComponent<Image>().color = mailBoxHoverEnterColor;
        }
        else if (collision.gameObject.name.Equals(inbox.name))
        {
            hoveringOn = inbox;
            inbox.gameObject.GetComponent<Image>().color = mailBoxHoverEnterColor;
        }
        else if (collision.gameObject.name.Equals(sent.name))
        {
            hoveringOn = sent;
            sent.gameObject.GetComponent<Image>().color = mailBoxHoverEnterColor;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (hoveringOn != null)
        {
            hoveringOn.gameObject.GetComponent<Image>().color = mailBoxHoverExitColor;
        }
        hoveringOn = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        previousColor = gameObject.GetComponent<Image>().color;
        gameObject.GetComponent<Image>().color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = previousColor;
    }

    public void clickOn()
    {
        previousColor = emailClickedOnColor;
    }
}