using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButtonScript : MonoBehaviour {

    public GameObject panel;
    public Camera_movement camera_Movement;

    public void OnClick()
    {
        camera_Movement.blocked = false;
        Destroy(panel);
    }
}
