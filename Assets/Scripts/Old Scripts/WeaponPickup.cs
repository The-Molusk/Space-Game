using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    WeaponUnlock unlockScript;
    WeaponSwitch switchScript;

    [SerializeField] int WeaponID;
    [SerializeField] float interactRadius;

    [SerializeField] GameObject player, pickupText, weaponHolder;


    // Start is called before the first frame update
    void Start()
    {
        unlockScript = weaponHolder.GetComponent<WeaponUnlock>();
        switchScript = weaponHolder.GetComponent<WeaponSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(transform.position, player.transform.position) < interactRadius)
        {
            pickupText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                unlockScript.Unlock(WeaponID);
                switchScript.selectWeapon(WeaponID);
                pickupText.SetActive(false);
                gameObject.SetActive(false);
            }
        }
        else
        {
            pickupText.SetActive(false);
        }
    }
}
