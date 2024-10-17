using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwap : MonoBehaviour
{
    public GameObject fpsCam;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2f))
            {
                if (hit.collider.CompareTag("character"))
                {
                    GameObject targetCharacter = hit.collider.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                    Debug.Log("Swapping to " + targetCharacter.name);
                    Swap(targetCharacter);
                }
            }
        }
    }

    void Swap(GameObject target)
    {
        GameObject targetPlayerControl = target.transform.GetChild(0).gameObject;
        GameObject targetAiControl = target.transform.GetChild(1).gameObject;

        GameObject selfPlayerControl = transform.parent.gameObject.transform.GetChild(0).gameObject;
        GameObject selfAiControl = transform.parent.gameObject.transform.GetChild(1).gameObject;

        targetPlayerControl.SetActive(true);
        targetAiControl.SetActive(false);

        selfAiControl.SetActive(true);
        selfPlayerControl.SetActive(false);
        selfAiControl.transform.position = selfPlayerControl.transform.position;
    }
}
