using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour {

    public Text nameField;
    public Text dialogueField;
    private Queue<string> sentences;

    // Use this for initialization
    void Start () {
        sentences = new Queue<string>();
	}

    public void BeginDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.name);
        nameField.text = dialogue.name;
        sentences.Clear(); //clears sentences from previous queue

        foreach(string s in dialogue.sentences)
        {
            sentences.Enqueue(s);
        }

        ShowNextSentence();
    }

    public void ShowNextSentence()
    {
        if(sentences.Count == 0)
        {
            StopDialogue();
            return;              
        }

        string sentence = sentences.Dequeue(); //gives us the first sentence of the queue.
        //Debug.Log(sentence);
        dialogueField.text = sentence;

    }

    public void StopDialogue()
    {
        Debug.Log("End of dialogue");
        SceneManager.LoadScene("Office");

    }
	
}
