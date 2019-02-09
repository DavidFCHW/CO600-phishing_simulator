using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

/*
 * Script to coordinate the events in the office
 */
public class OfficeScript : MonoBehaviour
{
    // Unity object references
    public ManagerTalkScript managerDialogue1;
    public ManagerTalkScript managerDialogue2;
    public AudioSource backgroundSound;
    public AudioSource clickSound;
    // Variables
    private static bool _managerExplanationsShown;

    private void Start()
    {
        // Play background sound
        backgroundSound.Play();
        // Show manager explanation the first time we enter the office
        if (!_managerExplanationsShown) managerDialogue1.ShowDialogue();
        else managerDialogue2.ShowDialogue();
        _managerExplanationsShown = true;
    }
    
    /*
     * The button in the manager bubble asking you for help
     */
    public void AcceptButtonClicked()
    {
        clickSound.Play();
        SceneManager.LoadScene("Email Client");
    }

    public void QuitGameClicked()
    {
        clickSound.Play();
        SceneManager.LoadScene("Title Screen");
    }
}
