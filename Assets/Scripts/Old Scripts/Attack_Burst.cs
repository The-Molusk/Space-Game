using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Burst : MonoBehaviour
{
    private float time;
    private bool shoot;
    private float burstTime = 0.1f;
    private int burstCount = 3;

    GameObject target;
    GameObject bullet;
    Transform shootPoint;
    float bulletVelocity;
    int damage;
    GameObject body;


    private void Update()
    {
        if (shoot)
        {
            time += Time.deltaTime;
            if (time > burstTime && burstCount>0)
            {
                time = 0;
                Debug.Log("Shoot");
                Shoot();
            }
        }
        
    }
    private void Shoot()
    {
        Vector3 targetPosition = target.transform.position;
        targetPosition.y -= 2;
        GameObject currentBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
        Vector3 direction = targetPosition - body.gameObject.transform.position;
        rb.AddForce(direction * bulletVelocity, ForceMode.VelocityChange);
        BulletHit hit = currentBullet.GetComponent<BulletHit>();
        hit.SetDamage(damage);
        burstCount--;
    }
    public void ShootAt(GameObject target, GameObject bullet, Transform shootPoint, float bulletVelocity, int damage, GameObject body)
    {
        this.target = target;
        this.bullet = bullet;
        this.bulletVelocity = bulletVelocity;
        this.shootPoint = shootPoint;
        this.damage = damage;
        this.body = body;
        shoot = true;

        //animation here
    }
}
