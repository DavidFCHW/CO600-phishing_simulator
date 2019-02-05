using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SenderScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string senderText;
    public string addressText;
    private Text thisComponent;
    private bool blocked = false;

    private void Awake()
    {
        thisComponent = this.GetComponent<Text>();
        thisComponent.text = senderText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!blocked) thisComponent.text = senderText + " " + addressText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!blocked) thisComponent.text = senderText;
    }

    public void Block()
    {
        blocked = true;
        thisComponent.text = senderText + " " + addressText;
    }

    public void UnBlock()
    {
        blocked = false;
        thisComponent.text = senderText;
    }
}
