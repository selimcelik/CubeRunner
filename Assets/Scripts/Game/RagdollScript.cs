using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{
    public bool die = false;

    [Header("References")]
    [SerializeField] private Animator animator = null;

    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    // Start is called before the first frame update

    void Start()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        ToggleRagdoll(false);

        gameObject.GetComponent<CapsuleCollider>().enabled = true;

        //Invoke("Die", 5);
    }

    private void Update()
    {
        if (die)
        {
            Die();
            die = false;
        }
    }

    private void Die()
    {
        ToggleRagdoll(true);

        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        /*foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = true;
        }*/
    }

    private void ToggleRagdoll(bool state)
    {
        animator.enabled = !state;

        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }

        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = state;
        }
        /*foreach(Rigidbody rb in gameObject.transform.GetChild(0).gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = !state;
        }
        foreach(Collider collider in gameObject.transform.GetChild(0).gameObject.GetComponentsInChildren<Collider>())
        {
            collider.enabled = state;
        }*/

    }
}
