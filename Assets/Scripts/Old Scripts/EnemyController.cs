using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject destination;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPoint, hitPoint, rayCastPoint;
    [SerializeField] private Transform head;
    [SerializeField, Range(0f, 100f)] private float viewDistance_find, viewDistance_remain, shootDistance;
    [SerializeField, Range(0f, 25f)] private float walkSpeed, acceleration, bulletVelocity, shootTimePeriod, rotationDamping, meleeRange;
    [SerializeField] private int damage;
    [SerializeField] GameObject animatorBody;
    private Animator animator;

    [SerializeField] int attackType;
    //0-melee, 1-shot, 2-burst, 3-shotgun

    private float currentShootTime;
    private bool canMove, canShoot, lookAtPlayer, seen;
    // Start is called before the first frame update
    void Start()
    {
        currentShootTime = shootTimePeriod;
        agent.acceleration = acceleration;
        agent.speed = walkSpeed;
        animator = animatorBody.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (canShoot)
        {
            currentShootTime -= Time.deltaTime;
            if(currentShootTime <= 0)
            {
                currentShootTime = shootTimePeriod;
                Attack(destination);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = new RaycastHit();
        if (lookAtPlayer)
        {
            var lookPos = destination.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            head.LookAt(destination.transform);
        }
        if (Vector3.Distance(transform.position, destination.transform.position) < viewDistance_find) //is within vision range?
        {
            if (seen)
            {
                canMove = true;   //moves
                agent.speed = walkSpeed;
                lookAtPlayer = true;
            }
            else if (Physics.Raycast(destination.transform.position,destination.transform.position - transform.position, out hit)) //is within line of sight?
            {
                seen = true;
                canMove = true;   //moves
                agent.speed = walkSpeed;
                lookAtPlayer = true;
                animator.SetBool("Seen", true);
            }
        }
        if (Vector3.Distance(transform.position, destination.transform.position) > viewDistance_remain) //out of large sight area?
        {
            canMove=false;
            canShoot=false;
            lookAtPlayer=false;
            seen = false;
            animator.SetBool("Seen", false);
        }
        if (Vector3.Distance(transform.position, destination.transform.position) < shootDistance) //within shooting range?
        {
            Physics.Raycast(rayCastPoint.position, destination.transform.position - rayCastPoint.position, out hit);
            if (hit.transform.CompareTag("Player"))
            {
                canMove = false;
                canShoot = true;
                lookAtPlayer = true;
            }

        }
        else
        {
            canShoot = false;
        }
        if (canMove)
        {
            agent.SetDestination(destination.transform.position); //sets pathfindign location
        }
        else
        {
            agent.speed = 0; //stops
        }
    }
    private void Attack(GameObject target)
    {
        switch (attackType)
        {
            case 0:
                animator.SetTrigger("Melee");
                Attack_Melee melee = gameObject.AddComponent<Attack_Melee>();
                melee.Hit(target, damage, this.gameObject, hitPoint, meleeRange);
                break;
            case 1:
                Attack_Shot shot = gameObject.AddComponent<Attack_Shot>();
                shot.ShootAt(target, bullet, shootPoint, bulletVelocity, damage, this.gameObject);
                break;
            case 2:
                Attack_Burst burst = gameObject.AddComponent<Attack_Burst>();
                burst.ShootAt(target, bullet, shootPoint, bulletVelocity, damage, this.gameObject);
                break;
        }
    }
}
