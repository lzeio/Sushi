using UnityEngine;
using TMPro;
public class GunSystem : MonoBehaviour
{
    [SerializeField] public GunData gunData;

    

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
    public TextMeshProUGUI txt;
    //public static GunSystem Instance;


    private AudioSource aus;    

   // public GunData gun;

    private void Awake()
    {
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
        txt.SetText(gunData.bulletsLeft.ToString());

    }

    private void FixedUpdate()
    {

        gunData.rotationalRecoil = Vector3.Lerp(gunData.rotationalRecoil, Vector3.zero, gunData.rotationalReturnSpeed * Time.deltaTime);
        gunData.positionalRecoil = Vector3.Lerp(gunData.positionalRecoil, Vector3.zero, gunData.positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, gunData.positionalRecoil, gunData.positionalRecoilSpeed * Time.fixedDeltaTime);
        gunData.rot = Vector3.Slerp(gunData.rot, gunData.rotationalRecoil, gunData.rotationalRecoilSpeed * Time.fixedDeltaTime);
        rotationPoint.localRotation = Quaternion.Euler(gunData.rot);

    }
    private void MyInput()
    {
        if (gunData.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
       
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);


        //Shoot
        if (readyToShoot && shooting && !reloading && gunData.bulletsLeft > 0)
        {
            gunData.bulletsShot = gunData.bulletsPerTap;
            Shoot(); Recoil(); 
            CamRecoil.crInstance.CameraRecoil();
            Detection.detectionInstance.SoundDetection();
        }
    }
    private void Shoot()
    {
        //add shooting audio here

        readyToShoot = false;

        //Spread
        float x = Random.Range(gunData.spreadX, gunData.spreadX);
        float y = Random.Range(0, gunData.spreadY);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(attackPoint.transform.position, direction, out rayHit, gunData.range, whatIsEnemy))
        {
            Debug.Log(rayHit.transform.name);
            switch (rayHit.transform.gameObject.tag)

            {
                case "Zombie": rayHit.transform.GetComponent<ZombieDeathDamage>().TakeDamage(gunData.damage);
                    break;
                case "Destruct":  Debug.Log("GunShot");
                    rayHit.transform.GetComponent<Destructo>().TakeDamage(gunData.damage);
                    break;
                case "Range": rayHit.transform.GetComponent<ZombieDeathDamage>().TakeDamage(gunData.damage);
                    break;
                default:
                    break;
            }

        }

        //Impacts
        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        //GameObject mFlash=Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        //Destroy(mFlash, .5f);
        gunData.bulletsLeft--;
        gunData.bulletsShot--;
        
        Invoke("ResetShot", gunData.timeBetweenShooting);

        if (gunData.bulletsShot > 0 && gunData.bulletsLeft > 0)
            Invoke("Shoot", gunData.timeBetweenShots);
        
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
   
    void Recoil()
    {
        gunData.rotationalRecoil += new Vector3(gunData.recoilRotation.x, UnityEngine.Random.Range(gunData.recoilRotation.y, gunData.recoilRotation.y), UnityEngine.Random.Range(gunData.recoilRotation.z, gunData.recoilRotation.z));
        gunData.positionalRecoil += new Vector3(UnityEngine.Random.Range(gunData.recoilKickBack.x, gunData.recoilKickBack.x), UnityEngine.Random.Range(gunData.recoilKickBack.y, gunData.recoilKickBack.y), gunData.recoilKickBack.z);
    }


   
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gunData.soundDetectionRadius);
    }


}
