using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerKirin : MonoBehaviour
{

    public PlayerMovement player;
    public bool isAware = false;

    //Angle
    public float fov = 120f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAware)
        {
            //Chase Player
        }
        else
        {
            SearchForPlayer();
        }
    }

    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) < fov/2f)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 10f)
            {
                OnAware(); //Stopped here for now
            }
        }
    }    


    public void OnAware()
    {
        isAware = true;
    }
}
