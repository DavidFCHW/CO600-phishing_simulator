using UnityEngine;
using System.Collections;

/**
 * Taken from https://gamedev.stackexchange.com/questions/104693/how-to-use-input-getaxismouse-x-y-to-rotate-the-camera
 */
public class CameraMovementScript : MonoBehaviour
{
    public float horizontalSpeed = 12F;
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

    /*
     * Perform a horizontal rotation of the camera
     */
    public void RotateHorizontal(int rotation)
    {
        transform.Rotate(0, rotation, 0);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.lockState = CursorLockMode.Confined;
    }
}
