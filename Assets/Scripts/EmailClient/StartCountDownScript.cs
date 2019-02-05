using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class representing the 3... 2... 1... countdown before the game starts
 */
public class StartCountDownScript : MonoBehaviour
{
    public GameObject[] elements;
    private GameScript gameScript;
    public AudioSource countdownLastBeep;

    private void Start()
    {
        foreach (GameObject element in elements)
        {
            element.SetActive(false);
        }
        this.gameObject.SetActive(true);
    }

    public void SetGameScript(GameScript gameScript)
    {
        this.gameScript = gameScript;
    }

    /*
     * Countdown before game starts
     */
    public void StartCountdown()
    {
        StartCoroutine(CountdownAllElements());
    }

    IEnumerator CountdownAllElements()
    {
        foreach (GameObject element in elements)
        {
            element.SetActive(true);
            element.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1);
            element.SetActive(false);
        }
        countdownLastBeep.Play();
        yield return new WaitForSeconds(0.2f);
        gameScript.CountdownDone();
    }
}
