using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] float damage = 10f;
    [SerializeField, Range(0f, 1000f)] float range = 100f;
    [SerializeField, Range(0f, 50f)] float fireRate = 5f;
    [SerializeField, Range(0f, 1f)] float accuracy = 0.2f;
    [SerializeField, Range(0f, 100f)] int shotCount = 1;


    [SerializeField] bool automatic;
    float nextTimeToFire = 0f;

    public Camera camera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject fireDelayText;
    public GameObject dispersionPoint;
    AudioSource audioSource;
    public AudioClip fire;
    Animator animator;
    public GameObject EventSystem;
    GlobalValues Global;

    private void Start()
    {
        Global = EventSystem.GetComponent<GlobalValues>();
        audioSource = GetComponent<AudioSource>();
        try
        {
            animator = GetComponent<Animator>();
        }
        catch { }
    }
    // Update is called once per frame
    void Update()
    {

        if ((Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire) && !automatic)
        {
            nextTimeToFire = Time.time + (1f / fireRate);
            int currentShotCount = 0;
            while (currentShotCount < shotCount)
            {
                Shoot();
                currentShotCount++;
            }
        }
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire && automatic)
        {
            nextTimeToFire = Time.time + (1f / fireRate);
            int currentShotCount = 0;
            while (currentShotCount < shotCount)
            {
                Shoot();
                currentShotCount++;
            }
            
        }
        if (nextTimeToFire - Time.time <= 0)
        {
            fireDelayText.GetComponent<TMPro.TextMeshProUGUI>().text = "0";
        }
        else
        {
            fireDelayText.GetComponent<TMPro.TextMeshProUGUI>().text = (nextTimeToFire - Time.time).ToString();
        }
        if (Global.moveCheck == true)
        {
            animator.SetBool("isMoving", true);
        }
        else animator.SetBool("isMoving", false);
    }
    void Shoot()
    {
        try
        {
            muzzleFlash.Play();
            audioSource.PlayOneShot(fire);
            animator.SetTrigger("isShoot");
        }
        catch (Exception e) { }
        RaycastHit hitInfo;
        
        

        float[] temp = new float[3];
        int[] direction = new int[3];
        temp[0] = UnityEngine.Random.value;
        temp[1] = UnityEngine.Random.value;
        temp[2] = UnityEngine.Random.value;
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i] > 0.5)
            {
                direction[i] = -1;
            }
            else direction[i] = 1;
        }
        float xShot = UnityEngine.Random.value * accuracy * direction[0];
        float yShot = UnityEngine.Random.value * accuracy * direction[1];
        float zShot = UnityEngine.Random.value * accuracy * direction[2];

        Vector3 shotLand = new Vector3(dispersionPoint.transform.position.x + xShot, dispersionPoint.transform.position.y + yShot, dispersionPoint.transform.position.z + zShot);
        if (Physics.Raycast(camera.transform.position, shotLand - camera.transform.position, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("HIT!");
                enemy.takeDamage(damage);
            }
            try
            {
                Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            }
            catch (Exception e) { }
        }
    }
}
