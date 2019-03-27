using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    public TimerScript timer;
    public PausePanelScript pausePanel;
    public ExplanationScript easyExplanations;
    public ExplanationScript mediumExplanations;
    public EmailScript emailScript;
    public ScoreScript score;
    public StartCountDownScript startCountdown;
    public GameObject finishedPanel;
    public GameObject tryAgainButton;
    public ContinueButtonHoverScript continueButton;
    public GameObject continueBlockedExplanations;
    public GameObject pauseMenu;
    public GameObject blur;
    // Audio sources
    public AudioSource whistleSound;
    public AudioSource backgroundMusic;
    public AudioSource backgroundNoise;
    public AudioSource lightClick;
    public AudioSource meanClick;
    public AudioSource scoreTally;
    // Variables
    private bool _pauseEnabled; // Game can be paused
    private bool _gameIsPaused; // Game is currently paused
    private bool _canContinue; // Whether or not the player achieved a high enough score to continue

    /*
     * initialisation
     */
    private void Awake()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        // Make pause enabled
//        _pauseEnabled = false;
        _pauseEnabled = true;
        // Start background noise
        backgroundNoise.Play();
        // Hide pause menu
        pauseMenu.SetActive(false);
        _gameIsPaused = false;
        // Make some objects inactive
        score.gameObject.SetActive(false);
        finishedPanel.SetActive(false);
        continueButton.gameObject.SetActive(false);
        tryAgainButton.gameObject.SetActive(false);
        continueBlockedExplanations.SetActive(false);
        // Blur the game
        blur.SetActive(true);
        // Give your reference to other objects
        score             .SetGameScript(this);
        timer             .SetGameScript(this);
        easyExplanations  .SetGameScript(this);
        mediumExplanations.SetGameScript(this);
        emailScript       .SetGameScript(this);
        pausePanel        .SetGameScript(this);
        startCountdown    .SetGameScript(this);
        continueButton    .SetGameScript(this);
        // Show explanations
        easyExplanations.gameObject.SetActive(false);
        mediumExplanations.gameObject.SetActive(false);
        if (StaticClass.GetCurrentLevel() == 1) easyExplanations.gameObject.SetActive(true);
        else if (StaticClass.GetCurrentLevel() == 2) mediumExplanations.gameObject.SetActive(true);
        else ExplanationsDone();
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

    public void Pause()
    {
        pauseMenu.SetActive(true);
        _gameIsPaused = true;
        // Stop timer
        timer.PauseTimer();
        // Pause music
        backgroundMusic.Pause();
        backgroundNoise.Pause();
    }

    public void UnPause()
    {
        pauseMenu.SetActive(false);
        _gameIsPaused = false;
        // Unpause music
        backgroundMusic.Play();
        backgroundNoise.Play();
        // Unpause timer
        timer.UnPauseTimer();
    }

    /*
     * Looked at all the explanation panels
     */
    public void ExplanationsDone()
    {
        // Remove all the explanations
        easyExplanations.DestroyExplanations();
        mediumExplanations.DestroyExplanations();
        // Start game
        StartCoroutine(StartGame());
    }

    /*
     * Start the countdown before starting the game
     * Then the countdown will start the game
     */
    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.2f);
        // Disable pause for countdown
        _pauseEnabled = false;
        // Show countdown
        startCountdown.StartCountdown();
    }

    /*
     * Called when the countdown is done
     */
     public void CountdownDone()
    {
        // Make pause enabled again
        _pauseEnabled = true;
        // Remove blur
        blur.SetActive(false);
        // Make the game pause-able
        _pauseEnabled = true;
        // Start music
        backgroundMusic.Play();
        // Start timer
        timer.StartTimer();
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
      * The timer reached 0
      */
     public void TimerEnded()
     {
         whistleSound.Play();
         StartCoroutine(EndGame());
     }

    /*
     * Game ended
     */
    private IEnumerator EndGame()
    {
        // Disable pause while showing score
        _pauseEnabled = false;
//        _pauseEnabled = true;
        // Stop the stuff
        backgroundMusic.Stop();
        backgroundNoise.Stop();
        finishedPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        // Blur
        blur.SetActive(true);
        var results = emailScript.CheckEmails();
        // (int totalEmailsInt, int phishingEmailsInt, int sortedEmailsInt, int correctlyIdentifiedInt, int wronglyTrashedInt)
        // Show score panel
        score.gameObject.SetActive(true);
        // Deactivate now useless panels
        finishedPanel.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        // Show score
        score.ShowScore(
            results[0], 
            results[1], 
            results[2], 
            results[3], 
            results[4], 
            results[5], 
            results[6]
        );
    }

    /*
     * After the score has been shown
     */
     public void FinishedShowingScore(bool passed)
    {
        // Make pause enabled again
        _pauseEnabled = true;
        // Remove blur
        blur.SetActive(false);
        // Tag emails
        emailScript.TagEmails();
        // Remove score panel and finish panel
        score.gameObject.SetActive(false);
        // Show try again, only show continue if passed
        tryAgainButton.gameObject.SetActive(true);
        if (!passed)
        {
            _canContinue = false;
            continueButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            _canContinue = true;
            continueButton.GetComponent<Button>().interactable = true;
        }
        continueButton.gameObject.SetActive(true);
    }

     public void ContinueButtonHoverStart()
     {
         if (!_canContinue) continueBlockedExplanations.SetActive(true);
     }
     
     public void ContinueButtonHoverStop()
     {
         if (!_canContinue) continueBlockedExplanations.SetActive(false);
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
        StaticClass.IncreaseLevel();
        // Go back to office scene
        SceneManager.LoadScene("Office");
    }

    public void QuitButtonPressed()
    {
        // play click sound
        PlayMeanClick();
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
        continueBlockedExplanations.SetActive(false);
        // Reset timer
        timer.gameObject.SetActive(true);
        timer.ResetTimer();
        // Reset email positions and color
        emailScript.ResetEmails();
        // Start game
        StartCoroutine(StartGame());
    }
}
