using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Attack_Melee : MonoBehaviour
{
    private float meleeDelay = 0.3f;
    private float currentTime;

    GameObject target;
    int damage;
    GameObject body;
    Transform hitPos;
    float hitRange;

    public void Hit(GameObject target, int damage, GameObject body, Transform hitPos, float hitRange)
    {
        currentTime = 0;

        this.target = target;
        this.damage = damage;
        this.body = body;
        this.hitPos = hitPos;
        this.hitRange = hitRange;
        //Vector3 displacement = target.transform.position - hitPos.transform.position;
        //float distance = displacement.magnitude;

        //if (distance < hitRange)
        //{
        //    Health targetHp = target.GetComponent<Health>();
        //    targetHp.TakeDamage(damage);
        //}
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > meleeDelay)
        {
            Attack(target, damage, body, hitPos, hitRange);
        }
    }
    private void Attack(GameObject target, int damage, GameObject body, Transform hitPos, float hitRange)
    {
        Vector3 displacement = target.transform.position - hitPos.transform.position;
        float distance = displacement.magnitude;

        if (distance < hitRange)
        {
            Health targetHp = target.GetComponent<Health>();
            if (targetHp.TakeDamage(damage))
            {
                targetHp.GiveIFrames(1);
            }
        }
        Destroy(this);
    }
}
