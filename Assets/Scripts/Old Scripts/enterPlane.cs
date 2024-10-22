using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterPlane : MonoBehaviour
{
    [SerializeField] GameObject shipCam;
    [SerializeField] GameObject player;
    [SerializeField] float interactRadius = 10f;
    [SerializeField] Transform playerHolder;

    public bool isInShip;


    // Start is called before the first frame update
    void Start()
    {
        
        shipCam.SetActive(false);
        gameObject.GetComponent<PlaneController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isInShip && (Vector3.Distance(transform.position, player.transform.position) < interactRadius))
            {
                getInShip();
            }
            else if (isInShip)
            {
                getOutShip();
            }
        }
    }
    void getInShip()
    {
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player.transform.parent = gameObject.transform;
        player.SetActive(false);
        shipCam.SetActive(true);
        gameObject.GetComponent<PlaneController>().enabled = true;
        player.gameObject.GetComponent<playerMovement>().enabled = false;
        isInShip = true;
    }
    void getOutShip()
    {
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.SetActive(true);
        player.transform.parent = playerHolder;
        shipCam.SetActive(false);
        gameObject.GetComponent<PlaneController>().enabled = false;
        player.gameObject.GetComponent<playerMovement>().enabled = true;
        isInShip = false;
        player.transform.rotation = new Quaternion(0, 0, 0, 0);

    }
}
