using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionCheck : MonoBehaviour
{
    public bool isTouching;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "wall")
        {
            isTouching = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isTouching = false;
    }
}
