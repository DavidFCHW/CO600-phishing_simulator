using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnCharacterOver : MonoBehaviour {

    public GameObject dialogueBox;
    public GameObject interogationMark;
    private bool dialogueBoxShowing = false;

    void Awake()
    {
        dialogueBox.transform.localScale = new Vector3(0, 0, 0);
        interogationMark.transform.localScale = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        /*if(Input.GetMouseButtonDown(0))
        {
            if (dialogueBoxShowing)
            {
                dialogueBox.transform.localScale = new Vector3(0, 0, 0);
                dialogueBoxShowing = false;
            }
        }*/
    }

    private void OnMouseDown()
    {
        if (dialogueBoxShowing) {
            dialogueBox.transform.localScale = new Vector3(0, 0, 0);
            dialogueBoxShowing = false;
        }
        else
        {
            dialogueBox.transform.localScale = new Vector3(1, 1, 1);
            dialogueBoxShowing = true;
        }
    }

    private void OnMouseExit()
    {
        interogationMark.transform.localScale = new Vector3(0, 0, 0);
    }

    private void OnMouseEnter()
    {
        interogationMark.transform.localScale = new Vector3(1, 1, 1);
    }
}
