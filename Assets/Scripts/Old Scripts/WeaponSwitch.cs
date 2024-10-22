using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public bool[] isWeaponUnlocked;
    [SerializeField] int totalWeapons;

    public int selectedWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        isWeaponUnlocked = new bool[totalWeapons];
        selectWeapon(selectedWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        int prevSltWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
            
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }
        if (prevSltWeapon != selectedWeapon)
        {
            selectWeapon(selectedWeapon);
        }
    }
    
    public void selectWeapon(int selectedWeapon)
    {
        bool canRun = weaponUnlocked();
        if (isWeaponUnlocked[selectedWeapon] == true && canRun)
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
                if (i == selectedWeapon)
                {
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
                i++;
            }
        }
        else
        {
            return;
        }
        
    }

    private bool weaponUnlocked()
    {
        for (int i = 0; (i + 1) < totalWeapons; i++)
        {
            if (isWeaponUnlocked[i] == true)
            {
                return true;
            }
        }
        return false;
    }
}
