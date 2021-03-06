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
    //public const float zomHealth;
    public string zomHealth; 


    [Header("Zombie Traits")]
    public string zombieAttackDamage;
    public float awarenessDistance;
    public float zombieWanderSpeed;
    public float zombieChaseSpeed;
    public float attackDistance;
    public float zombieStoppingDistance;


    [Header("Ranged")]
    public bool isRanged;
    public float rangeAttackDistance;
    public GameObject projectile;
    public float projectileSpeed;
    public string timeBetweenAttacks;
}
