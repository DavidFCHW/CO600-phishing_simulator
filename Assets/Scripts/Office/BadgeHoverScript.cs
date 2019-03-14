using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Office
{
    public class BadgeHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
            var screenPoint = new Vector3(
                Input.mousePosition.x - hiddenPanel.GetComponent<RectTransform>().rect.width / 4,
                Input.mousePosition.y + 50,
                1.0f
            );
            hiddenPanel.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
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