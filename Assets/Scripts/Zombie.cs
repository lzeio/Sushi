using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Zombie : MonoBehaviour
{
    public PlayerController player;
    public bool isAware = false;

    private NavMeshAgent agent;

    public float walkPointRange;
    public Vector3 walkPoint;
    bool walkPointSet;
    
    public float awarenessDistance;
    

    //Angle
    public float fov = 120f;



    // Start is called before the first frame updat
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       
    }

    // Update is called once per frame
    void Update()
    {
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

    public void SearchForPlayer()
    {
        Debug.Log("Searching for player");
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) <= fov / 2f)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= awarenessDistance)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, player.transform.position, out hit,-1))
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
        transform.LookAt(player.transform);
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
        Debug.Log("Wandering");
        if (!walkPointSet) WalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1)
        {
            walkPointSet = false;
        }
    }

    public void OnAware()
    {
        isAware = true;
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
