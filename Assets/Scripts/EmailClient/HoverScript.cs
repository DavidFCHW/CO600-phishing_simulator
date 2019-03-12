using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EmailClient
{
    /*
     * Hover over a component and a panel appears below the mouse
     * Similar to LinkScript but on the entire component
     * Used for attachments
     */
    public class HoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject hiddenPanel; // The panel to show on hover
        [SerializeField] private Color32 normalColor;
        [SerializeField] private Color32 hoverColor;
        
        private void Start()
        {
            hiddenPanel.SetActive(false);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            // Change object color
            gameObject.GetComponent<Image>().color = hoverColor;
            // Position the panel below the mouse
            hiddenPanel.transform.position = new Vector3(
                Input.mousePosition.x - 2,
                Input.mousePosition.y - hiddenPanel.GetComponent<RectTransform>().rect.height - 10,
                Input.mousePosition.z - 2
            );
            // Show the panel
            hiddenPanel.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Change object color
            gameObject.GetComponent<Image>().color = normalColor;
            // Hide panel
            hiddenPanel.SetActive(false);
        }
    }
}