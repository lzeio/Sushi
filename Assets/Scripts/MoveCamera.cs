using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MoveCamera : MonoBehaviour {

    public Transform player;
    public Rigidbody rb;

    void Update() {
        transform.position = player.transform.position;
    }
}
