using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] Sprite icon;
    private GameObject Model;

    public string getName()
    {
        return name;
    }
    public string getDescription()
    {
        return description;
    }
    public Sprite getIcon()
    {
        return icon;
    }
    public GameObject getModel()
    {
        return this.gameObject;
    }
}
