using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Zombie : MonoBehaviour
{
    public PlayerController player;
    public bool isAware = false;

    private NavMeshAgent agent;
    public float fov = 120f;
    public float walkPointRange;
    public Vector3 walkPoint;
    bool walkPointSet;

    public float zomHealth;


    public float awarenessDistance;
    public float zombieWanderSpeed;
    public float zombieChaseSpeed;

    public Collider[] ragColliders;
    public Rigidbody[] ragRigidbodies;

    //Angle


    private Animator animZom;



    // Start is called before the first frame updat
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animZom = GetComponent<Animator>();
        ragColliders = GetComponentsInChildren<Collider>();
        ragRigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Collider c in ragColliders)
        {
            if (!c.CompareTag("Zombie"))
            {
                c.enabled = false;
            }
        }
        foreach (Rigidbody r in ragRigidbodies)
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
        if (zomHealth <= 0)
        {
            return;
        }
        if (isAware)
        {
            ChasePlayer();
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
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) <= fov / 2f)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= awarenessDistance)
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
        agent.speed = zombieChaseSpeed;
        agent.SetDestination(player.transform.position);
    }

    void WalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

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
            agent.speed = zombieWanderSpeed;
        }
        animZom.Play("Walk");

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 3f)
        {
            walkPointSet = false;
        }
    }



    public void TakeDamage(float damagee)
    {
        zomHealth -= damagee;
        if (zomHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        animZom.enabled = false;
        agent.speed = 0;
        foreach (Collider c in ragColliders)
        {
            c.enabled = true;
            if (c.CompareTag("Zombie"))
            {
                c.enabled = false;
            }
        }
        foreach (Rigidbody r in ragRigidbodies)
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
        float rayRange = awarenessDistance;
        float halfFOV = fov / 2f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);

    }



}
