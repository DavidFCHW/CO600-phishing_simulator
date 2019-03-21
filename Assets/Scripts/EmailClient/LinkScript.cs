using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * Put this script on a gameObject with a TextMeshProUGUI element
 * Add the hidden panels to the array in the order they appear in text
 * this shows a panel when hovering on the correct element
 *
 * This can only have either hiddenPanels or LinkPanels
 */
namespace EmailClient
{
    public class LinkScript : MonoBehaviour
    {

        private TextMeshProUGUI _textMeshProText;
        private int _previousLinkIndex; // The last active link panel
        private int _previousFeedbackIndex; // The last active feedback panel
        public GameObject[] feedbackPanel; // The undercover feedback panels
        public GameObject[] linkPanels; // The undercover link panels

        private void Start()
        {
            _textMeshProText = GetComponent<TextMeshProUGUI>();
            Array.ForEach(feedbackPanel, x => x.SetActive(false));
            Array.ForEach(linkPanels, x => x.SetActive(false));
            _previousLinkIndex = -1;
            _previousFeedbackIndex = -1;
        }

        private void LateUpdate()
        {
            // Find the link hovered on if any
            var hoveredLinkIndex = -1;
            try
            {
                hoveredLinkIndex = TMP_TextUtilities.FindIntersectingLink(_textMeshProText, Input.mousePosition, null);
            }
            catch (IndexOutOfRangeException e)
            {
            }
            
            // Check if it exists
            if (hoveredLinkIndex != -1)
            {
                // Get the attributes
                foreach (var attribute in _textMeshProText.textInfo.linkInfo[hoveredLinkIndex].GetLinkID().Split(new [] { ", " }, StringSplitOptions.None))
                {
                    var splitId = attribute.Split('=');
                    if (string.CompareOrdinal(splitId[0], "feedback") == 0) UpdateFeedbackPanels(int.Parse(splitId[1]));
                    else if (string.CompareOrdinal(splitId[0], "link") == 0) UpdateLinkPanels(int.Parse(splitId[1]));
                }
            }
            // We're hovering over nothing and a feedback panel is showing so hide it
            else if (hoveredLinkIndex == -1 && _previousFeedbackIndex != -1)
            {
                feedbackPanel[_previousFeedbackIndex].SetActive(false);
                _previousFeedbackIndex = -1;
            }
            // We're hovering over nothing and a feedback panel is showing so hide it
            else if (hoveredLinkIndex == -1 && _previousLinkIndex != -1)
            {
                linkPanels[_previousLinkIndex].SetActive(false);
                _previousLinkIndex = -1;
            }
        }

        private void UpdateFeedbackPanels(int panelIndex)
        {
            // Check if it's different from the previous one
            if (panelIndex == _previousFeedbackIndex) return;
            // Unselect panel for previously hovered on link if there was one
            if (_previousFeedbackIndex != -1) feedbackPanel[_previousFeedbackIndex].SetActive(false);
            // Position panel above where the mouse is
feedbackPanel[panelIndex].transform.position = new Vector3(
    Input.mousePosition.x + 2,
    Input.mousePosition.y + 2,
    Input.mousePosition.z + 2
);
            // Show the panel for the link we're hovering on
            feedbackPanel[panelIndex].SetActive(true);
            // Assign previous panel
            _previousFeedbackIndex = panelIndex;
        }
        
        private void UpdateLinkPanels(int panelIndex)
        {
            // Check if it's different from the previous one
            if (panelIndex == _previousLinkIndex) return;
            // Unselect panel for previously hovered on link if there was one
            if (_previousLinkIndex != -1) linkPanels[_previousLinkIndex].SetActive(false);
            // Position panel above where the mouse is
            var rect = linkPanels[panelIndex].GetComponent<RectTransform>().rect;
            linkPanels[panelIndex].transform.position = new Vector3(
                Input.mousePosition.x - rect.width / 4,
                Input.mousePosition.y - rect.height / 2,
                Input.mousePosition.z - 2
            );
            // Show the panel for the link we're hovering on
            linkPanels[panelIndex].SetActive(true);
            // Assign previous panel
            _previousLinkIndex = panelIndex;
        }
    }
}