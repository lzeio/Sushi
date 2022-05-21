using UnityEngine;
using UnityEngine.AI;
public class Zombie : MonoBehaviour
{
    [Header("Components")]
    private NavMeshAgent agent;
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

    void Update()
    {

        if (isAware)
        {

            if (!alreadyAttacking)
            {
                Attack();

            }
            ChasePlayer();
        }
        else
        {
            Wandering();
            SearchForPlayer();
        }

        remainingDistance = agent.remainingDistance;
        enemyDestination = agent.destination;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        point = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }

    public void OnAware()
    {
        isAware = true;

    }


    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) <= zombieData.FOV / 2f)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= zombieData.awarenessDistance)
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
        animZom.SetBool("Chasing", true);
        transform.LookAt(point);
        agent.speed = zombieData.zombieChaseSpeed;
        agent.SetDestination(player.transform.position);
        Debug.Log("Chasing");
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
                    agent.destination = walkPoint;
                    animZom.SetTrigger("Walking");
                    agent.speed = zombieData.zombieWanderSpeed;
                }


            }
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < agent.stoppingDistance)
        {
            walkPointSet = false;
        }
    }

    void WalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-zombieData.walkPointRange, zombieData.walkPointRange);
        float randomX = UnityEngine.Random.Range(-zombieData.walkPointRange, zombieData.walkPointRange);

        walkPoint = new Vector3(randomX, transform.position.y, randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, player.groundMask))
            walkPointSet = true;
    }

    public void ActivateFist()
    {
        attackBodyPart.GetComponent<SphereCollider>().enabled = true;
    }

    public void DeactivateFist()
    {
        attackBodyPart.GetComponent<SphereCollider>().enabled = false;
        Debug.Log("Called");
    }

    public void Lab()
    {
        Debug.Log("Har");
        alreadyAttacking = false;
    }
    void Attack()
    {

        if (distanceToPlayer <= zombieData.attackDistance && !alreadyAttacking)
        {
            alreadyAttacking = true;
            animZom.SetFloat("Attack", Random.Range(0f, 3f));
            animZom.SetBool("Chasing", false);
            Debug.Log("Attacking");
        }
        else
        {
            animZom.SetFloat("Attack", 0f);
            ChasePlayer();
            alreadyAttacking = false;

        }
    }

    float RandomGenerator()
    {
        return Random.Range(0, 3f);
    }

    void LookAtClamps()
    {

        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

    }
    private void OnDrawGizmos()
    {
        float rayRange = zombieData.awarenessDistance;
        float halfFOV = zombieData.FOV / 2f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);


        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, zombieData.attackDistance);
    }
}

