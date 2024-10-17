using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerTracker;
    playerTracker tracker;
    GameObject player;
    NavMeshAgent agent;
    public bool doFollow;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        tracker = playerTracker.GetComponent<playerTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        player = tracker.getPlayer().transform.GetChild(0).gameObject;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (doFollow && distanceToPlayer > 5)
        {
            agent.enabled = true;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.enabled = false;
        }
    }
}
