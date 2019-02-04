using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script to coordinate the events in the office
 */
public class OfficeScript : MonoBehaviour
{
    // Unity object references
    public ManagerTalkScript manaherDialogue1;
    public AudioSource backgroundSound;
    // Variables
    private static bool managerExplanationsShown = false;

    private void Start()
    {
        // Play background sound
        backgroundSound.Play();
        // Show manager explanation the first time we enter the office
        if (!managerExplanationsShown) manaherDialogue1.ShowDialogue();
        managerExplanationsShown = true;
    }
}
