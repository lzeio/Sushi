using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwables : MonoBehaviour
{

    public float throwForce = 300f;
    public GameObject grenade;
    public Transform attackpoint;
    Transform Weapon;

    public int grenadeCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G) && grenadeCounter>0)
        {
            Boom();
            grenadeCounter--;
        }
    }

    void Boom()
    {   
        GameObject throwed = Instantiate(grenade, attackpoint.position, attackpoint.rotation);  
        Rigidbody rb = throwed.GetComponent<Rigidbody>();
        rb.AddForce(attackpoint.transform.forward*throwForce,ForceMode.VelocityChange);
    }

   public void GrenadePickUp()
    {
        grenadeCounter++;
    }
}
