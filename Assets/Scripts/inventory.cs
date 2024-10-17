using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class inventory : MonoBehaviour
{
    [SerializeField] bool[] heldObjects;
    // Start is called before the first frame update
    void Start()
    {
        string[] objects = AssetDatabase.FindAssets("*", new[] { "Assets/Items" });
        heldObjects = new bool[objects.Length];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
