using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGun : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] float damage = 10f;
    [SerializeField, Range(0f, 1000f)] float range = 100f;
    [SerializeField, Range(0f, 100f)] float fireRate = 15f;
    float nextTimeToFire = 0f;

    public Camera camera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject fireDelayText;
    AudioSource audioSource;
    public AudioClip fire;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + (1f / fireRate);
            Shoot();
        }
        if (nextTimeToFire - Time.time <= 0)
        {
            fireDelayText.GetComponent<TMPro.TextMeshProUGUI>().text = "0";
        }
        else
        {
            fireDelayText.GetComponent<TMPro.TextMeshProUGUI>().text = (nextTimeToFire - Time.time).ToString();
        }

    }
    void Shoot()
    {
        muzzleFlash.Play();
        audioSource.PlayOneShot(fire);
        
        RaycastHit hitInfo;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, range))
        {

            Debug.Log(hitInfo.transform.name);

            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("HIT!");
                enemy.takeDamage(damage);
            }
            Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        }
    }
}
