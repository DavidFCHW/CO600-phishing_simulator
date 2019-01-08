using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

    public TimerScript timer;

    private void Start()
    {
        // Give your reference to other objects
        timer.SetGameScript(this);
    }

    public void TimerEnded()
    {

    }
}
