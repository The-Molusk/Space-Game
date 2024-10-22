using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterCar : MonoBehaviour
{
    [SerializeField] GameObject carCam;
    [SerializeField] GameObject mapCam;
    [SerializeField] GameObject player;
    [SerializeField] float interactRadius = 10f;
    [SerializeField] Transform playerHolder;

    public bool isInCar;

    // Start is called before the first frame update
    void Start()
    {
        carCam.SetActive(false);
        gameObject.GetComponent<CarController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isInCar && (Vector3.Distance(transform.position, player.transform.position) < interactRadius))
            {
                getInCar();
            }
            else if (isInCar)
            {
                getOutCar();
            }
        }
    }
    void getInCar()
    {
        
        player.transform.parent = gameObject.transform;
        player.SetActive(false);
        carCam.SetActive(true);
        mapCam.SetActive(true);
        gameObject.GetComponent<CarController>().enabled = true;
        player.gameObject.GetComponent<playerMovement>().enabled = false;
        isInCar = true;
    }
    void getOutCar()
    {
        player.SetActive(true);
        player.transform.parent = playerHolder;
        carCam.SetActive(false);
        mapCam.SetActive(false);
        gameObject.GetComponent<CarController>().enabled = false;
        player.gameObject.GetComponent<playerMovement>().enabled = true;
        isInCar = false;
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
        
    }
}
