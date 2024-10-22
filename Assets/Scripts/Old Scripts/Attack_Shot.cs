using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Shot : MonoBehaviour
{
    public void ShootAt(GameObject target, GameObject bullet, Transform shootPoint, float bulletVelocity, int damage, GameObject body)
    {
        //animations here

        Vector3 targetPosition = target.transform.position;
        float targetDistance = Vector3.Distance(target.transform.position, body.transform.position);
        targetPosition.y -= 2;
        GameObject currentBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
        Vector3 direction = targetPosition - body.gameObject.transform.position;
        rb.AddForce(direction * bulletVelocity *(1/targetDistance), ForceMode.VelocityChange);
        BulletHit hit = currentBullet.GetComponent<BulletHit>();
        hit.SetDamage(damage);
    }
}
