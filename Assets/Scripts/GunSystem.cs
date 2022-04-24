using UnityEngine;
using TMPro;
public class GunSystem : MonoBehaviour
{
    [SerializeField] private GunData gSO;

    

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphic
    //public GameObject muzzleFlash, bulletHoleGraphic;
   // public TextMeshProUGUI text;
    public static GunSystem gsInstance;


    [Header("Recoil Data")]
    public Transform recoilPosition;
    public Transform rotationPoint;
    //bools 
    bool shooting, readyToShoot, reloading;
    //Reference

    //Graphics
    //public GameObject flash, bulletHoleGraphic;
    //public TextMeshProUGUI txt;
    //public static GunSystem Instance;


    private AudioSource aus;    

   // public GunData gun;

    private void Awake()
    {
        gSO.bulletsLeft = gSO.magazineSize;
        gSO.totalBullets = gSO.magazineSize * gSO.totalMags;
        readyToShoot = true;
       // Instance = this;
    }

    private void Start()
    {
        aus = GetComponent<AudioSource>();
    }
    private void Update()
    {
        MyInput();
        //Ammo
       // txt.SetText(bulletsLeft + " / " + totalBullets);
    }

    private void FixedUpdate()
    {

        gSO.rotationalRecoil = Vector3.Lerp(gSO.rotationalRecoil, Vector3.zero, gSO.rotationalReturnSpeed * Time.deltaTime);
        gSO.positionalRecoil = Vector3.Lerp(gSO.positionalRecoil, Vector3.zero, gSO.positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, gSO.positionalRecoil, gSO.positionalRecoilSpeed * Time.fixedDeltaTime);
        gSO.rot = Vector3.Slerp(gSO.rot, gSO.rotationalRecoil, gSO.rotationalRecoilSpeed * Time.fixedDeltaTime);
        rotationPoint.localRotation = Quaternion.Euler(gSO.rot);

    }
    private void MyInput()
    {
        if (gSO.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
       
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        
        if (Input.GetKeyDown(KeyCode.R) && gSO.bulletsLeft / gSO.totalMags < gSO.magazineSize / gSO.totalMags && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && gSO.bulletsLeft > 0)
        {
            gSO.bulletsShot = gSO.bulletsPerTap;
            Shoot();
            Detection.dInstance.SoundDetection();
            Recoil();
            CamRecoil.crInstance.CameraRecoil();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(gSO.spreadX, gSO.spreadX);
        float y = Random.Range(0, gSO.spreadY);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(attackPoint.transform.position, direction, out rayHit, gSO.range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<Target>().TakeDamage(gSO.damage);
        }

        //Impacts
        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        //GameObject mFlash=Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        //Destroy(mFlash, .5f);
        gSO.bulletsLeft--;
        gSO.bulletsShot--;
        //totalBullets--;
        
        Invoke("ResetShot", gSO.timeBetweenShooting);

        if (gSO.bulletsShot > 0 && gSO.bulletsLeft > 0)
            Invoke("Shoot", gSO.timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", gSO.reloadTime);
    }
    private void ReloadFinished()
    {
        if (gSO.totalBullets >= gSO.magazineSize)
            gSO.bulletsLeft = gSO.magazineSize;
        else if(gSO.totalBullets < gSO.magazineSize)
            gSO.bulletsLeft = gSO.totalBullets;
        reloading = false;
    }

    void Recoil()
    {
        gSO.rotationalRecoil += new Vector3(gSO.recoilRotation.x, UnityEngine.Random.Range(gSO.recoilRotation.y, gSO.recoilRotation.y), UnityEngine.Random.Range(gSO.recoilRotation.z, gSO.recoilRotation.z));
        gSO.positionalRecoil += new Vector3(UnityEngine.Random.Range(gSO.recoilKickBack.x, gSO.recoilKickBack.x), UnityEngine.Random.Range(gSO.recoilKickBack.y, gSO.recoilKickBack.y), gSO.recoilKickBack.z);
    }


   
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gSO.soundDetectionRadius);
    }
}
