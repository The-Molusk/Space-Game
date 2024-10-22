using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FunnyCarCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            RagdollToggle rToggle = gameObject.GetComponent<RagdollToggle>();
            BoxCollider bColl = gameObject.GetComponent<BoxCollider>();
            rToggle.isRagdoll = true;
            bColl.enabled = false;
            GameObject parent = gameObject.transform.parent.gameObject;
            NavMeshAgent navAgent = parent.GetComponent<NavMeshAgent>();
            navAgent.enabled = false;
        }
    }
}
