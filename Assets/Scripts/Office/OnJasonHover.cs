using UnityEngine;

/*
 * Script that manages Jason's dialogue and the retry of previous levels
 */
namespace Office
{
    public class OnJasonHover : MonoBehaviour
    {
        public GameObject[] dialogueBoxes;
        public GameObject interogationMark;
        private bool _dialogueBoxShowing;
        private bool _clickedOnManager;
        public AudioSource boopSound;
        public AudioSource lightClick;
        public AudioSource meanClick;
        private bool awardedAchYet = false;
        public OfficeScript officeScript;
        
        private void Awake()
        {
            foreach (var dialogueBox in dialogueBoxes)
            {
                dialogueBox.SetActive(false);
            }
            interogationMark.SetActive(false);
        }
        
        private void OnMouseDown()
        {
            if (_dialogueBoxShowing)
            {
                dialogueBoxes[StaticClass.JsonCount].SetActive(false);
                _dialogueBoxShowing = false;
            }
            else
            {
                dialogueBoxes[StaticClass.JsonCount].SetActive(true);
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

        public void OnNextClicked()
        {
            lightClick.Play();
            dialogueBoxes[StaticClass.JsonCount].SetActive(false);
            StaticClass.JsonCount++;
            dialogueBoxes[StaticClass.JsonCount].SetActive(true);
        }

        public void RetryEasyLevel()
        {
            meanClick.Play();
            StaticClass.RetryEasyLevel();
        }
        
        public void RetryMediumLevel()
        {
            meanClick.Play();
            StaticClass.RetryMediumLevel();
        }
    }
}