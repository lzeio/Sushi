using UnityEngine;
using TMPro;
public class GunSystem : MonoBehaviour
{
    [Header("GunAttributes")]
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spreadX,spreadY, range, reloadTime, timeBetweenShots;
    public int magazineSize,totalMags,bulletsPerTap;
    int totalBullets;
    public bool allowButtonHold;
    private int bulletsLeft, bulletsShot;
    /////bools 
    bool shooting, readyToShoot, reloading;
    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
   
    public GameObject muzzleFlash, bulletHoleGraphic;
    public TextMeshProUGUI text;
    public static GunSystem gsInstance;


    [Header("Recoil")]
    public Transform recoilPosition;
    public Transform rotationPoint;

    public float positionalRecoilSpeed = 8f;
    public float rotationalRecoilSpeed = 8f;

    public float positionalReturnSpeed = 18f;
    public float rotationalReturnSpeed = 38f;

    public Vector3 recoilRotation = new Vector3(10, 4, 6);
    public Vector3 recoilKickBack = new Vector3(.015f, 0f, -0.2f);

    Vector3 rotationalRecoil, positionalRecoil, rot;


    private void Awake()
    {
        bulletsLeft = magazineSize;
        totalBullets = magazineSize * totalMags;
        readyToShoot = true;
        gsInstance = this;
    }
    private void Update()
    {
        MyInput();
        //Ammo
        text.SetText(bulletsLeft + " / " + totalBullets);
    }

    private void FixedUpdate()
    {
       
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalRecoilSpeed * Time.fixedDeltaTime);
        rot = Vector3.Slerp(rot, rotationalRecoil, rotationalRecoilSpeed * Time.fixedDeltaTime);
        rotationPoint.localRotation = Quaternion.Euler(rot);

    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
       
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft/totalMags < magazineSize/totalMags && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
            Recoil();
            CamRecoil.crInstance.CameraRecoil();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spreadX, spreadX);
        float y = Random.Range(0, spreadY);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(attackPoint.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<Target>().TakeDamage(damage);
        }

        
      
        //Impacts
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        GameObject mFlash=Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        Destroy(mFlash, .5f);
        bulletsLeft--;
        bulletsShot--;
        //totalBullets--;
        
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        if (totalBullets >= magazineSize)
           bulletsLeft = magazineSize;
        else if(totalBullets<magazineSize)
            bulletsLeft = totalBullets;
        reloading = false;
    }

    void Recoil()
    {
        rotationalRecoil += new Vector3(-recoilRotation.x, UnityEngine.Random.Range(-recoilRotation.y, recoilRotation.y), UnityEngine.Random.Range(-recoilRotation.z, recoilRotation.z));
        positionalRecoil += new Vector3(UnityEngine.Random.Range(-recoilKickBack.x, recoilKickBack.x), UnityEngine.Random.Range(-recoilKickBack.y, recoilKickBack.y), recoilKickBack.z);
    }
}
