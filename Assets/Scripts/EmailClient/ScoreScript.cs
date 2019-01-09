using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    private GameScript gameScript;
    // Flags
    private int moneyGainedPerSortedEmail = 200;
    private int moneyGainedPerCorrectlyIdentifiedEmail = 50;
    private int moneyLostPerWronglyTrashedEmail = 100;
    private string phishingEmailsString = "Phishing emails: ";
    private string sortedEmailsString = "Sorted emails: ";
    private string correctlyIdentifiedString = "Correctly identified: ";
    private string wronglyTrashedString = "Wrongly trashed: ";
    // Unity objects
    public Text phishingEmails;
    public Text sortedEmails;
    public Text correctlyIdentified;
    public Text wronglyTrashed;
    public Text profit;
    public Button doneButton;
    // The profit made
    private int profitValue = 0;

    /*
     * Initialisation
     */
    private void Awake()
    {
        profit.gameObject.SetActive(true);
        phishingEmails.gameObject.SetActive(false);
        sortedEmails.gameObject.SetActive(false);
        correctlyIdentified.gameObject.SetActive(false);
        wronglyTrashed.gameObject.SetActive(false);
        doneButton.gameObject.SetActive(false);
    }

    public void SetGameScript(GameScript gameScript)
    {
        this.gameScript = gameScript;
    }

    /*
     * Show the score
     */
    public void ShowScore(int totalEmailsInt, int phishingEmailsInt, int sortedEmailsInt, int correctlyIdentifiedInt, int wronglyTrashedInt)
    {
        // Calculate gains and loses
        int sortedEmailsGains = sortedEmailsInt * moneyGainedPerSortedEmail;
        int correctlyIdentifiedGains = correctlyIdentifiedInt * moneyGainedPerCorrectlyIdentifiedEmail;
        int wronglyTrashedLoss = wronglyTrashedInt * moneyLostPerWronglyTrashedEmail;
        // Change the static string values
        profit.text = "<color=green>£" + profitValue + "</color>";
        phishingEmailsString = phishingEmailsString + "<color=teal>" + phishingEmailsInt + "/" + totalEmailsInt + "</color>";
        sortedEmailsString = sortedEmailsString + "<color=teal>" + sortedEmailsInt + "/" + totalEmailsInt + "</color>";
        correctlyIdentifiedString = correctlyIdentifiedString + "<color=teal>" + correctlyIdentifiedInt + "/" + phishingEmailsInt + "</color>";
        wronglyTrashedString = wronglyTrashedString + "<color=teal>" + wronglyTrashedInt + "/" + (totalEmailsInt - phishingEmailsInt) + "</color>";
        // Change the text objects with the new string value + the gains
        phishingEmails.text = phishingEmailsString;
        sortedEmails.text = sortedEmailsString + "<color=green> + £" + sortedEmailsGains + "</color>";
        correctlyIdentified.text = correctlyIdentifiedString + "<color=green> + £" + correctlyIdentifiedGains + "</color>";
        wronglyTrashed.text = wronglyTrashedString + "<color=red> - £" + wronglyTrashedLoss + "</color>";
        // Make them appear dramatically
        StartCoroutine(MakeScoreTextAppearDramatically(sortedEmailsGains, correctlyIdentifiedGains, wronglyTrashedLoss));
    }

    /*
     * Makes score text appear dramatically
     */
    IEnumerator MakeScoreTextAppearDramatically(int sortedEmailsGains, int correctlyIdentifiedGains, int wronglyTrashedLoss)
    {
        // The step used when adding stuff dramatically
        int step = 5;

        // Show phishing emails
        yield return new WaitForSeconds(1);
        phishingEmails.gameObject.SetActive(true);

        // Show sorted emails
        yield return new WaitForSeconds(1);
        sortedEmails.gameObject.SetActive(true);
        // Add the profit
        yield return new WaitForSeconds(1);
        for (int i = 0; i < sortedEmailsGains; i += step)
        {
            profitValue += step;
            profit.text = "<color=green>£" + profitValue + "</color>";
            yield return null;
        }

        // Change step for smaller values
        step = 1;

        // Show correctly identified emails
        yield return new WaitForSeconds(1);
        correctlyIdentified.gameObject.SetActive(true);
        // Add the profit
        yield return new WaitForSeconds(1);
        for (int i = 0; i < correctlyIdentifiedGains; i += step)
        {
            profitValue += step;
            profit.text = "<color=green>£" + profitValue + "</color>";
            yield return null;
        }

        // Show wrongly trashed emails
        yield return new WaitForSeconds(1);
        wronglyTrashed.gameObject.SetActive(true);
        // Add the profit
        yield return new WaitForSeconds(1);
        for (int i = 0; i < wronglyTrashedLoss; i += step)
        {
            profitValue -= step;
            profit.text = "<color=green>£" + profitValue + "</color>";
            yield return null;
        }

        // Show button
        yield return new WaitForSeconds(1);
        doneButton.gameObject.SetActive(true);
    }

    /*
     * Done button clicked
     */
    public void DoneClicked()
    {
        gameScript.FinishedShowingScore();
    }
}
