using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {

    public TimerScript timer;
    public PausePanelScript pausePanel;
    public ExplanationScript explanations;
    public EmailScript emailScript;
    public ScoreScript score;
    public StartCountDownScript startCountdown;
    public GameObject finishedPanel;
    public GameObject tryAgainButton;
    public GameObject continueButton;
    public GameObject pauseMenu;
    public GameObject blur;
    // Audio sources
    public AudioSource whistleSound;
    public AudioSource backgroundMusic;
    public AudioSource lightClick;
    public AudioSource meanClick;
    public AudioSource scoreTally;
    // Variables
    private bool _pauseEnabled; // Game can be paused
    private bool _gameIsPaused; // Game is currently paused

    /*
     * initialisation
     */
    private void Awake()
    {
        // Make pause disabled
        _pauseEnabled = false;
        pauseMenu.SetActive(false);
        _gameIsPaused = false;
        // Make some objects inactive
        score.gameObject.SetActive(false);
        finishedPanel.SetActive(false);
        continueButton.gameObject.SetActive(false);
        tryAgainButton.gameObject.SetActive(false);
        // Blur the game
        blur.SetActive(true);
        // Give your reference to other objects
        score         .SetGameScript(this);
        timer         .SetGameScript(this);
        explanations  .SetGameScript(this);
        emailScript   .SetGameScript(this);
        pausePanel    .SetGameScript(this);
        startCountdown.SetGameScript(this);
    }

    /*
     * Called every frame after everything else
     */
    private void Update()
    {
        // Check if pause button pressed
        if (_pauseEnabled && Input.GetKeyUp(KeyCode.Escape))
        {
            if (!_gameIsPaused) Pause();
            else UnPause();
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        _gameIsPaused = true;
        // Stop timer
        timer.PauseTimer();
        // Pause music
        backgroundMusic.Pause();
    }

    public void UnPause()
    {
        pauseMenu.SetActive(false);
        _gameIsPaused = false;
        // Unpause music
        backgroundMusic.Play();
        // Unpause timer
        timer.UnPauseTimer();
    }

    /*
     * The timer reached 0
     */
    public void TimerEnded()
    {
        whistleSound.Play();
        StartCoroutine(EndGame());
    }

    /*
     * Check button was clicked after every mail was sorted
     */
     public void FinishedSortingEmails()
    {
        whistleSound.Play();
        timer.StopTimer();
        StartCoroutine(EndGame());
    }

    /*
     * Looked at all the explanation panels
     */
    public void ExplanationsDone()
    {
        StartCoroutine(StartGame());
    }

    /*
     * Start the game
     */
    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.2f);
        // Show countdown
        startCountdown.StartCountdown();
    }

    /*
     * Called when the countdown is done
     */
     public void CountdownDone()
    {
        // Remove blur
        blur.SetActive(false);
        // Make pause enabled
        _pauseEnabled = true;
        // Start music
        backgroundMusic.Play();
        // Start timer
        timer.StartTimer();
    }

    /*
     * Game ended
     */
    private IEnumerator EndGame()
    {
        // Disable pause
        _pauseEnabled = false;
        // Stop the stuff
        backgroundMusic.Stop();
        finishedPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        // Blur
        blur.SetActive(true);
        int[] results = emailScript.CheckEmails();
        // (int totalEmailsInt, int phishingEmailsInt, int sortedEmailsInt, int correctlyIdentifiedInt, int wronglyTrashedInt)
        // Show score panel
        score.gameObject.SetActive(true);
        // Deactivate now useless panels
        finishedPanel.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        // Show score
        score.ShowScore(results[0], results[1], results[2], results[3], results[4]);
    }

    /*
     * After the score has been shown
     */
     public void FinishedShowingScore(bool passed)
    {
        // Remove blur
        blur.SetActive(false);
        // Tag emails
        emailScript.TagEmails();
        // Remove score panel and finish panel
        score.gameObject.SetActive(false);
        // Show try again, only show continue if passed
        tryAgainButton.gameObject.SetActive(true);
        if (passed) continueButton.gameObject.SetActive(true);
    }

    public void PlayLightClick()
    {
        lightClick.Play();
    }

    public void PlayMeanClick()
    {
        meanClick.Play();
    }

    public void ToggleScoreTally(bool toggle)
    {
        if (toggle)scoreTally.Play();
        else scoreTally.Pause();
    }

    public void TryAgainButtonPressed()
    {
        // play click sound
        PlayMeanClick();
        // Start game over
        StartOver();
    }

    public void ContinueButtonPressed()
    {
        // play click sound
        PlayMeanClick();
        // Increase level
        emailScript.IncreaseLevel();
        // Go back to office scene
        SceneManager.LoadScene("Office");
    }

    /*
     * Called on try again
     */
    private void StartOver()
    {
        // Blur
        blur.SetActive(true);
        // Make some objects inactive
        score.Reset();
        finishedPanel.SetActive(false);
        continueButton.gameObject.SetActive(false);
        tryAgainButton.gameObject.SetActive(false);
        // Reset timer
        timer.gameObject.SetActive(true);
        timer.ResetTimer();
        // Reset email positions and color
        emailScript.ResetEmails();
        // Start game
        StartCoroutine(StartGame());
    }
}
