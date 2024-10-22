using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    moneySystem cash;
    GameObject cashObject;
    int addCash;
    private void Start()
    {
        cash = GameObject.FindGameObjectWithTag("moneySystem").GetComponent<moneySystem>();
    }
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "cash")
        {
            addCash = Random.Range(50, 100);
            Debug.Log("Pickup" + addCash.ToString());
            cash.addMoney(addCash);

            cashObject = hit.gameObject;
            Destroy(cashObject);
        }
    }

}
