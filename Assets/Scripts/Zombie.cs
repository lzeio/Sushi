using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class Zombie : MonoBehaviour
{

    public PlayerController player;
    public ZombieData zSO;
    public bool isAware = false;

    private NavMeshAgent agent;
    private float health;
    private Vector3 walkPoint;
    bool walkPointSet;


    public GameObject attackBodyPart;
    private Animator animZom;


    // Start is called before the first frame updat
    void Start()
    {
        health = int.Parse(zSO.zomHealth);
        Debug.Log(health);
        agent = GetComponent<NavMeshAgent>();
        animZom = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
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
        Debug.Log("Aaa");
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
        animZom.SetTrigger("Chasing");
        transform.LookAt(player.transform);
        agent.speed = zSO.zombieChaseSpeed;
        agent.SetDestination(player.transform.position);
    }



    void Wandering()
    {
        if (!walkPointSet) WalkPoint();

        if (walkPointSet)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.position * 10, out hit, 3f))
            {
                if (hit.transform.CompareTag("Walls"))
                {
                    walkPointSet = false;
                }
                else
                {
                    agent.SetDestination(player.transform.position);
                    animZom.SetTrigger("Walking");
                    agent.speed = zSO.zombieWanderSpeed;
                }


            }

        }


        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 3f)
        {
            walkPointSet = false;
        }
    }

    void WalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-zSO.walkPointRange, zSO.walkPointRange);
        float randomX = UnityEngine.Random.Range(-zSO.walkPointRange, zSO.walkPointRange);

        walkPoint = new Vector3(randomX, transform.position.y, randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, player.groundMask))
            walkPointSet = true;
    }



    public void TakeDamage(float damagee)
    {
        health -= damagee;
        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        animZom.enabled = false;
        agent.speed = 0;
        ZombieRagdoll.Instance.RagdollON();
        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }



    void Attack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= zSO.attackDistance)
        {
            animZom.SetBool("Attacking", true);
        }
    }

    public void ActivateFist()
    {
        attackBodyPart.GetComponent<SphereCollider>().enabled = true;
    }

    public void DeactivateFist()
    {
        attackBodyPart.GetComponent<SphereCollider>().enabled = false;
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


        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(player.transform.position, zSO.attackDistance);


    }



}
