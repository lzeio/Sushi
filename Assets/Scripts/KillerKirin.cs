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
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
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

    // both functions to be declared in player movement since they are player dependent
    void OnShoot()
    {
        //take an audio source
        //Take an array colliders and do overlapsphere just like for grenade can use layer mask
        //then call onaware() that's it for every collider in radius
    }

    void OntriggerEnter()
    {
        //check for zombie tag

        //and change the radius for sprint and walk
        
    }
}



