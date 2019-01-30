using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour {

    public AudioSource clickSound;

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //either this
        clickSound.Play();
        SceneManager.LoadScene("Office"); //Or this one...
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
    }
}