using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Shows the actual address when you click on the down arrow in email preview
 */
public class ShowActualAddressScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // File constants
    private Color32 showActualAddressColor = new Color32(255, 165, 255, 255);
    private Color32 normalAddressColor = new Color32(255, 255, 255, 255);
    private Vector3 normalAddressScale = new Vector3(1, 1, 1);

    public GameObject actualAddressPanel;
    private bool actualEmailAddressIsShowed = false;

    /*
     * Hide the actual address by default
     */
    private void Start()
    {
        //HideActualAddress();
    }

    /*
     * Toggles between showing and hiding the address box
     */
    public void ToggleActualEmailAddress()
    {
        if (actualEmailAddressIsShowed) {
            HideActualAddress();
            actualEmailAddressIsShowed = false;
        } else {
            ShowActualAddress();
            actualEmailAddressIsShowed = true;
        }
    }

    /*
     * Shows the address
     */
    public void ShowActualAddress() {
        actualAddressPanel.transform.localScale = normalAddressScale;
        //actualAddressPanel.SetActive(true);
        this.gameObject.GetComponent<Image>().color = showActualAddressColor;
    }

    /*
     * Hides the address
     */
    public void HideActualAddress()
    {
        actualAddressPanel.transform.localScale = new Vector3(0, 0, 0);
        //actualAddressPanel.SetActive(false);
        this.gameObject.GetComponent<Image>().color = normalAddressColor;
    }

    /*
     * Change color on hover
     */
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = showActualAddressColor;
    }

    /*
     * Change color back to normal when you stop hovering
     */
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!actualEmailAddressIsShowed)
        {
            this.gameObject.GetComponent<Image>().color = normalAddressColor;
        }
    }
}
