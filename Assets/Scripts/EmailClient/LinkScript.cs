using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LinkScript : MonoBehaviour {

    private TextMeshProUGUI textMeshProText;
    private int currentLinkIndexActive;
    private GameObject associatedGameObject;
    public GameObject phishingDefinition;

    private void Awake()
    {
        textMeshProText = this.GetComponent<TextMeshProUGUI>();
        phishingDefinition.SetActive(false);
        currentLinkIndexActive = -1;
        associatedGameObject = null;
    }

    private void LateUpdate()
    {
        // Find the link hovered on if any
        int hoveredLinkIndex = TMP_TextUtilities.FindIntersectingLink(textMeshProText, Input.mousePosition, null);
        // Check if it exists and if it's different from the previous one
        if (hoveredLinkIndex != -1 && hoveredLinkIndex != currentLinkIndexActive)
        {
            // Get link info based on index
            TMP_LinkInfo linkInfo = textMeshProText.textInfo.linkInfo[hoveredLinkIndex];
            // Unselect previously hovered on link
            if (associatedGameObject != null) associatedGameObject.SetActive(false);
            // Assign currentLinkActive
            currentLinkIndexActive = hoveredLinkIndex;
            // Check if we're hovering over the word "Phishing"
            if (String.Compare(linkInfo.GetLinkID(), "Phishing definition") == 0)
            {
                associatedGameObject = phishingDefinition;
                // Position the panel above the cursor
                //associatedGameObject.transform.localPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                // Make it visible
                associatedGameObject.SetActive(true);
            }
        }
        else if (hoveredLinkIndex == -1 && currentLinkIndexActive != -1)
        {
            associatedGameObject.SetActive(false);
            currentLinkIndexActive = -1;
        }
    }
}
