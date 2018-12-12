using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour
{
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    private bool blocked = true;

    void Update()
    {
        if (!blocked) {
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            //float v = verticalSpeed * Input.GetAxis("Mouse Y");
            //transform.Rotate(v, h, 0);
            transform.Rotate(0, h, 0);
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