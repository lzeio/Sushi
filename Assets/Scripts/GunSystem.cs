using UnityEngine;
using TMPro;
public class GunSystem : MonoBehaviour
{

    //bools 
    bool shooting, readyToShoot, reloading;
    //Reference

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    public TextMeshProUGUI text;
    public static GunSystem gsInstance;

    Vector3 rotationalRecoil, positionalRecoil, rot;


    public GunData gun;

    private void Awake()
    {
        gun.bulletsLeft = gun.magazineSize;
        gun.totalBullets = gun.magazineSize * gun.totalMags;
        readyToShoot = true;
        gsInstance = this;
    }
    private void Update()
    {
        MyInput();
        //Ammo
        text.SetText(gun.bulletsLeft + " / " + gun.totalBullets);
    }

    private void FixedUpdate()
    {
       
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, gun.rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, gun.positionalReturnSpeed * Time.deltaTime);

        gun.recoilPosition.localPosition = Vector3.Slerp(gun.recoilPosition.localPosition, positionalRecoil, gun.positionalRecoilSpeed * Time.fixedDeltaTime);
        rot = Vector3.Slerp(rot, rotationalRecoil, gun.rotationalRecoilSpeed * Time.fixedDeltaTime);
        gun.rotationPoint.localRotation = Quaternion.Euler(rot);

    }
    private void MyInput()
    {
        if (gun.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
       
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        
        if (Input.GetKeyDown(KeyCode.R) && gun.bulletsLeft / gun.totalMags < gun.magazineSize / gun.totalMags && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && gun.bulletsLeft > 0)
        {
            gun.bulletsShot = gun.bulletsPerTap;
            Shoot();
            Recoil();
            CamRecoil.crInstance.CameraRecoil();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-gun.spreadX, gun.spreadX);
        float y = Random.Range(0, gun.spreadY);

        //Calculate Direction with Spread
        Vector3 direction = gun.fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(gun.attackPoint.transform.position, direction, out gun.rayHit, gun.range, gun.whatIsEnemy))
        {
            Debug.Log(gun.rayHit.collider.name);

            if (gun.rayHit.collider.CompareTag("Enemy"))
                gun.rayHit.collider.GetComponent<Target>().TakeDamage(gun.damage);
        }

        
      
        //Impacts
        Instantiate(bulletHoleGraphic, gun.rayHit.point, Quaternion.Euler(0, 180, 0));
        GameObject mFlash=Instantiate(muzzleFlash, gun.attackPoint.position, Quaternion.identity);
        Destroy(mFlash, .5f);
        gun.bulletsLeft--;
        gun.bulletsShot--;
        //totalBullets--;
        
        Invoke("ResetShot", gun.timeBetweenShooting);

        if (gun.bulletsShot > 0 && gun.bulletsLeft > 0)
            Invoke("Shoot", gun.timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", gun.reloadTime);
    }
    private void ReloadFinished()
    {
        if (gun.totalBullets >= gun.magazineSize)
            gun.bulletsLeft = gun.magazineSize;
        else if(gun.totalBullets < gun.magazineSize)
            gun.bulletsLeft = gun.totalBullets;
        reloading = false;
    }

    void Recoil()
    {
        rotationalRecoil += new Vector3(-gun.recoilRotation.x, UnityEngine.Random.Range(-gun.recoilRotation.y, gun.recoilRotation.y), UnityEngine.Random.Range(-gun.recoilRotation.z, gun.recoilRotation.z));
        positionalRecoil += new Vector3(UnityEngine.Random.Range(-gun.recoilKickBack.x, gun.recoilKickBack.x), UnityEngine.Random.Range(-gun.recoilKickBack.y, gun.recoilKickBack.y), gun.recoilKickBack.z);
    }
}
