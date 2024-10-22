using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    [SerializeField] float requiredVeloity;

    public AudioSource enterCar;
    public AudioSource crashCarBig;
    public AudioSource crashCarSmall;
    public AudioSource accelerate;
    [SerializeField] GameObject player;
    [SerializeField] GameObject carCam;
    Rigidbody rb;
    //public AudioSource decelerate;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 2)
            {
                enterCar.Play();
            }
        }

        
        if (Input.GetKeyDown(KeyCode.W) && (carCam.active == true))
        {
            if(accelerate.isPlaying == false)
            {
                accelerate.Play();
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            accelerate.Stop();
            //decelerate.Play();
        }
    }
    public void StartCar()
    {
        enterCar.Play();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "wall" && (rb.velocity.magnitude > requiredVeloity))
        {
            crashCarBig.Play();
            accelerate.Stop();
        }
        else if (collision.gameObject.tag == "wall")
        {
            crashCarSmall.Play();
            accelerate.Stop();
        }
        else if(collision.gameObject.tag == "post")
        {
            crashCarSmall.Play();
        }
    }
}
