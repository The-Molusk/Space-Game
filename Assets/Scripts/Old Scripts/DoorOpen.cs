using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float interactRadius, X, Y, Z, rotationLimit;

    bool isOpen;
    void Update()
    {
        if ((Vector3.Distance(transform.position, player.transform.position) < interactRadius) && isOpen == false)
        {
            openDoor();
        }
        if (isOpen == true && (Vector3.Distance(transform.position, player.transform.position) > interactRadius))
        {
            closeDoor();
        }
        
    }
    void openDoor()
    {
        gameObject.transform.Rotate(X * Time.deltaTime, Y * Time.deltaTime, Z * Time.deltaTime);
        if (gameObject.transform.rotation.y <= rotationLimit)
        {
            isOpen = true;
        }
        
    }
    void closeDoor()
    {
        isOpen = false;
    }

}
