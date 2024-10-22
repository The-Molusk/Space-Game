using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostFall : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GameObject lightSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.Sleep();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            rb.WakeUp();
            lightSource.SetActive(false);
        }
    }
}
