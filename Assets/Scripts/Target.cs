using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Animator anim;
    public float health;// = 100f;

    public static Target _tInstance;
    // Start is called before the first frame update
    void Awake()
    {
        _tInstance = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damagee)
    {
        health -= damagee;
        if (health <= 0)
        {
            anim.Play("Die");
            Invoke("Die", 2f);
        }    
    }

    public void Die()
    {
        anim.enabled = false;
    }
}
