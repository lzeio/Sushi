using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathDamage : MonoBehaviour
{


    public float playerHealth;

    public static PlayerDeathDamage playerDeathDamageInstance;
    // Start is called before the first frame update

    private void Awake()
    {
        playerDeathDamageInstance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damagee)
    {
        playerHealth -= damagee;
        if (playerHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("wut we do here");
    }
}
