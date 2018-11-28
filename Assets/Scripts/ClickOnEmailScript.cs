using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnEmailScript : MonoBehaviour {

    private BodyScript emailBodiesContainer;
    public GameObject emailBody;

    public void OnClick() {
        emailBodiesContainer.hideAllEmailBodiesAndUnHighlightSubject();
        emailBody.transform.localScale = new Vector3(1, 1, 1);
        this.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void setEmailBodiesContainer(BodyScript emailBodiesContainer)
    {
        this.emailBodiesContainer = emailBodiesContainer;
    }
}
