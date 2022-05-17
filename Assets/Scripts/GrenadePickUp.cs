using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickUp : MonoBehaviour
{


    public float pickupRange;
    public int addedGrenades;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceFromPlayer = player.position - transform.position;

        if (distanceFromPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUpGrenade();
        }
    }

    public void PickUpGrenade()
    {
        player.GetComponent<Throwables>().grenadeCounter += addedGrenades;
        gameObject.SetActive(false);
    }

}
