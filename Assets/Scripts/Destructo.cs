using UnityEngine;

public class Destructo : MonoBehaviour
{
    public float hits;
    public float health;
    public float blastRadius;
    public float halfBlastRadius;

    public GameObject meshFilterDestructo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (health <= 0)
        //{
        //    GiveDamage(hits);
        //    Explosion();
        //    //Change Mesh Render
        //}
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
         if (health <= 0)
        {
            GiveDamage(hits);
            Explosion();
            //Change Mesh Render
        }
    }

    public void GiveDamage(float hits)
    {
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

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position,halfBlastRadius);  
    }
}
