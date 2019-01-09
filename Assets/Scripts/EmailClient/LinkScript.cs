using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LinkScript : MonoBehaviour {

    private TextMeshProUGUI textMeshProText;
    private int currentLinkActive;
    private GameObject associatedGameObject;
    public GameObject phishingDefinition;

    private void Awake()
    {
        textMeshProText = this.GetComponent<TextMeshProUGUI>();
        phishingDefinition.SetActive(false);
        currentLinkActive = -1;
        associatedGameObject = null;
    }

    private void LateUpdate()
    {
        //// Find the link hovered on if any
        //int hoveredLink = TMP_TextUtilities.FindIntersectingLink(textMeshProText, Input.mousePosition, null);
        //// Check if it exists and if it's different from the previous one
        //if (hoveredLink != -1 && hoveredLink != currentLinkActive)
        //{

        //    // Unselect previously hovered on link
        //    if (associatedGameObject != null) associatedGameObject.SetActive(false);
        //    // Assign currentLinkActive
        //    currentLinkActive = hoveredLink;
        //    // Check if we're hovering over the word "Phishing"
        //    if (hoveredLink == 1)
        //    {
        //        associatedGameObject = phishingDefinition;
        //        // Position the panel above the cursor
        //        associatedGameObject.transform.localPosition = new Vector3(Input.mousePosition.x + 2, Input.mousePosition.y + 2, 0);
        //        // Make it visible
        //        associatedGameObject.SetActive(true);
        //    }
        //}
    }
}
