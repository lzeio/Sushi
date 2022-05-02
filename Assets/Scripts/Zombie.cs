using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Zombie : MonoBehaviour
{

    public PlayerController player;
    public ZombieData zSO;
    public bool isAware = false;

    private NavMeshAgent agent;
   
    private Vector3 walkPoint;
    bool walkPointSet;

    
    

    //Angle


    private Animator animZom;


    // Start is called before the first frame updat
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animZom = GetComponent<Animator>();
        zSO.ragColliders = GetComponentsInChildren<Collider>();
        zSO.ragRigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Collider c in zSO.ragColliders)
        {
            if (!c.CompareTag("Zombie"))
            {
                c.enabled = false;
            }
        }
        foreach (Rigidbody r in zSO.ragRigidbodies)
        {
            if (!r.CompareTag("Zombie"))
            {
                r.isKinematic = true;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (zSO.zomHealth <= 0)
        {
            return;
        }
        if (isAware)
        {
            ChasePlayer();
            Attack();
        }
        else
        {
            Wandering();
            SearchForPlayer();

        }
    }

    public void OnAware()
    {
        isAware = true;
    }


    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) <= zSO.FOV / 2f)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= zSO.awarenessDistance)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, player.transform.position, out hit, -1))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        OnAware();
                    }

                }

            }
        }
    }


    void ChasePlayer()
    {
        animZom.Play("Chasing");
        transform.LookAt(player.transform);
        agent.speed = zSO.zombieChaseSpeed;
        agent.SetDestination(transform.position);
    }

    void WalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-zSO.walkPointRange, zSO.walkPointRange);
        float randomX = UnityEngine.Random.Range(-zSO.walkPointRange, zSO.walkPointRange);

        walkPoint = new Vector3(randomX, transform.position.y, randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, player.groundMask))
            walkPointSet = true;
    }

    void Wandering()
    {
        if (!walkPointSet) WalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            agent.speed = zSO.zombieWanderSpeed;
        }
        animZom.Play("Walk");

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 3f)
        {
            walkPointSet = false;
        }
    }

    void Attack()
    {
        if (agent.stoppingDistance <= zSO.attackDistance)
        {

        }
    }

    public void TakeDamage(float damagee)
    {
        zSO.zomHealth -= damagee;
        if (zSO.zomHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        animZom.enabled = false;
        agent.speed = 0;
        foreach (Collider c in zSO.ragColliders)
        {
            c.enabled = true;
            if (c.CompareTag("Zombie"))
            {
                c.enabled = false;
            }
        }
        foreach (Rigidbody r in zSO.ragRigidbodies)
        {
            if (!r.CompareTag("Zombie"))
            {
                r.isKinematic = false;
            }
        }
        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }



    private void OnDrawGizmos()
    {
        float rayRange = zSO.awarenessDistance;
        float halfFOV = zSO.FOV / 2f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);


        Gizmos.color= Color.yellow;
        Gizmos.DrawSphere(player.transform.position, zSO.attackDistance);
    }



}
