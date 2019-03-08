using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    private GameScript _gameScript;
    // Flags
    private readonly int _moneyGainedPerLegitEmailArchived = 100;
    private readonly int _moneyLostPerLegitEmailTrashed = 100;
    private readonly int _moneyLostPerPhishingEmailArchived = 100;
    private readonly int _moneyGainedPerPhishingEmailTrashed = 100;
    private int scoreThreshold = 800;

    // Unity objects
    public GameObject scorePanel;
    public GameObject[] explanationPanels;
    // Score showing related stuff
    public Text legitEmailsArchived;
    public Text legitEmailsArchivedGains;
    public Text phishingEmailsArchived;
    public Text phishingEmailsArchivedLoss;
    public Text phishingEmailsTrashed;
    public Text phishingEmailsTrashedGains;
    public Text legitEmailsTrashed;
    public Text legitEmailsTrashedLosses;
    public Text profit;
    public Button doneButton;
    // The profit made
    private int _profitValue = 0;
    // The step used when adding stuff dramatically
    private int _step = 5;
    // The coroutine that shows the score dramatically
    private Coroutine _showScoreCoroutine;
    // The calculated amount of gains and losses
    private int _legitEmailsArchivedGainsInt;
    private int _legitEmailsTrashedLossesInt;
    private int _phishingEmailsArchivedLossInt;
    private int _phishingEmailsTrashedGainsInt;
    // The explanation panel we're on
    private int _currentExplanationPanel;
    
    /*
     * Initialisation
     */
    private void Start()
    {
        scorePanel.SetActive(true);
        foreach (GameObject o in explanationPanels) o.SetActive(false);
        // Set all the text fields inactive
        profit.gameObject.SetActive(true);
        legitEmailsArchived.gameObject.SetActive(false);
        legitEmailsArchivedGains.gameObject.SetActive(false);
        legitEmailsTrashed.gameObject.SetActive(false);
        legitEmailsTrashedLosses.gameObject.SetActive(false);
        phishingEmailsArchived.gameObject.SetActive(false);
        phishingEmailsArchivedLoss.gameObject.SetActive(false);
        phishingEmailsTrashed.gameObject.SetActive(false);
        phishingEmailsTrashedGains.gameObject.SetActive(false);
        // Set the done button inactive
        doneButton.gameObject.SetActive(false);
    }

    public void SetGameScript(GameScript gameScript)
    {
        _gameScript = gameScript;
    }

    /*
     * Show the score
     */
    public void ShowScore(int totalEmailsInt, int phishingEmailsInt, int sortedEmailsInt, int phishingEmailsTrashedInt, int legitEmailsTrashedInt, int phishingEmailsArchivedInt, int legitEmailsArchivedInt)
    {
        // Calculate gains and loses
        _legitEmailsArchivedGainsInt = legitEmailsArchivedInt * _moneyGainedPerLegitEmailArchived;
        _legitEmailsTrashedLossesInt = legitEmailsTrashedInt * _moneyLostPerLegitEmailTrashed;
        _phishingEmailsArchivedLossInt = phishingEmailsArchivedInt * _moneyLostPerPhishingEmailArchived;
        _phishingEmailsTrashedGainsInt = phishingEmailsTrashedInt * _moneyGainedPerPhishingEmailTrashed;
        // Change the string values
        profit.text = "<color=green>£" + _profitValue + "</color>";
        legitEmailsArchived.text = "You archived <color=teal>" + legitEmailsArchivedInt + "</color> legit emails";
        phishingEmailsArchived.text = "You archived <color=teal>" + phishingEmailsArchivedInt + "</color> phishing emails";
        phishingEmailsTrashed.text = "You trashed <color=teal>" + phishingEmailsTrashedInt + "</color> phishing emails";
        legitEmailsTrashed.text = "You trashed <color=teal>" + legitEmailsTrashedInt + "</color> legit emails";
        
        legitEmailsArchivedGains.text = "<color=green>+£" + _legitEmailsArchivedGainsInt + "</color>";
        phishingEmailsArchivedLoss.text = "<color=red>-£" + _phishingEmailsArchivedLossInt + "</color>";
        phishingEmailsTrashedGains.text = "<color=green>+£" + _phishingEmailsTrashedGainsInt + "</color>";
        legitEmailsTrashedLosses.text = "<color=red>-£" + _legitEmailsTrashedLossesInt + "</color>";
        // Make them appear dramatically
        _showScoreCoroutine = StartCoroutine(MakeScoreTextAppearDramatically(_legitEmailsArchivedGainsInt, _legitEmailsTrashedLossesInt, _phishingEmailsArchivedLossInt, _phishingEmailsTrashedGainsInt));
    }

    /*
     * Makes score text appear dramatically
     */
    IEnumerator MakeScoreTextAppearDramatically(int legitEmailsArchivedGainsInt, int legitEmailsTrashedLossesInt, int phishingEmailsArchivedLossInt, int phishingEmailsTrashedGainsInt)
    {
        // Show legit emails archived
        yield return new WaitForSeconds(0.5f);
        legitEmailsArchived.gameObject.SetActive(true);
        // Show Gain/Loss from it
        yield return new WaitForSeconds(1f);
        legitEmailsArchivedGains.gameObject.SetActive(true);
        // Change profit
        if (legitEmailsArchivedGainsInt != 0) yield return new WaitForSeconds(0.5f);
        _gameScript.ToggleScoreTally(true); // Plays the sound
        for (var i = 0; i < legitEmailsArchivedGainsInt; i += _step)
        {
            _profitValue += _step;
            if (_profitValue > 0) profit.text = "<color=green>£" + _profitValue + "</color>";
            else profit.text = "<color=red>£" + _profitValue + "</color>";
            yield return null;
        }
        _gameScript.ToggleScoreTally(false); // Stops the sound

        // Show phishing emails archived
        yield return new WaitForSeconds(0.5f);
        phishingEmailsArchived.gameObject.SetActive(true);
        // Show Gain/Loss from it
        yield return new WaitForSeconds(1f);
        phishingEmailsArchivedLoss.gameObject.SetActive(true);
        // Change profit
        if (phishingEmailsArchivedLossInt != 0) yield return new WaitForSeconds(0.5f);
        _gameScript.ToggleScoreTally(true); // Plays the sound
        for (var i = 0; i < phishingEmailsArchivedLossInt; i += _step)
        {
            _profitValue -= _step;
            if (_profitValue > 0) profit.text = "<color=green>£" + _profitValue + "</color>";
            else profit.text = "<color=red>£" + _profitValue + "</color>";
            yield return null;
        }
        _gameScript.ToggleScoreTally(false); // Stops the sound
        
        // Show phishing emails trashed
        yield return new WaitForSeconds(0.5f);
        phishingEmailsTrashed.gameObject.SetActive(true);
        // Show Gain/Loss from it
        yield return new WaitForSeconds(1f);
        phishingEmailsTrashedGains.gameObject.SetActive(true);
        // Change profit
        if (phishingEmailsTrashedGainsInt != 0) yield return new WaitForSeconds(0.5f);
        _gameScript.ToggleScoreTally(true); // Plays the sound
        for (var i = 0; i < phishingEmailsTrashedGainsInt; i += _step)
        {
            _profitValue += _step;
            if (_profitValue > 0) profit.text = "<color=green>£" + _profitValue + "</color>";
            else profit.text = "<color=red>£" + _profitValue + "</color>";
            yield return null;
        }
        _gameScript.ToggleScoreTally(false); // Stops the sound
        
        // Show legit emails trashed
        yield return new WaitForSeconds(0.5f);
        legitEmailsTrashed.gameObject.SetActive(true);
        // Show Gain/Loss from it
        yield return new WaitForSeconds(1f);
        legitEmailsTrashedLosses.gameObject.SetActive(true);
        // Change profit
        if (legitEmailsTrashedLossesInt != 0) yield return new WaitForSeconds(0.5f);
        _gameScript.ToggleScoreTally(true); // Plays the sound
        for (var i = 0; i < legitEmailsTrashedLossesInt; i += _step)
        {
            _profitValue -= _step;
            if (_profitValue > 0) profit.text = "<color=green>£" + _profitValue + "</color>";
            else profit.text = "<color=red>£" + _profitValue + "</color>";
            yield return null;
        }
        _gameScript.ToggleScoreTally(false); // Stops the sound

        // Show button
        yield return new WaitForSeconds(0.5f);
        doneButton.gameObject.SetActive(true);
    }

    private void ShowScoreUndramatically(int legitEmailsArchivedGainsInt, int legitEmailsTrashedLossesInt, int phishingEmailsArchivedLossInt, int phishingEmailsTrashedGainsInt)
    {
        _profitValue = legitEmailsArchivedGainsInt - legitEmailsTrashedLossesInt - phishingEmailsArchivedLossInt + phishingEmailsTrashedGainsInt;
        if (_profitValue > 0) profit.text = "<color=green>£" + _profitValue + "</color>";
        else profit.text = "<color=red>£" + _profitValue + "</color>";
        
        legitEmailsArchived.gameObject.SetActive(true);
        legitEmailsTrashed.gameObject.SetActive(true);
        phishingEmailsTrashed.gameObject.SetActive(true);
        phishingEmailsArchived.gameObject.SetActive(true);
        
        legitEmailsArchivedGains.gameObject.SetActive(true);
        legitEmailsTrashedLosses.gameObject.SetActive(true);
        phishingEmailsTrashedGains.gameObject.SetActive(true);
        phishingEmailsArchivedLoss.gameObject.SetActive(true);
        
        doneButton.gameObject.SetActive(true);
    }

    /*
     * Next button clicked on score panel
     */
    public void ShowScoreDone()
    {
        _gameScript.PlayLightClick();
        scorePanel.SetActive(false);
        // Show the first explanation panel
        explanationPanels[0].SetActive(true);
        _currentExplanationPanel = 0;
    }

    /*
     * Next clicked on explanations
     */
    public void NextExplanation()
    {
        _gameScript.PlayLightClick();
        explanationPanels[_currentExplanationPanel].SetActive(false);
        _currentExplanationPanel++;
        explanationPanels[_currentExplanationPanel].SetActive(true);
    }
    
    /*
     * Previous clicked on explanations
     */
    public void PreviousExplanation()
    {
        _gameScript.PlayLightClick();
        explanationPanels[_currentExplanationPanel].SetActive(false);
        _currentExplanationPanel--;
        explanationPanels[_currentExplanationPanel].SetActive(true);
    }
    
    /*
     * Done clicked on explanations
     */
    public void ExplanationsDone()
    {
        _gameScript.PlayMeanClick();
        explanationPanels[_currentExplanationPanel].SetActive(false);
        _gameScript.FinishedShowingScore(_profitValue >= scoreThreshold);
    }
    
    /*
     * Button to skip the score showing animation
     */
    public void SkipClicked()
    {
        StopCoroutine(_showScoreCoroutine);
        _gameScript.ToggleScoreTally(false);
        ShowScoreUndramatically(_legitEmailsArchivedGainsInt, _legitEmailsTrashedLossesInt, _phishingEmailsArchivedLossInt, _phishingEmailsTrashedGainsInt);
    }

    public void Reset()
    {
        scorePanel.SetActive(true);
        // Set all the text fields inactive
        profit.gameObject.SetActive(true);
        legitEmailsArchived.gameObject.SetActive(false);
        legitEmailsArchivedGains.gameObject.SetActive(false);
        legitEmailsTrashed.gameObject.SetActive(false);
        legitEmailsTrashedLosses.gameObject.SetActive(false);
        phishingEmailsArchived.gameObject.SetActive(false);
        phishingEmailsArchivedLoss.gameObject.SetActive(false);
        phishingEmailsTrashed.gameObject.SetActive(false);
        phishingEmailsTrashedGains.gameObject.SetActive(false);
        // Set the done button inactive
        doneButton.gameObject.SetActive(false);
        // Set the score panel inactive
        gameObject.SetActive(false);
        // Reset profit
        _profitValue = 0;
    }
}
