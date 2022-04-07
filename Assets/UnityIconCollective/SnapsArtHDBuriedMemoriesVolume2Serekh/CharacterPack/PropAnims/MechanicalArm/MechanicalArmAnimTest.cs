using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalArmAnimTest : MonoBehaviour
{

    Animator anim;
    //AudioSource audioData;


    void Start()
    {
        anim = GetComponent<Animator>();
        //audioData = GetComponent<AudioSource>();
    }

    //Trigger Anims
    public void MechanicalArm_Moving_Idle_v1() => anim.Play("MechanicalArm_Moving_Idle_v1");
    public void MechanicalArm_Moving_Idle_v2() => anim.Play("MechanicalArm_Moving_Idle_v2");
    public void MechanicalArm_Moving_Idle_v3() => anim.Play("MechanicalArm_Moving_Idle_v3");
    public void MechanicalArm_Moving_Idle_v4() => anim.Play("MechanicalArm_Moving_Idle_v4");
    public void MechanicalArm_Grab_Capsule() => anim.Play("MechanicalArm_Grab_Capsule");
    public void MechanicalArm_Grab_Return_Capsule() => anim.Play("MechanicalArm_Grab_Return_Capsule");

}
