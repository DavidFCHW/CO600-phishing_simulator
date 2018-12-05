using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnEmailScript : MonoBehaviour {
    // File contants
    private Color32 emailClickedOnColor = new Color32(255, 255, 255, 255);

    private BodyScript emailBodiesContainer;
    public GameObject emailBody;

    public void OnClick() {
        emailBodiesContainer.hideAllEmailBodiesAndUnHighlightSubject();
        emailBody.transform.localScale = new Vector3(1, 1, 1);
        this.gameObject.GetComponent<Image>().color = emailClickedOnColor;
        this.gameObject.GetComponent<DragScript>().clickOn();
    }

    public void setEmailBodiesContainer(BodyScript emailBodiesContainer)
    {
        this.emailBodiesContainer = emailBodiesContainer;
    }
}
