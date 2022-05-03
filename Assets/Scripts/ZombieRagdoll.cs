using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRagdoll : MonoBehaviour
{
    public static ZombieRagdoll Instance;

    public Collider[] ragColliders;
    public Rigidbody[] ragRigidbodies;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ragColliders = GetComponentsInChildren<Collider>();
        ragRigidbodies = GetComponentsInChildren<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RagdollOFF()
    {
        foreach (Collider c in ragColliders)
        {
            if (!c.CompareTag("Zombie")||!c.CompareTag("AttackPoint"))
            {
                c.enabled = false;
            }
        }
        foreach (Rigidbody r in ragRigidbodies)
        {
            if (!r.CompareTag("Zombie"))
            {
                r.isKinematic = true;
            }
        }
    }
    public void RagdollON()
    {
        foreach (Collider c in ragColliders)
        {
            c.enabled = true;

        }
        foreach (Rigidbody r in ragRigidbodies)
        {
            if (!r.CompareTag("Zombie"))
            {
                r.isKinematic = false;
            }
        }
    }
}
