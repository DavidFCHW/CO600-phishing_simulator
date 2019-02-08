using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour
{
    public bool blocked = true;
    public enum RotationAxis
    {
        MouseX = 1, MouseY = 2
    }
    public RotationAxis axes = RotationAxis.MouseX;
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;
    public float minimumHori = -45.0f;
    public float maximumHori = 45.0f;
    public float sensHorizontal = 5.0f; //the sensitivity of camera movement horizontally.
    public float sensVertical = 5.0f; //the sensitivity of camera movement vertically.
    public float _rotationX = 0;
    public float _rotationY = 0;
    // Update is called once per frame
    public void Update()
    {
        if (!blocked)
        {
            if (axes == RotationAxis.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensHorizontal, 0);
                _rotationY -= Input.GetAxis("Mouse X") * sensHorizontal;
                _rotationX = Mathf.Clamp(_rotationX, minimumHori, maximumHori);
            }
            else if (axes == RotationAxis.MouseY)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensHorizontal, 0);
                _rotationX -= Input.GetAxis("Mouse Y") * sensVertical;
                _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert); //Clamps the vertical angle within the min and max variables.
                float rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(_rotationX, rotationY);
            }
        }
    }

    public void UnBlock()
    {
        blocked = false;
    }

    public void Block()
    {
        blocked = true;
    }
}