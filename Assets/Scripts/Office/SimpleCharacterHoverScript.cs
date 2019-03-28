using UnityEngine;

/*
 * Simplified version of the character hover script
 * created for the non-important characters as the other script didn't fit
 */
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