using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCamController : MonoBehaviour
{
    // Sensitivity of the mouse movement
    public float mouseSensitivity = 100f;

    // Rotation around the X-axis (vertical rotation)
    private float xRotation = 0f;

    // Reference to the player object
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse movement delta
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player horizontally
        player.transform.Rotate(Vector3.up * mouseX);

        // Calculate the vertical rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate the camera vertically
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
