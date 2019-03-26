using UnityEngine;
using System.Collections;

public class FirstPersonCameraScript : MonoBehaviour {

    public float speedH;
    public float speedV;

    private float _yaw;
    private float _pitch;
    private bool _rotationBlocked;
    
    private bool _mouseKeyDown;
    
    public Texture2D cursorClosedHandTexture;
    public Texture2D cursorOpenHandTexture;

    private void Start()
    {
        Cursor.SetCursor(cursorOpenHandTexture, Vector2.zero, CursorMode.Auto);
    }

    private void Update ()
    {   
        if (_rotationBlocked) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _mouseKeyDown = true;
            Cursor.SetCursor(cursorClosedHandTexture, Vector2.zero, CursorMode.Auto);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _mouseKeyDown = false;
            Cursor.SetCursor(cursorOpenHandTexture, Vector2.zero, CursorMode.Auto);
        }

        if (!_mouseKeyDown) return;
        _yaw += speedH * Input.GetAxis("Mouse X") * Time.deltaTime;
        _pitch -= speedV * Input.GetAxis("Mouse Y") * Time.deltaTime;
        transform.eulerAngles = new Vector3(-_pitch, -_yaw, 0.0f);
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