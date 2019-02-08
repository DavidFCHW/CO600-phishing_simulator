using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The manager explanations upon entering the office
 * Re-designed to be re-useable
 * Assign this script to the container Panel (not the canvas)
 */
public class ManagerTalkScript : MonoBehaviour {

    public FirstPersonCameraScript mainCam;
    public GameObject[] textPanels; // All the text panels
    private int currentTextPanel; // The text panel we're currently on
    public AudioSource clickSound;

    private void Awake()
    {
        mainCam.UnBlock();
        // Hide object
        this.gameObject.SetActive(false);
        // Hide all text panels
        foreach (GameObject go in textPanels)
        {
            go.SetActive(false);
        }
        // Show the first text panel
        currentTextPanel = 0;
        textPanels[currentTextPanel].SetActive(true);
    }

    /*
     * Show dilogue
     */
     public void ShowDialogue()
    {
        mainCam.Block();
        this.gameObject.SetActive(true);
    }

    /*
     * Go to next panel
     */
    private void GoToNextPanel()
    {
        if (currentTextPanel+1 >= textPanels.Length)
        {
            // We're on the last one
            Destroy(gameObject);
            mainCam.UnBlock();
        }
        else
        {
            // Go to the next panel
            textPanels[currentTextPanel].SetActive(false);
            currentTextPanel++;
            textPanels[currentTextPanel].SetActive(true);
        }
    }

    /*
     * Next button clicked
     */
    public void OnNextClicked()
    {
        clickSound.Play();
        GoToNextPanel();
    }
}
