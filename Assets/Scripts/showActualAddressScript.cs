using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class showActualAddressScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // File constants
    private Color32 showActualAddressColor = new Color32(255, 165, 255, 255);
    private Color32 normalAddressColor = new Color32(255, 255, 255, 255);
    private Vector3 normalAddressScale = new Vector3(1, 1, 1);

    public GameObject actualAddressPanel;
    private bool actualEmailAddressIsShowed = false;

    public void toggleActualEmailAddress()
    {
        if (actualEmailAddressIsShowed) {
            hideActualAddress();
            actualEmailAddressIsShowed = false;
        } else {
            showActualAddress();
            actualEmailAddressIsShowed = true;
        }
    }

    public void showActualAddress() {
        actualAddressPanel.transform.localScale = normalAddressScale;
        this.gameObject.GetComponent<Image>().color = showActualAddressColor;
    }

    public void hideActualAddress()
    {
        actualAddressPanel.transform.localScale = new Vector3(0, 0, 0);
        this.gameObject.GetComponent<Image>().color = normalAddressColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = showActualAddressColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!actualEmailAddressIsShowed)
        {
            this.gameObject.GetComponent<Image>().color = normalAddressColor;
        }
    }
}
