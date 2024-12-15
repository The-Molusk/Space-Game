using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu, playerTracker, slotsHolder;

    private GameObject[] slots;
    private bool active;
    private GameObject currentPlayer, fpsCam;
    private void Start()
    {
        inventoryMenu.SetActive(active);

        slots = new GameObject[slotsHolder.transform.childCount];
        for (int i = 0; i < slotsHolder.transform.childCount; i++)
        {
            slots[i] = slotsHolder.transform.GetChild(i).gameObject;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            active = !active;
            inventoryMenu.SetActive(active);
            UnityEngine.Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
            UnityEngine.Cursor.visible = active;
            currentPlayer = playerTracker.GetComponent<playerTracker>().getPlayer();
            fpsCam = currentPlayer.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            fpsCam.GetComponent<FpsCamController>().enabled = !active;

            if (active)
            {
                GameObject[] items = currentPlayer.GetComponent<Inventory>().getActiveItems();
                Debug.Log(items.Length + " items found in inventory.");
                for (int i = 0; i < slots.Length; i++) 
                {
                    slots[i].GetComponent<UnityEngine.UI.Image>().sprite = null;

                }
                for (int i = 0; i < items.Length; i++)
                {
                    slots[i].GetComponent<UnityEngine.UI.Image>().sprite = items[i].GetComponent<InventoryItem>().getIcon();
                }
            }
        }
    }
}
