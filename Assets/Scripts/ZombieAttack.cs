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
        Debug.Log(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerDeathDamage.playerDeathDamageInstance.TakeDamage(damage);
        }
    }

}
