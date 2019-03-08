using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelScript : MonoBehaviour
{
    private GameScript gameScript;

    public void SetGameScript(GameScript gameScript)
    {
        this.gameScript = gameScript;
    }

    public void ResumeClicked()
    {
        gameScript.UnPause();
    }

    public void QuitClicked()
    {
        gameScript.QuitButtonPressed();
    }
}
