using UnityEngine;

public class Grenades : MonoBehaviour
{

    public float delay = 2f;
    float countdown;
    bool hasExploded = false;
    public GameObject explosionEffect;
    public float blastRadius = 10f;
    public float explosion = 700f;
    GameObject graphics;
    GameObject Jb;

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

        Jb = Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] blastObjects = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearby in blastObjects)
        {
            Target dest = nearby.GetComponent<Target>();
            if (dest != null)
            {
                dest.TakeDamage(30);
            }
        }
        Collider[] moveObjects = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearby in moveObjects)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosion, transform.position, blastRadius);
            }
        }

        Destroy(gameObject);
        Destroy(Jb, 3f);

    }
}
