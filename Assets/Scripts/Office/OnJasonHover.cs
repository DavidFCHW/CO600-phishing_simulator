using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                dialogueBoxes[StaticClass.jsonCount].SetActive(false);
                _dialogueBoxShowing = false;
            }
            else
            {
                dialogueBoxes[StaticClass.jsonCount].SetActive(true);
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
            dialogueBoxes[StaticClass.jsonCount].SetActive(false);
            StaticClass.jsonCount++;
            dialogueBoxes[StaticClass.jsonCount].SetActive(true);
        }

        public void OnOKClicked()
        {
            meanClick.Play();
            dialogueBoxes[StaticClass.jsonCount].SetActive(false);
            _dialogueBoxShowing = false;
            if (!awardedAchYet)
            {
                awardedAchYet = true;
                officeScript.BumpAchievement();
            }
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