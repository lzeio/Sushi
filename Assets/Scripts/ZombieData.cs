using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie", menuName = "Zomb")]
public class ZombieData : ScriptableObject
{
    public float FOV;
    public float walkPointRange;
    

    //Health
    [Header("Health")]
    public float zomHealth;


    [Header("Zombie Traits")]

    public float awarenessDistance;
    public float zombieWanderSpeed;
    public float zombieChaseSpeed;

    [Header ("Colliders")]

    public float attackDistance;
    public Collider[] ragColliders;
    public Rigidbody[] ragRigidbodies;
}
