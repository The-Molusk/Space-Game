using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwap : MonoBehaviour
{
    public GameObject playerTracker;
    public GameObject fpsCam;

    // Update is called once per frame
    void Update()
    {
        // Check if the 'F' key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            // Perform a raycast from the position and direction of the first-person camera
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2f))
            {
                // Check if the raycast hits an object with the tag "character"
                if (hit.collider.CompareTag("character"))
                {
                    // Get the parent game object of the hit object
                    GameObject targetCharacter = hit.collider.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                    // Log the name of the target character
                    Debug.Log("Swapping to " + targetCharacter.name);
                    // Call the Swap method with the target character as the parameter
                    Swap(targetCharacter);
                }
            }
        }
    }

    // Method to swap the player and AI controls between characters
    void Swap(GameObject target)
    {
        // Get the player control and AI control game objects of the target character
        GameObject targetPlayerControl = target.transform.GetChild(0).gameObject;
        GameObject targetAiControl = target.transform.GetChild(1).gameObject;

        // Get the player control and AI control game objects of the current character
        GameObject selfPlayerControl = transform.parent.gameObject.transform.GetChild(0).gameObject;
        GameObject selfAiControl = transform.parent.gameObject.transform.GetChild(1).gameObject;

        targetPlayerControl.SetActive(true);
        targetAiControl.SetActive(false);

        selfAiControl.SetActive(true);
        selfPlayerControl.SetActive(false);

        // Set the position of the current AI control to match the position of the current player control
        selfAiControl.transform.position = selfPlayerControl.transform.position;

        playerTracker tracker = playerTracker.GetComponent<playerTracker>();

        // Update global player tracker to target character
        tracker.setPlayer(targetPlayerControl.transform.parent.gameObject);
    }
}
