using UnityEngine;

public class ZombieAttack : MonoBehaviour
{

    public ZombieData zombieData;
    PlayerController playerController;

    public SphereCollider sp;
    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = float.Parse(zombieData.zombieAttackDamage);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.gameObject.transform.name);
            other.gameObject.GetComponentInParent<PlayerDeathDamage>().TakeDamage(damage);
        }
    }

}
