using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScreenScript : MonoBehaviour {

	public void onClick(){
        Debug.Log("Loading next scene");
        SceneManager.LoadScene("Office");
    }
}
