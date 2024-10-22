using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public bool canLook = true;

    public float mouseSensitivity = 100f; //set smouse sesitivity when looking around
    public Transform playerBody;  //accesess the player body to rotate it 
    float xRotation = 0f;  
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  //locks cursor in place on screen
    }

    // Update is called once per frame
    void Update()
    {
        if (canLook)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //creates a float for the rotation of the camera on the x axis
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; //creates a float for the rotation of the camera on the y axis
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); //clamps the x rotation so u cant do flips
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  //wtf is a quaternion
            playerBody.Rotate(Vector3.up * mouseX); //rotates the player on the x axisto face towards where the camera is
        }
    }
    public void disableLook()
    {
        canLook = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void enableLook()
    {
        canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
