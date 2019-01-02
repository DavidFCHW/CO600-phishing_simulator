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

    private void Start()
    {
        thisComponent = this.GetComponent<Text>();
        thisComponent.text = senderText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        thisComponent.text = senderText + " " + addressText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        thisComponent.text = senderText;
    }
}
