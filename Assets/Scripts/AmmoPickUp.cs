using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public GunSystem gunScript;
    public Transform player;

    public int addedAmmo;
    public float pickupRange;
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
            PickUpAmmo();
        }
    }

    public void PickUpAmmo()
    {
        gunScript.gunData.totalBullets += addedAmmo;
        gameObject.SetActive(false  );
    }


}
