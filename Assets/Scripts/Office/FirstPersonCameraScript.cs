using UnityEngine;
using System.Collections;

public class FirstPersonCameraScript : MonoBehaviour {

    public float speedH;
    public float speedV;
    public GameObject cursor;

    private float _yaw;
    private float _pitch;
    private bool _rotationBlocked = false;

    private void Update () {
        if (_rotationBlocked) return;
        _yaw += speedH * Input.GetAxis("Mouse X") * Time.deltaTime;
        _pitch -= speedV * Input.GetAxis("Mouse Y") * Time.deltaTime;

        transform.eulerAngles = new Vector3(_pitch, _yaw, 0.0f);
    }

    public void Block()
    {
//        cursor.SetActive(false);
        _rotationBlocked = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnBlock()
    {
//        cursor.SetActive(true);
        _rotationBlocked = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}