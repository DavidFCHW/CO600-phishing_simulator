using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class showActualAddressScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

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
        actualAddressPanel.transform.localScale = new Vector3(1, 1, 1);
        this.gameObject.GetComponent<Image>().color = new Color32(255, 165, 255, 255);
    }

    public void hideActualAddress()
    {
        actualAddressPanel.transform.localScale = new Vector3(0, 0, 0);
        this.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = new Color32(255, 165, 255, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!actualEmailAddressIsShowed)
        {
            this.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
}
