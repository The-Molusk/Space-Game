using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentreOfMass : MonoBehaviour
{
    public Vector3 massCentre;
    protected Rigidbody r;
    // Start is called before the first frame update
    void Start()
    {
        r = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        r.centerOfMass = massCentre;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * massCentre, 0.2f);
    }
}
