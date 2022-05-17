using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public GunSystem gunScript;
    public Rigidbody rb;
    public BoxCollider collider;
    public Transform player, gunContainer, fpsCam;

    public float pickupRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;

    public static bool slotFull;

    // Start is called before the first frame update
    void Start()
    {
        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            collider.isTrigger = true;
        }
        if(!equipped)
        {
            gunScript.enabled=false;
            rb.isKinematic = false;
            collider.isTrigger=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position; 

        if(!equipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
        if(equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }
    public void PickUp()
    {
        equipped = true;


        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
        rb.isKinematic = true;
        collider.isTrigger = true;


        gunScript.enabled = true;
    }

    public void Drop()
    {
        equipped = false;

        transform.SetParent(null);


        rb.isKinematic = false;
        collider.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;


        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up *dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random,random, random)*10);
        gunScript.enabled = false;
    }
}
