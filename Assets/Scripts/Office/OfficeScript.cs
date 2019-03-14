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
    public ManagerTalkScript managerDialogue3;
    public AudioSource backgroundSound;
    public AudioSource clickSound;
    // Achievement related stuff
    public GameObject greyedPhisherman;
    public GameObject phisherman;
    public GameObject greyedKingphisher;
    public GameObject kingphisher;
    public GameObject greyedPoseidon;
    public GameObject poseidon;
    // Manager
    public GameObject managerNeutral;
    public GameObject managerThinking;
    // Hidden jason nurse
    public GameObject jasonNurse;

    private void Start()
    {
        // Play background sound
        backgroundSound.Play();
        switch (StaticClass.GetCurrentLevel())
        {
            case 1:
            {
                // Sort out the dialogue
                if (!StaticClass.DialogueForCurrentLevelShown())
                {
                    managerDialogue1.ShowDialogue();
                    StaticClass.SeenDialogueForCurrentLevel();
                }
                // Show the correct manager
                managerNeutral.SetActive(true);
                managerThinking.SetActive(false);
                // Hide Jason
                jasonNurse.SetActive(false);
                break;
            }
            case 2:
            {
                if (!StaticClass.DialogueForCurrentLevelShown())
                {
                    managerDialogue2.ShowDialogue();
                    StaticClass.SeenDialogueForCurrentLevel();
                }
                // Show the correct manager
                managerNeutral.SetActive(true);
                managerThinking.SetActive(false);
                // Hide Jason
                jasonNurse.SetActive(false);
                break;
            }
            case 3:
            {
                if (!StaticClass.DialogueForCurrentLevelShown())
                {
                    managerDialogue3.ShowDialogue();
                    StaticClass.SeenDialogueForCurrentLevel();
                }
                // Show the correct manager
                managerNeutral.SetActive(false);
                managerThinking.SetActive(true);
                // Show Jason
                jasonNurse.SetActive(true);
                break;
            }
        }
        // Sort out the badges
        greyedPhisherman.SetActive(!StaticClass.gotAchievementEasy);
        greyedKingphisher.SetActive(!StaticClass.gotAchievementMedium);
        greyedPoseidon.SetActive(!StaticClass.gotAchievementHard);
        phisherman.SetActive(StaticClass.gotAchievementEasy);
        kingphisher.SetActive(StaticClass.gotAchievementMedium);
        poseidon.SetActive(StaticClass.gotAchievementHard);
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

    public void BumpAchievement()
    {
        StaticClass.gotAchievementHard = true;
        greyedPoseidon.SetActive(false);
        poseidon.SetActive(true);
    }
}

/*
 * Class that holds variables that stay loaded
 */
public static class StaticClass {
    
    private static int _currentLevel = 1;
    private static bool _dialogueForCurrentLevelShown { get; set; }
    public static bool gotAchievementEasy;
    public static bool gotAchievementMedium;
    public static bool gotAchievementHard;
    
    /*
     * Called when a level is completed
     */
    public static void IncreaseLevel()
    {
        if (_currentLevel == 1) gotAchievementEasy = true;
        else if (_currentLevel == 2) gotAchievementMedium = true;
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
