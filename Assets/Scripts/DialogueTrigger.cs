using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;
    public Button nextButton;

    private void Start()
    {
        nextButton.interactable = false;
    }

    public void TriggerDialogue()
    {
        this.gameObject.GetComponent<Button>().interactable = false;
        nextButton.interactable = true;
        FindObjectOfType<DialogueManager>().BeginDialogue(dialogue);
    }
}
