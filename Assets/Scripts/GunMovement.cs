using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    [SerializeField]
    Transform PlayerPos,camera;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
         transform.position = PlayerPos.position;
        transform.rotation = camera.rotation;
    }
}
