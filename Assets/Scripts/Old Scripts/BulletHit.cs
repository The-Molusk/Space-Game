using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    [SerializeField] float maxTime;

    private int damage;
    private float flyTime;

    private void Update()
    {
        flyTime += Time.deltaTime;
        if (flyTime > maxTime)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT");
        if (collision.gameObject.tag == "Player")
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth.TakeDamage(damage))
            {
                playerHealth.GiveIFrames(1f);
            }
        }
        if(collision.gameObject.tag != "enemy")
        {
            Destroy(this.gameObject);
        }
    }

    public void SetDamage(int value)
    {
        damage = value;
    }

}
