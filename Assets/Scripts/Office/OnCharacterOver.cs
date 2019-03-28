using UnityEngine;

/*
 * Script called when you hover over the manager
 * it shows the boop and explanations and whatnot
 */
namespace Office
{
    public class OnCharacterOver : MonoBehaviour
    {

        public GameObject dialogueBox;
        public GameObject interogationMark;
        private bool _dialogueBoxShowing;
        private bool _clickedOnManager;
        public AudioSource boopSound;

        private void Awake()
        {
            dialogueBox.SetActive(false);
            interogationMark.SetActive(false);
        }

        private void OnMouseDown()
        {
            if (_dialogueBoxShowing)
            {
                dialogueBox.SetActive(false);
                _dialogueBoxShowing = false;
            }
            else
            {
                dialogueBox.SetActive(true);
                _dialogueBoxShowing = true;
            }
        }

        private void OnMouseExit()
        {
            interogationMark.SetActive(false);
        }

        private void OnMouseEnter()
        {
            boopSound.Play();
            interogationMark.SetActive(true);
        }
    }
}