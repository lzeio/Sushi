using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    public PlayerController controllerScript;
    //Dashing
    public bool isDashing;
    public float dashSpeed = 40f; //speed of dash
    public float dashTime = 0.5f; //time of dash
    // Start is called before the first frame update


    [SerializeField] ParticleSystem forwardDashParticles;
    [SerializeField] ParticleSystem backwardDashParticles;
    [SerializeField] ParticleSystem leftDashParticles;
    [SerializeField] ParticleSystem RightDashParticles;
    void Start()
    {
        controllerScript = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
     
        if (controllerScript.isDashing)
        {
            StartCoroutine(Dash());//need a cool down here

        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        while (Time.time - startTime < dashTime)
        {       
            controllerScript.cc.Move(controllerScript.movement * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }    

    void PlayDashParticles()
    {
        if(controllerScript.isDashing)
        {
            if (controllerScript.x < 0)
            {
                leftDashParticles.Play();
            }
            if(controllerScript.x>0)
            {
                RightDashParticles.Play();
            }
            if(controllerScript.z>0)
            {
                forwardDashParticles.Play();
            }
            if(controllerScript.z<0)
            {
                backwardDashParticles.Play();
            }
        }
    }
}
