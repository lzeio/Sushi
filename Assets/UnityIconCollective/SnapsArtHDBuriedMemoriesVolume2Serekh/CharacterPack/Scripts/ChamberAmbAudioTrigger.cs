using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChamberAmbAudioTrigger : MonoBehaviour
{
    public AudioMixerSnapshot chamberOn;
    public AudioMixerSnapshot chamberMute;
    public AudioMixerSnapshot serviceAreaOn;
    public AudioMixerSnapshot serviceAreaMute;

    // Player enters the collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Entered Chamber");
            chamberOn.TransitionTo(1.0f);
            serviceAreaMute.TransitionTo(1.0f);
        }
    }

    // Player exits collider
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Exits Chamber");
        }
    }
}
