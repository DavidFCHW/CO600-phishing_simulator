using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * Put this script on a gameObject with a TextMeshProUGUI element
 * Add the hidden panels to the array in the order they appear in text
 * this shows a panel when hovering on the correct element
 */
public class LinkScript : MonoBehaviour {

    private TextMeshProUGUI _textMeshProText;
    private int _currentLinkIndexActive; // The link id we're currently hovering on
    public GameObject[] hiddenPanels; // The undercover panels revealed by hovering on the correct link

    private void Awake()
    {
        _textMeshProText = GetComponent<TextMeshProUGUI>();
        Array.ForEach(hiddenPanels, x => x.SetActive(false));
        _currentLinkIndexActive = -1;
    }

    private void LateUpdate()
    {
        // Find the link hovered on if any
        int hoveredLinkIndex = TMP_TextUtilities.FindIntersectingLink(_textMeshProText, Input.mousePosition, null);
        // Check if it exists and if it's different from the previous one
        if (hoveredLinkIndex != -1 && hoveredLinkIndex != _currentLinkIndexActive)
        {
            // Unselect panel for previously hovered on link
            if (_currentLinkIndexActive != -1) hiddenPanels[_currentLinkIndexActive].SetActive(false);
            // Assign currentLinkActive
            _currentLinkIndexActive = hoveredLinkIndex;
            // Show the panel for the link we're hovering on
            hiddenPanels[_currentLinkIndexActive].SetActive(true);
        }
        // We're hovering over nothing, hide the panel
        else if (hoveredLinkIndex == -1 && _currentLinkIndexActive != -1)
        {
            hiddenPanels[_currentLinkIndexActive].SetActive(false);
            _currentLinkIndexActive = -1;
        }
    }
}

//            if (hoveredLinkIndex != -1 && hoveredLinkIndex != _currentLinkIndexActive)
//            {
//                // Get link info based on index
//                TMP_LinkInfo linkInfo = _textMeshProText.textInfo.linkInfo[hoveredLinkIndex];
//                // Unselect previously hovered on link
//                if (_associatedGameObject != null) _associatedGameObject.SetActive(false);
//                // Assign currentLinkActive
//                _currentLinkIndexActive = hoveredLinkIndex;
//                // Check if we're hovering over the word "Phishing"
//                if (string.CompareOrdinal(linkInfo.GetLinkID(), "Phishing definition") == 0)
//                {
//                    _associatedGameObject = phishingDefinition;
//                    // Position the panel above the cursor
//                    //associatedGameObject.transform.localPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
//                    // Make it visible
//                    _associatedGameObject.SetActive(true);
//                }
//            }
//            else if (hoveredLinkIndex == -1 && _currentLinkIndexActive != -1)
//            {
//                _associatedGameObject.SetActive(false);
//                _currentLinkIndexActive = -1;
//            }