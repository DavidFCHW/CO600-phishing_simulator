using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

    public TimerScript timer;
    public ExplanationScript explanations;
    public EmailScript emailScript;
    public ScoreScript score;
    public GameObject finishedPanel;
    // Audio sources
    public AudioSource whistleSound;
    public AudioSource backgroundMusic;
    public AudioSource lightClick;
    public AudioSource meanClick;
    public AudioSource scoreTally;

    /*
     * initialisation
     */
    private void Awake()
    {
        // Make some objects inactive
        score.gameObject.SetActive(false);
        finishedPanel.SetActive(false);
        // Give your reference to other objects
        score       .SetGameScript(this);
        timer       .SetGameScript(this);
        explanations.SetGameScript(this);
        emailScript .SetGameScript(this);
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
     IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.2f);
        backgroundMusic.Play();
        timer.StartTimer();
    }

    /*
     * Game ended
     */
    IEnumerator EndGame()
    {
        backgroundMusic.Pause();
        finishedPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        int[] results = emailScript.CheckEmails();
        // (int totalEmailsInt, int phishingEmailsInt, int sortedEmailsInt, int correctlyIdentifiedInt, int wronglyTrashedInt)
        score.gameObject.SetActive(true);
        Destroy(finishedPanel.gameObject);
        score.ShowScore(results[0], results[1], results[2], results[3], results[4]);
    }

    /*
     * After the score has been shown
     */
     public void FinishedShowingScore()
    {
        // Remove score panel and finish panel
        Destroy(score.gameObject);
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
        if (toggle)
        {
            scoreTally.Play();
        }
        else
        {
            scoreTally.Pause();
        }
    }
}
