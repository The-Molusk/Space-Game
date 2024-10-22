//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class KnifeAttack : MonoBehaviour
//{
//    [SerializeField] GameObject camera;
//    [SerializeField] float damage, range;

//    private float nextTimeToHit = 0f;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
//        {
//            nextTimeToFire = Time.time + (1f / fireRate);
//            Shoot();
//        }
//        if (nextTimeToFire - Time.time <= 0)
//        {
//            fireDelayText.GetComponent<TMPro.TextMeshProUGUI>().text = "0";
//        }
//        else
//        {
//            fireDelayText.GetComponent<TMPro.TextMeshProUGUI>().text = (nextTimeToFire - Time.time).ToString();
//        }
//    }
//    void Hit()
//    {
//        RaycastHit hitInfo;
//        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, range))
//        {
//            Debug.Log(hitInfo.transform.name);

//            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
//            if (enemy != null)
//            {
//                Debug.Log("HIT!");
//                enemy.takeDamage(damage);
//            }
//        }
//    }
//}
