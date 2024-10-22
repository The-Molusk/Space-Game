using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holdRotation : MonoBehaviour
{
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;
    void Update()
    {
        gameObject.transform.rotation = new Quaternion(x, y, z, 0);
    }
}
