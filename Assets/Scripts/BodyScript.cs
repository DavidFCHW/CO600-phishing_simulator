using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyScript : MonoBehaviour {

    public ClickOnEmailScript[] emailSubjects;

    private void Awake()
    {
        for (int i = 0; i < emailSubjects.Length; i++)
        {
            emailSubjects[i].setEmailBodiesContainer(this);
        }
    }

    public void hideAllEmailBodiesAndUnHighlightSubject() {

        for (int i = 0; i < emailSubjects.Length; i++)
        {
            emailSubjects[i].emailBody.transform.localScale = new Vector3(0, 0, 0);
            emailSubjects[i].gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }
}
