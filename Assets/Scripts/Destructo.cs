using UnityEngine;

public class Destructo : MonoBehaviour
{
    public float hits;
    public float health;
    public float blastRadius;

    public GameObject meshFilterDestructo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GiveDamage(hits);
            Explosion();
            //Change Mesh Render
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void GiveDamage(float hits)
    {
        Collider[] blastObjects = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearby in blastObjects)
        {
            switch (nearby.transform.gameObject.tag)
            {
                case "Zombie":
                    nearby.transform.GetComponent<ZombieDeathDamage>().TakeDamage(hits);
                    break;
                case "Range":
                    nearby.transform.GetComponent<ZombieDeathDamage>().TakeDamage(hits);
                    break;
                case "Player": 
                    nearby.transform.GetComponent<PlayerDeathDamage>().TakeDamage(hits);
                    break;
                default:
                    break;
            }
        }
    }

    void Explosion()
    {
        Debug.Log("Boom");
        gameObject.GetComponent<MeshFilter>().mesh = meshFilterDestructo.GetComponent<MeshFilter>().sharedMesh;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, blastRadius); 
    }
}
