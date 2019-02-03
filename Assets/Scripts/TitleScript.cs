using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour {

    public AudioSource clickSound;
    public GameObject buttonPanel;
    public GameObject creditsPanel;
    public GameObject creditInnerPanel;

    private void Start()
    {
        buttonPanel.SetActive(true);
        creditsPanel.SetActive(false);
        creditInnerPanel.SetActive(false);
    }

    private void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //either this
        SceneManager.LoadScene("Office"); //Or this one...
    }

    private void QuitGame()
    {
        Debug.Log("Quit game");
    }

    private void PlayCredits()
    {
        buttonPanel.SetActive(false);
        creditInnerPanel.SetActive(true);
        creditsPanel.SetActive(true);
    }

    public void PlayClicked()
    {
        clickSound.Play();
        PlayGame();
    }

    public void CreditsClicked()
    {
        clickSound.Play();
        PlayCredits();
    }

    public void QuitClicked()
    {
        clickSound.Play();
        QuitGame();
    }
}