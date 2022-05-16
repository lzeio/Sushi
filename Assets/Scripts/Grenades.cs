using UnityEngine;

public class Grenades : MonoBehaviour
{

    public float delay = 2f;
    float countdown;
    bool hasExploded = false;
    public GameObject explosionEffect;
    public float blastRadius = 10f;
    public float explosion = 700f;
    public float hits;
    public float halfBlastRadius;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {

        ///Jb = Instantiate(explosionEffect, transform.position, transform.rotation);
         Collider[] blastObjects = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearby in blastObjects)
        {
            switch (nearby.transform.gameObject.tag)
            {
                case "Zombie":
                    nearby.GetComponent<ZombieDeathDamage>().TakeDamage(hits);
                    break;
                case "Range":
                    nearby.GetComponent<ZombieDeathDamage>().TakeDamage(hits);
                    break;
                case "Player":
                    nearby.GetComponent<PlayerDeathDamage>().TakeDamage(hits);
                    break;
                default:
                    break;
            }
        }

        Collider[] blastObjects2 = Physics.OverlapSphere(transform.position, halfBlastRadius);
        foreach (Collider nearby2 in blastObjects2)
        {
            switch (nearby2.transform.gameObject.tag)
            {
                case "Zombie":
                    nearby2.GetComponent<ZombieDeathDamage>().TakeDamage(hits*2);
                    break;
                case "Range":
                    nearby2.GetComponent<ZombieDeathDamage>().TakeDamage(hits*2);
                    break;
                case "Player":
                    nearby2.GetComponent<PlayerDeathDamage>().TakeDamage(hits*2);
                    break;
                default:
                    break;
            }
        }

        Collider[] moveObjects = Physics.OverlapSphere(transform.position, halfBlastRadius);

        foreach (Collider nearby in moveObjects)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosion, transform.position, blastRadius);
            }
        }

        Destroy(gameObject);
        

    }
}
