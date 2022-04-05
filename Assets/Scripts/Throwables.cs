using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwables : MonoBehaviour
{

    public float throwForce = 300f;
    public GameObject grenade;
    public Transform attackpoint;
    Transform Weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Boom();
        }
    }

    void Boom()
    {   
        GameObject throwed = Instantiate(grenade, attackpoint.position, attackpoint.rotation);  
        Rigidbody rb = throwed.GetComponent<Rigidbody>();
        rb.AddForce(attackpoint.transform.forward*throwForce,ForceMode.VelocityChange);
    }
}
