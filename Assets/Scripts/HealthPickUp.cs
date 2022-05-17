using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{

    public float pickupRange;
    public float addedHealth;
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
            PickUpHealth();
        }
    }

    void PickUpHealth()
    {
        player.GetComponent<PlayerDeathDamage>().playerHealth += addedHealth;
        gameObject.SetActive(false);    
    }
}
