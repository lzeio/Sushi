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

    public void GiveDamage(float damage)
    {
        Collider[] blastObjects = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearby in blastObjects)
        {
            switch (nearby.transform.gameObject.layer)

            {
                case 10:
                    nearby.transform.GetComponent<ZombieDeathDamage>().TakeDamage(damage);
                    break;
                default:
                    break;
            }
        }
    }

    void Explosion()
    {
        Debug.Log("Called");
        gameObject.GetComponent<MeshFilter>().mesh = meshFilterDestructo.GetComponent<MeshFilter>().sharedMesh;
    }
}
