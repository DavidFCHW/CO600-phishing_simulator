using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
    public TextMeshProUGUI screenText;

    private void Start()
    {
        // Play background sound
        backgroundSound.Play();
        switch (StaticClass.GetCurrentLevel())
        {
            case 1:
            {
                if (!StaticClass.DialogueForCurrentLevelShown())
                {
                    managerDialogue1.ShowDialogue();
                    StaticClass.SeenDialogueForCurrentLevel();
                }
                screenText.text = "Easy level <sprite=3>\nMedium level <sprite=3>\nHard level <sprite=3>";
                break;
            }
            case 2:
            {
                if (!StaticClass.DialogueForCurrentLevelShown())
                {
                    managerDialogue2.ShowDialogue();
                    StaticClass.SeenDialogueForCurrentLevel();
                }
                screenText.text = "Easy level <sprite=4>\nMedium level <sprite=3>\nHard level <sprite=3>";
                break;
            }
            default:
                screenText.text = "Easy level <sprite=4>\nMedium level <sprite=4>\nHard level <sprite=3>";
                break;
        }
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

/*
 * Class that holds variables that stay loaded
 */
public static class StaticClass {
    
    private static int _currentLevel = 1;
    private static bool _dialogueForCurrentLevelShown { get; set; }
    
    /*
     * Called when a level is completed
     */
    public static void IncreaseLevel()
    {
        _currentLevel++;
        _dialogueForCurrentLevelShown = false;
    }

    public static int GetCurrentLevel()
    {
        return _currentLevel;
    }
    
    public static bool DialogueForCurrentLevelShown()
    {
        return _dialogueForCurrentLevelShown;
    }

    public static void SeenDialogueForCurrentLevel()
    {
        _dialogueForCurrentLevelShown = true;
    }
}
