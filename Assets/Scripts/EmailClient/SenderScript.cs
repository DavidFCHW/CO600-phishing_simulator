using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SenderScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject fullAddress;

    private void Start()
    {
        fullAddress.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        fullAddress.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        fullAddress.SetActive(false);
    }
}
