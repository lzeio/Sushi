using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{

    public ZombieData zOS;
    PlayerController playerController;

    public SphereCollider sp;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = float.Parse(zOS.zombieAttackDamage);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.transform.name);
            PlayerController.playerInstance.playerHealth -= damage;
        }
    }

}
