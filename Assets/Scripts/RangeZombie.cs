using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeZombie : MonoBehaviour
{
    [Header("Components")]
    private Animator animRange;
    private NavMeshAgent rangeAgent;
    public ZombieData rangeData;

    [Header("Player")]
    public PlayerController player;

    [Header("Wandering")]
    bool walkPointSet;
    Vector3 walkPoint;

    [Header("Range Data")]    
    public bool inRange;
    public bool alreadyAttacked;
    public float timeBetweenAttacks;
    Vector3 point;
    public Transform attackPoint;

    private void Awake()
    {
        inRange = false;
        animRange = GetComponent<Animator>();   
        rangeAgent = GetComponent<NavMeshAgent>();
        rangeAgent.stoppingDistance = rangeData.zombieStoppingDistance;
    }
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenAttacks = float.Parse(rangeData.timeBetweenAttacks);
    }

    // Update is called once per frame
    void Update()
    {

        if (inRange)
        {
            RangeChase();
            RangeAttack();
        }
        else
        {
            Wandering();
            SearchForPlayer();
        }

        point = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }


        public void InRange()
    {
        inRange = true;
    }

    public void SearchForPlayer()
    {
        if (rangeData.isRanged)
        {
            if (Physics.CheckSphere(transform.position, rangeData.rangeAttackDistance, player.transform.gameObject.layer))
            {
                InRange();
            }
        }
    }
    void RangeChase()
    {
        animRange.SetBool("Frisbee", true);
        rangeAgent.SetDestination(player.transform.position);
    }



    void Wandering()
    {
        if (!walkPointSet) WalkPoint();

        if (walkPointSet)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (walkPoint - transform.position), out hit, 3f))
            {
                if (hit.transform.CompareTag("Walls"))
                {
                    walkPointSet = false;
                }
                else
                {
                    rangeAgent.destination = walkPoint;
                    animRange.SetTrigger("Walking");
                    rangeAgent.speed = rangeData.zombieWanderSpeed;
                }
            }
        }


        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < rangeAgent.stoppingDistance)
        {
            walkPointSet = false;
        }
    }

    void WalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-rangeData.walkPointRange, rangeData.walkPointRange);
        float randomX = UnityEngine.Random.Range(-rangeData.walkPointRange, rangeData.walkPointRange);

        walkPoint = new Vector3(randomX, transform.position.y, randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, player.groundMask))
            walkPointSet = true;
    }
    public void RangeAttack()
    {
        transform.LookAt(point);
        if(!alreadyAttacked)
        {
            Rigidbody rb =Instantiate(rangeData.projectile, attackPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward *32f,ForceMode.Impulse);
            rb.AddForce(transform.up *8f,ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            Destroy(rb, timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked=false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;    
        Gizmos.DrawSphere(transform.position,rangeData.rangeAttackDistance);
    }
}
