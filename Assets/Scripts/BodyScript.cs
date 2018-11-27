using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyScript : MonoBehaviour {

    public ClickOnEmailScript[] emailSubjectsArray;
    public HashSet<ClickOnEmailScript> emailSubjects;

    private void Awake()
    {
        emailSubjects = new HashSet<ClickOnEmailScript>();
        //foreach (Transform child in transform)
        //{
        //    print("Foreach loop: " + child.gameObject);
        //    print("Foreach loop: " + child.gameObject.GetComponent(typeof(ClickOnEmailScript)));
        //    ClickOnEmailScript childScript = (ClickOnEmailScript) child.gameObject.GetComponent(typeof(ClickOnEmailScript));
        //    childScript.setEmailBodiesContainer(this);
        //    emailSubjects.Add(childScript);
        //}
        for (int i = 0; i < emailSubjectsArray.Length; i++)
        {
            emailSubjects.Add(emailSubjectsArray[i]);
            emailSubjectsArray[i].setEmailBodiesContainer(this);
        }
        //foreach (ClickOnEmailScript child in emailSubjects)
        //{
        //    child.setEmailBodiesContainer(this);
        //}
    }

    public void hideAllEmailBodiesAndUnHighlightSubject() {

        //for (int i = 0; i < emailSubjects.Length; i++)
        //{
        //    emailSubjects[i].emailBody.transform.localScale = new Vector3(0, 0, 0);
        //    emailSubjects[i].gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        //}
        foreach (ClickOnEmailScript emailBody in emailSubjects)
        {
            emailBody.emailBody.transform.localScale = new Vector3(0, 0, 0);
            emailBody.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }
}
