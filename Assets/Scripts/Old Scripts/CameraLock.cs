using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    [SerializeField] private GameObject target;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 50, target.transform.position.z);
    }
}
