using System.Collections.Generic;
using UnityEngine;

namespace Office
{
    public class OnJasonHover : MonoBehaviour
    {
        private int count = 0;
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
                dialogueBoxes[count].SetActive(false);
                _dialogueBoxShowing = false;
            }
            else
            {
                dialogueBoxes[count].SetActive(true);
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
            dialogueBoxes[count].SetActive(false);
            count++;
            dialogueBoxes[count].SetActive(true);
        }

        public void OnOKClicked()
        {
            meanClick.Play();
            dialogueBoxes[count].SetActive(false);
            _dialogueBoxShowing = false;
            if (!awardedAchYet)
            {
                awardedAchYet = true;
                officeScript.BumpAchievement();
            }
        }
    }
}