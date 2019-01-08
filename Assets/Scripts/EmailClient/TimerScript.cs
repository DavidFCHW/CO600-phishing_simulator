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
    // Variables
    private float timerValue;
    private bool counting = false;

    private void Start()
    {
        // Set the color
        shrinkingRectangle.GetComponent<Image>().color = stillOkColor;
        // Set timer value
        timerValue = countFrom;
        // Set timer text
        timerText.text = timerValue.ToString();
    }

    public void SetGameScript(GameScript gameScript)
    {
        this.gameScript = gameScript;
    }

    // Update is called once per frame
    void Update () {
        if (counting && timerValue > 0.0f)
        {
            // Timer goes down
            timerValue -= Time.deltaTime;
            // Update the text
            timerText.text = timerValue.ToString("n0");
            // Shrink the rectangle
            shrinkingRectangle.GetComponent<Image>().fillAmount -= 1.0f / countFrom * Time.deltaTime;
            // Change color
            shrinkingRectangle.GetComponent<Image>().color = new Color(
                Mathf.Lerp(stillOkColor.r, criticalTimeColor.r, timerValue / countFrom),
                Mathf.Lerp(stillOkColor.g, criticalTimeColor.g, timerValue / countFrom),
                Mathf.Lerp(stillOkColor.b, criticalTimeColor.b, timerValue / countFrom),
                Mathf.Lerp(stillOkColor.a, criticalTimeColor.a, timerValue / countFrom));
        }
        else if (timerValue <= 0.0f) TimerEnded();
    }

    public void StartTimer()
    {
        counting = true;
    }

    public void TimerEnded()
    {
        counting = false;
        gameScript.TimerEnded();
    }
}
