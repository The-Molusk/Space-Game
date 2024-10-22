using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollToggle : MonoBehaviour
{
    public List<Collider> ragdollParts = new List<Collider>();
    public List<Rigidbody> ragdollRBS = new List<Rigidbody>();

    [SerializeField] public bool isRagdoll;
    [SerializeField] private GameObject mainBody, subBody;
    // Start is called before the first frame update
    void Start()
    {
        
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            if (c.gameObject != this.gameObject)
            {
                c.isTrigger = true;
                c.gameObject.SetActive(false);
                ragdollParts.Add(c);
            }
            
        }
    }
    public void activateRagdoll()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        foreach (Collider c in ragdollParts)
        {
            if (c.gameObject != this.gameObject)
            {
                c.isTrigger = false;
                c.gameObject.SetActive(true);
            }
        }
        EnemyController controller = mainBody.GetComponent<EnemyController>();
        controller.enabled = false;
        Animator enemyAnimtor = subBody.GetComponent<Animator>();
        enemyAnimtor.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isRagdoll)
        {
            activateRagdoll();
            isRagdoll = false;
        }
    }
}
