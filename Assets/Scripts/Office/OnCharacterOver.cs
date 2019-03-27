using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnCharacterOver : MonoBehaviour {

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
