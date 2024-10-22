using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFlip : MonoBehaviour
{
    
    [SerializeField] GameObject carCam;
    // Update is called once per frame
    
    void Update()
    {
        if (gameObject.transform.eulerAngles.z > 90 && gameObject.transform.eulerAngles.z < 270)
        {
            if(carCam.active)
            {
                if (Input.GetKey(KeyCode.F))
                {
                    gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                    gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                }
                Debug.Log("Beanus");
            }
            
        }
        
    }
}
