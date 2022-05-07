    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public SphereCollider detect;

    public float detectionRangeSprint,detectionRangeWalk;

    public static Detection detectionInstance;

    public GunData gSO;

    public LayerMask whatIsEnemy;
    // Start is called before the first frame update
    void Start()
    {
        detectionInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.playerInstance.isDashing)
        {
            detect.radius = detectionRangeSprint;
        }
        else
        {
            detect.radius = detectionRangeWalk;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
     if(other.transform.gameObject.layer==10)
        {
            switch (other.transform.gameObject.tag)
            {

                case "Zombie": other.GetComponent<Zombie>().OnAware();
                    Debug.Log("Zombie Called");
                    break;
                case "Range": other.GetComponent<RangeZombie>().InRange();
                    Debug.Log("Range Zombie Called");

                    break;
                default:
                    break;
            }
        }
    }

    public void SoundDetection()
    {
       
        //Play Gun Sound aus.playoneshot(
        Collider[] zomCollider = Physics.OverlapSphere(transform.position, gSO.soundDetectionRadius, whatIsEnemy);
        for (int i = 0; i < zomCollider.Length; i++)
        {
            if (zomCollider[i].GetComponent<Zombie>() != null)
            {
                zomCollider[i].GetComponent<Zombie>().OnAware();
                
            }
        }
           
    }

}
