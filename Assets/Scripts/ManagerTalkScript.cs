using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTalkScript : MonoBehaviour {

    // Flags
    private Vector3 hiddenScale = new Vector3(0, 0, 0);
    private Vector3 normalScale = new Vector3(1, 1, 0);

    public CameraMovementScript mainCam;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    // Button in text box 1 is clicked
    public void OnClick1()
    {
        // Hide text box 1
        text1.transform.localScale = hiddenScale;
        // Move around a bit
        //mainCam.RotateHorizontal(5);
        // Show text2
        text2.transform.localScale = normalScale;
    }

    // 
    public void OnClick2()
    {
        text2.transform.localScale = hiddenScale;
        text3.transform.localScale = normalScale;
    }

    public void OnClick3()
    {
        text3.transform.localScale = hiddenScale;
        Destroy(gameObject);
        mainCam.UnBlock();
        mainCam.LockCursor();
    }
}
