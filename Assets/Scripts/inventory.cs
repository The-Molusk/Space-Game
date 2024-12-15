using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public newItem[] activeItems;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allItems = Resources.LoadAll<GameObject>("Items");
        Debug.Log(allItems.Length + " items found in resources.");
        activeItems = new newItem[allItems.Length];
        for (int i = 0; i < allItems.Length; i++)
        {
            activeItems[i] = new newItem(allItems[i], false);
        }
    }

    public void pickUpItem(GameObject item)
    {
        foreach (newItem i in activeItems)
        {
            if (i.item.name == item.name)
            {
                i.active = true;
                break;
            }
        }
    }

    public GameObject[] getActiveItems()
    {
        List<GameObject> items = new List<GameObject>();
        foreach (newItem i in activeItems)
        {
            if (i.active)
            {
                items.Add(i.item);
            }
        }
        return items.ToArray();
    }
}

[Serializable]
public class newItem
{
    public GameObject item;
    public bool active;
    public newItem(GameObject item, bool active)
    {
        this.item = item;
        this.active = active;
    }
}
