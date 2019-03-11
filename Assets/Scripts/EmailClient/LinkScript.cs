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
        private int _currentLinkIndexActive; // The link id we're currently hovering on
        private int _previousHyperLinkIndex;
        public GameObject[] hiddenPanels; // The undercover feedback panels revealed by hovering on the correct link
        public GameObject[] linkPanels;

        private void Awake()
        {
            _textMeshProText = GetComponent<TextMeshProUGUI>();
            Array.ForEach(hiddenPanels, x => x.SetActive(false));
            Array.ForEach(linkPanels, x => x.SetActive(false));
            _currentLinkIndexActive = -1;
            _previousHyperLinkIndex = -1;
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

            if (hiddenPanels.Length > 0) UpdateFeedbackPanels(hoveredLinkIndex);
            else UpdateLinkPanels(hoveredLinkIndex);
        }

        private void UpdateFeedbackPanels(int hoveredLinkIndex)
        {
            // Check if it exists and if it's different from the previous one
            if (hoveredLinkIndex != -1 && hoveredLinkIndex != _currentLinkIndexActive)
            {
                // Unselect panel for previously hovered on link
                if (_currentLinkIndexActive != -1) hiddenPanels[_currentLinkIndexActive].SetActive(false);
                // Assign currentLinkActive
                _currentLinkIndexActive = hoveredLinkIndex;
                // Position panel above where the mouse is
                hiddenPanels[_currentLinkIndexActive].transform.position = new Vector3(
                    Input.mousePosition.x + 2,
                    Input.mousePosition.y + 2,
                    Input.mousePosition.z + 2
                );
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

        private void UpdateLinkPanels(int hoveredLinkIndex)
        {
            // Check if it exists
            if (hoveredLinkIndex != -1)
            {
                // Check if it's a link and if it's different from the previous one
                var splitId = _textMeshProText.textInfo.linkInfo[hoveredLinkIndex].GetLinkID().Split(' ');
                if (string.CompareOrdinal(splitId[0], "link") == 0) _currentLinkIndexActive = int.Parse(splitId[1]);
                else _currentLinkIndexActive = -1;
                if (_currentLinkIndexActive == -1 || _currentLinkIndexActive == _previousHyperLinkIndex) return;
                // Unselect panel for previously hovered on link
                if (_previousHyperLinkIndex != -1) linkPanels[_previousHyperLinkIndex].SetActive(false);
                // Position panel below the mouse
                linkPanels[_currentLinkIndexActive].transform.position = new Vector3(
                    Input.mousePosition.x - 2,
                    (float) (Input.mousePosition.y - linkPanels[_currentLinkIndexActive]
                                 .GetComponent<RectTransform>().rect.height * 1.5),
                    Input.mousePosition.z - 2
                );
                // Show the panel for the link we're hovering on
                linkPanels[_currentLinkIndexActive].SetActive(true);
                // The current link becomes the previous link
                _previousHyperLinkIndex = _currentLinkIndexActive;
            }
            // We're hovering over nothing, hide the previous panel
            else if (hoveredLinkIndex == -1 && _previousHyperLinkIndex != -1)
            {
                linkPanels[_previousHyperLinkIndex].SetActive(false);
                _previousHyperLinkIndex = -1;
            }
        }
    }
}