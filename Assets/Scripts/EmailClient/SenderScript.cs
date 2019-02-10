using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SenderScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string senderText;
    public string addressText;
    private Text _thisComponent;
    private bool _blocked;

    private void Awake()
    {
        _thisComponent = GetComponent<Text>();
        _thisComponent.text = senderText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_blocked) _thisComponent.text = senderText + " " + addressText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_blocked) _thisComponent.text = senderText;
    }

    public void Block()
    {
        _blocked = true;
        _thisComponent.text = senderText + " " + addressText;
    }

    public void UnBlock()
    {
        _blocked = false;
        _thisComponent.text = senderText;
    }
}
