using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
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
                if (hit.collider.CompareTag("item"))
                {
                    Debug.Log("picked up "+ hit.collider.name);
                    transform.parent.GetComponent<Inventory>().pickUpItem(hit.collider.gameObject);
                }
            }
        }
    }
}
