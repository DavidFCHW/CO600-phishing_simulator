﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour {

    public AudioSource clickSound;
    public AudioSource backgroundMusic;
    public GameObject buttonPanel;
    public GameObject creditsPanel;
    public GameObject creditsText;
    // Variables
    private float _creditsTextYOffset;
    private int _step = 1; // How fast the credits go
    private RectTransform _creditsTextRectTrans;

    private void Start()
    {
        _creditsTextRectTrans = creditsText.GetComponent<RectTransform>();
        // Place the credit panel just below the canvas
        var creditsTextRectTrans = creditsText.GetComponent<RectTransform>();
        _creditsTextYOffset = -gameObject.GetComponent<RectTransform>().rect.height;
        creditsTextRectTrans.offsetMax = new Vector2(0, _creditsTextYOffset);
        // Hide credits
        buttonPanel.SetActive(true);
        creditsPanel.SetActive(false);
        creditsText.SetActive(true);
        // Start the music
        backgroundMusic.Play();
    }

    /*
     * Make the credits go faster
     */
     private void IncreaseStep()
    {
        if (_step == 1) _step = 4;
        else if (_step == 4) _step = 8;
        else if (_step == 8) _step = 1;
    }

     private void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //either this
        SceneManager.LoadScene("Office"); //Or this one...
    }

    private void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

    private void PlayCredits()
    {
        // Hide button panel
        buttonPanel.SetActive(false);
        // Show credits panel
        creditsPanel.SetActive(true);
        // Move credits text
        StartCoroutine(MoveCreditsUp());
    }

    private void RemoveCredits()
    {

        // Hide credit panel and show buttons
        creditsPanel.SetActive(false);
        buttonPanel.SetActive(true);
    }

    /*
     * Moves the credits up until out of view
     */
    IEnumerator MoveCreditsUp()
    {
        for (float i = _creditsTextYOffset; i < LayoutUtility.GetPreferredSize(_creditsTextRectTrans, 1); i += _step)
        {
            _creditsTextRectTrans.offsetMax = new Vector2(0, i);
            yield return null;
        }
        _step = 1;
        yield return new WaitForSeconds(1);
        RemoveCredits();
    }

    /*
     * Play button clicked
     */
    public void PlayClicked()
    {
        clickSound.Play();
        PlayGame();
    }

    /*
     * Credits button clicked
     */
    public void CreditsClicked()
    {
        clickSound.Play();
        PlayCredits();
    }

    /*
     * Quit button clicked
     */
    public void QuitClicked()
    {
        clickSound.Play();
        QuitGame();
    }

    /*
     * Fast forward button clicked
     */
     public void FastForwardClicked()
    {
        clickSound.Play();
        IncreaseStep();
    }
}