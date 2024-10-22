using UnityEngine;

public class Enemy : MonoBehaviour
{
    RagdollToggle toggle;

    [SerializeField] GameObject cashDrop;
    public float health = 100f;
    private void Start()
    {
        toggle = this.GetComponent<RagdollToggle>();
    }
    public ParticleSystem bloodSplash;
    public void takeDamage(float damageAmount)
    {
        bloodSplash.Play();
        health = health - damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        cashDrop.SetActive(true);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        toggle.activateRagdoll();
        //GFX.SetActive(false);
        
    }
    private void Update()
    {
        //healthBar.GetComponent<TMPro.TextMeshPro>().text = health.ToString();
    }
}
