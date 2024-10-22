using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlock : MonoBehaviour
{
    WeaponSwitch switchScript;

    private void Start()
    {
        switchScript = gameObject.GetComponent<WeaponSwitch>();
    }
    public void Unlock(int weaponNumber)
    {
        switchScript.isWeaponUnlocked[weaponNumber] = true;
    }
}
