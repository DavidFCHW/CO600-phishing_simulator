using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyScript : MonoBehaviour {
    // File constants
    private Color32 normalEmailPreviewColor = new Color32(255, 255, 255, 100);

    public ClickOnEmailScript[] emailSubjectsArray;
    public HashSet<ClickOnEmailScript> emailSubjects;

    private void Awake()
    {
        emailSubjects = new HashSet<ClickOnEmailScript>();
        for (int i = 0; i < emailSubjectsArray.Length; i++)
        {
            emailSubjects.Add(emailSubjectsArray[i]);
            emailSubjectsArray[i].setEmailBodiesContainer(this);
        }
    }

    public void hideAllEmailBodiesAndUnHighlightSubject() {

        foreach (ClickOnEmailScript emailBody in emailSubjects)
        {
            emailBody.emailBody.transform.localScale = new Vector3(0, 0, 0);
            emailBody.gameObject.GetComponent<Image>().color = normalEmailPreviewColor;
        }
    }
}
