    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public SphereCollider detect;

    public float detectionRangeSprint,detectionRangeWalk;

    public static Detection dInstance;

    public GunData gSO;

    public LayerMask whatIsEnemy;
    // Start is called before the first frame update
    void Start()
    {
        dInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.instance.isDashing)
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
        if (other.gameObject.CompareTag("Zombie"))
        {
            other.GetComponent<Zombie>().OnAware();
        }
    }

    public void SoundDetection()
    {
        Debug.Log("De");
        //Play Gun Sound aus.playoneshot(
        Collider[] zomCollider = Physics.OverlapSphere(transform.position, gSO.soundDetectionRadius, whatIsEnemy);
        for (int i = 0; i < zomCollider.Length; i++)
        {
            if (zomCollider[i].GetComponent<Zombie>() != null)
            {
                zomCollider[i].GetComponent<Zombie>().OnAware();
                Debug.Log(zomCollider[i].name);
            }
        }
           
    }

}
