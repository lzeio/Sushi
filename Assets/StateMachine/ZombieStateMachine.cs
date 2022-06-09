using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieStateMachine : MonoBehaviour
{

    [Header("Components")]
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animZom;
    public ZombieData zombieData;


    [Header("Player")]
    public PlayerController player;


    [Header("Wandering")]
    public Vector3 walkPoint;
    bool walkPointSet;
    Vector3 point;


    [Header("Attacking")]
    public bool isAware = false;
    public bool alreadyAttacking = false;
    public GameObject attackBodyPart;
    public float distanceToPlayer;



    [Header("Misc")]
    public float remainingDistance;
    public Vector3 enemyDestination;

    private void Awake()
    {

        agent = GetComponent<NavMeshAgent>();
        animZom = GetComponent<Animator>();
        agent.stoppingDistance = zombieData.zombieStoppingDistance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
