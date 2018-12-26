using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    // Flags
    private float countFrom = 20.0f;
    Color32 stillOkColor = new Color32(0, 255, 0, 255);
    Color32 middleTimeColor = new Color32(255, 206, 0, 255);
    Color32 criticalTimeColor = new Color32(255, 0, 0, 255);
    // Object references
    public GameObject shrinkingRectangle;
    public Text timerText;
    // Variables
    private float timerValue;
    private bool counting = false;
    // Consts
    private float timerWidth;
    private float timeToWidthRatio;
    private float middleTime;
    private float criticalTime;

    private void Start()
    {
        // Set constants
        timerWidth = this.GetComponent<RectTransform>().rect.width;
        timeToWidthRatio = timerWidth / countFrom;
        middleTime = countFrom * 0.5f;
        criticalTime = countFrom * 0.2f;
        // Set the color
        shrinkingRectangle.GetComponent<Image>().color = stillOkColor;
        // Set timer value
        timerValue = countFrom;
        // Set timer text
        timerText.text = timerValue.ToString();
        counting = true;
    }

    // Update is called once per frame
    void Update () {
        if (counting && timerValue > 0.0f)
        {
            // Timer goes down
            timerValue -= Time.deltaTime;
            // Update the text
            timerText.text = timerValue.ToString("n1");
            // Shrink the rectangle
            RectTransform rectTransform = shrinkingRectangle.GetComponent<RectTransform>();
            rectTransform.offsetMax = new Vector2(-(countFrom - timerValue) * timeToWidthRatio, 0);
            // Change to critical color when we're in critical time
            if (timerValue <= middleTime && criticalTime < timerValue)
            {
                shrinkingRectangle.GetComponent<Image>().color = middleTimeColor;
            }
            else if (timerValue <= criticalTime)
            {
                shrinkingRectangle.GetComponent<Image>().color = criticalTimeColor;
            }
        }
        else if (timerValue <= 0.0f) TimerEnded();
    }

    public void StartTimer()
    {
        counting = true;
    }

    public void TimerEnded()
    {

    }
}
