using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour {
    public void PlayGame()
    {
        SceneManager.LoadScene("Office");
    }

    public void QuitGame()
    {
        Debug.Log("The game has been exited.");
        Application.Quit();
    }
}
