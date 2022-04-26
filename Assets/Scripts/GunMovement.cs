using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private Transform playerCam;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
         transform.position = playerPos.position;
        transform.rotation = playerCam.rotation;
    }
}
