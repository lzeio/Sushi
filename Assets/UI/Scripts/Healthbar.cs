using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
 private float health;
 public float maxHealth = 100f;
 private float lerpTimer;
 public float chipSpeed = 2f;
 public Image frontHealthBar;
 public Image backHealthBar;

public static Healthbar instance;

    private void Awake()
    {
        instance = this;    
    }
    void Start()
 {
    health = maxHealth;
 }
 void Update()
 {
     health = Mathf.Clamp(health, 0, maxHealth);
     UpdateHealthUI();
     //if(Input.GetKeyDown(KeyCode.A))
     //{
     //   TakeDamage(Random.Range(5, 10));
     //}
     //if(Input.GetKeyDown(KeyCode.S))
     //{
     //   Heal(Random.Range(5, 10));
     //}
 }
 public void UpdateHealthUI()
 {
     //Debug.Log(health);
     float fillF = frontHealthBar.fillAmount;
     float fillB = backHealthBar.fillAmount;
     float hFraction = health / maxHealth;
     if(fillB>hFraction)
     {
         frontHealthBar.fillAmount=hFraction;
         backHealthBar.color = Color.red;
         lerpTimer += Time.deltaTime;
         float percentComplete = lerpTimer/chipSpeed;
         percentComplete = percentComplete * percentComplete;
         backHealthBar.fillAmount= Mathf.Lerp(fillB, hFraction, percentComplete);
     }
     if(fillF<hFraction)
     {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount=hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount= Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
     }
 }
 public void  TakeDamage(float Damage)
 {
     health -= Damage;
     if (health <= 0)
     {
         health = 0;
     }
     lerpTimer=0f;
 }
 public void  Heal(float healAmount)
 {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        lerpTimer=0f;
 }
}
