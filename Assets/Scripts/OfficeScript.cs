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
    public GameObject manaherDialogue1;
    public AudioSource backgroundSound;
    // Variables
    private bool managerExplanationsShown = false;

    private void Awake()
    {
        // Set every bubble inactive
        manaherDialogue1.SetActive(false);
        // Play background sound
        backgroundSound.Play();
        // Show manager explanation the first time we enter the office
        if (!managerExplanationsShown) ShowManagerExplanations();
        managerExplanationsShown = true;
    }

    private void ShowManagerExplanations()
    {
        manaherDialogue1.SetActive(true);
    }
}
