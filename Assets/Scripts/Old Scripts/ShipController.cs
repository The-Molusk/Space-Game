using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float forwardSpeed = 25f, turnSpeed = 7.5f;
    public float currentForwardSpeed;
    public float forwardAccel = 2.5f, turnAccel = 2f;

    collisionCheck check; //has bool isTouching to check if ship is touching any other colliders

    public float lookRotateSpeed = 90f;
    private Vector2 lookInput, screenCentre, mouseDistance;
    
    void Start()
    {
        check = this.gameObject.GetComponent<collisionCheck>();
        screenCentre.x = Screen.width * 0.5f;
        screenCentre.y = Screen.height * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCentre.x) / screenCentre.y;
        mouseDistance.y = (lookInput.y - screenCentre.y) / screenCentre.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        transform.Rotate(mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, 0f, Space.Self);

        currentForwardSpeed = Mathf.Lerp(currentForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAccel * Time.deltaTime);
        
        
        transform.position = transform.position + (transform.forward * currentForwardSpeed * Time.deltaTime * -1);
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, turnSpeed * Time.deltaTime * -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }
    }
}
