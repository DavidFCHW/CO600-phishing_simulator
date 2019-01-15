using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    GameScript gameScript;
    // Flags
    private float countFrom = 20.0f;
    Color32 stillOkColor = new Color32(0, 255, 0, 255);
    Color32 criticalTimeColor = new Color32(255, 0, 0, 255);
    // Object references
    public GameObject shrinkingRectangle;
    public Text timerText;
    public AudioSource countdown;
    // Variables
    private float timerValue;
    private bool counting = false;
    private bool playedCountdown = false;

    private void Awake()
    {
        ResetTimer();
    }

    public void SetGameScript(GameScript gameScript)
    {
        this.gameScript = gameScript;
    }

    // Update is called once per frame
    void Update () {
        if (counting && timerValue > 0.0f)
        {
            // Check if we're on the last 5 seconds
            if ((int) timerValue == 4 && !playedCountdown)
            {
                countdown.Play();
                playedCountdown = true;
            }
            // Timer goes down
            timerValue -= Time.deltaTime;
            // Update the text
            timerText.text = (timerValue).ToString("n0");
            // Shrink the rectangle
            shrinkingRectangle.GetComponent<Image>().fillAmount -= 1.0f / countFrom * Time.deltaTime;
            // Change color
            shrinkingRectangle.GetComponent<Image>().color = new Color32(
                (byte) Mathf.Lerp(criticalTimeColor.r, stillOkColor.r, (float) timerValue / countFrom),
                (byte) Mathf.Lerp(criticalTimeColor.g, stillOkColor.g, (float)timerValue / countFrom),
                (byte) Mathf.Lerp(criticalTimeColor.b, stillOkColor.b, (float)timerValue / countFrom),
                (byte) Mathf.Lerp(criticalTimeColor.a, stillOkColor.a, (float)timerValue / countFrom));
        }
        else if (timerValue < 0.0f && counting) TimerEnded();
    }

    public void StartTimer()
    {
        counting = true;
    }

    public void StopTimer()
    {
        counting = false;
    }

    public void TimerEnded()
    {
        counting = false;
        gameScript.TimerEnded();
    }

    public void ResetTimer()
    {
        // Stop timer
        counting = false;
        // Set timer value
        timerValue = countFrom;
        // Set timer text
        timerText.text = (timerValue).ToString();
        // Set color
        shrinkingRectangle.GetComponent<Image>().fillAmount = 1;
        shrinkingRectangle.GetComponent<Image>().color = stillOkColor;
        // Reset coutdown
        playedCountdown = false;
    }
}
