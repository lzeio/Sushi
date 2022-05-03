using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{

    public ZombieData lol;
    PlayerController playerController;

    public SphereCollider sp;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = float.Parse(lol.zombieAttackDamage);
    }

    // Update is called once per frame
    void Update()
    {
        if( sp != null || sp.enabled!=true)
        {
            sp.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("UH");
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController.instance.playerHealth -= damage;
        }
    }

}
