using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieDeathDamage : MonoBehaviour
{
    [Header("Components")]
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animZom;
    public ZombieData zombieData;


    public float health;

    // Start is called before the first frame update
    void Start()
    {
        health = int.Parse(zombieData.zomHealth);
        agent = GetComponent<NavMeshAgent>();
        animZom=GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);
    }

    public void Death()
    {
        Debug.Log("Dead");
        animZom.enabled = false;
        agent.speed = 0;
        ZombieRagdoll.zomRagInstance.RagdollON();
        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    public void TakeDamage(float damagee)
    {
        health -= damagee;
        if (health <= 0)
        {
            Death();
        }
    }
}
