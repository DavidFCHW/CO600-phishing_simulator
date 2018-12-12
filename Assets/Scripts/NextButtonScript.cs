using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButtonScript : MonoBehaviour {

    public GameObject panel;
    public CameraMovementScript cameraMovement;

    public void OnClick()
    {
        cameraMovement.UnBlock();
        Destroy(panel);
    }
}
