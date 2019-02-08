using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}
