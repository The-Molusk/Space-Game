using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    float currentHealth;
    public int maxHealth;
    public GameObject healthText;
    private bool canDamage;
    private float invincibleTimer;
    public GameObject iTimer;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        invincibleTimer -= Time.deltaTime;
        if (invincibleTimer < 0)
        {
            canDamage = true;
            invincibleTimer = 0;
        }
        iTimer.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = invincibleTimer.ToString();
        if (currentHealth <= 0)
        {
            healthText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "DEAD";
        }
        else healthText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();
        if (canDamage)
        {
            healthText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            healthText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = Color.yellow;
        }
    }
    public bool TakeDamage(int amount)
    {
        if (canDamage)
        {
            currentHealth -= amount;
            canDamage = false;
            return true;
        }
        else return false;
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth < maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void GiveIFrames(float time)
    {
        canDamage = false;
        if (invincibleTimer < time)
        {
            invincibleTimer = time;
        }
    }
}
