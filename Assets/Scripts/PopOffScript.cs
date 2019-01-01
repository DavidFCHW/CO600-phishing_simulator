using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOffScript : MonoBehaviour {
    private Vector3 interogationMarkNormalScale = new Vector3(1, 1, 1);
    private Vector3 dialogueBoxNormalScale = new Vector3(0, 0, 0);

    public GameObject dialogueBox;
    public GameObject interogationMark;
    private bool dialogueBoxShowing = false;

    /*void Awake()
    {
        dialogueBox.transform.localScale = new Vector3(0, 0, 0);
        interogationMark.transform.localScale = new Vector3(0, 0, 0);
    }*/

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        OnMouseDown();
	}

    private void OnMouseDown()
    {
        if (dialogueBoxShowing)
        {
            dialogueBox.transform.localScale = new Vector3(0, 0, 0);
            dialogueBoxShowing = false;
        }
        else
        {
            dialogueBox.transform.localScale = dialogueBoxNormalScale;
            dialogueBoxShowing = true;
        }
    }
}
