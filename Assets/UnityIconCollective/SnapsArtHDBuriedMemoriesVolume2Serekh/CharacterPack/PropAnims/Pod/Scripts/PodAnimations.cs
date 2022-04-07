using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodAnimations : MonoBehaviour
{

    Animator anim;
    //AudioSource audioData;


    void Start()
    {
        anim = GetComponent<Animator>();
        //audioData = GetComponent<AudioSource>();
    }

    //Trigger Anims
    public void Normal_Open_Ready() => anim.Play("Normal_Open_Ready");
    public void Normal_Close_Ready() => anim.Play("Normal_Close_Ready");


}
