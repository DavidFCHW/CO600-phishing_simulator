using UnityEngine;

namespace Office
{
    public class SimpleCharacterHoverScript : MonoBehaviour
    {
        public GameObject dialogue;

        private void Start()
        {
            dialogue.SetActive(false);
        }

        private void OnMouseExit()
        {
            dialogue.SetActive(false);
        }

        private void OnMouseEnter()
        {
            dialogue.SetActive(true);
        }
    }
}