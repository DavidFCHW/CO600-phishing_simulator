using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AcceptPhishingScript : MonoBehaviour {

	public void OnClick()
    {
        Debug.Log("Loading Email Client scene...");
        SceneManager.LoadScene("Email Client");
    }
}
