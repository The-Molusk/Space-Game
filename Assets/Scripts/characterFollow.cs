using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class characterFollow : MonoBehaviour
{
    public GameObject fpsCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2f))
            {
                if (hit.collider.CompareTag("character"))
                {
                    // Get the parent of the hit object
                    GameObject targetCharacter = hit.collider.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                    Debug.Log(targetCharacter.name + " follow toggled");
                    SetToFollow(targetCharacter);
                }
            }
        }
    }

    // Set the target character to follow or stop following
    public void SetToFollow(GameObject target)
    {
        // Get the AI controller of the target character
        GameObject targetAiController = target.transform.GetChild(1).gameObject;
        FollowPlayer navController = targetAiController.GetComponent<FollowPlayer>();
        navController.doFollow = !navController.doFollow;
    }
}
