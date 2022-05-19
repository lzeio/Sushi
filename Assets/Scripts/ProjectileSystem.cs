using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSystem : MonoBehaviour
{
    [Header("Gun Data")]
    public GunData gunData;

    public GameObject projectile;

    public float shootForce, upwardForce;

    bool shooting, readyToShoot;

    public GameObject pseudoProjectle;

    public Camera camera;
    public Transform attackPoint;

    public bool allowInvoke;
    private void Awake()
    {
        readyToShoot = true; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    void MyInput()
    {
        if (gunData.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if(readyToShoot && shooting && gunData.bulletsLeft>0)
        {
            gunData.bulletsShot = 0;
            Debug.Log("Called");
            Shoot();
        }
      
    }


    void Shoot()
    {
        pseudoProjectle.SetActive(false);
        readyToShoot=false;

        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else targetPoint = ray.GetPoint(75);

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x  = Random.Range(gunData.spreadX, gunData.spreadX);
        float y = Random.Range(0, gunData.spreadY);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x,y, 0); 

        GameObject currentBullet = Instantiate(projectile,attackPoint.position,Quaternion.identity);

        currentBullet.transform.forward = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
       // currentBullet.GetComponent<Rigidbody>().AddForce(camera.transform.up*upwardForce,ForceMode.Impulse);

        gunData.bulletsShot++;
        gunData.bulletsLeft--;

        if(allowInvoke)
        {
            Invoke("ResetShot", gunData.timeBetweenShooting);
                allowInvoke = false;
        }

        if(gunData.bulletsShot<gunData.bulletsPerTap && gunData.bulletsLeft>0)
        {
            Invoke("Shoot", gunData.timeBetweenShots);
        }
    }

    void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
        pseudoProjectle.SetActive(true);
    }
}
