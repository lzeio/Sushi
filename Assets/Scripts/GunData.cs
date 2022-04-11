using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[CreateAssetMenu(fileName ="Gun",menuName ="GunSystem")]
public class GunData : ScriptableObject
{
    [Header("GunAttributes")]
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spreadX, spreadY, range, reloadTime, timeBetweenShots;
    public int magazineSize, totalMags, bulletsPerTap;
    public int totalBullets;
    public bool allowButtonHold;
    public int bulletsLeft, bulletsShot;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphic
    public GameObject muzzleFlash, bulletHoleGraphic;
    public TextMeshProUGUI text;
    public static GunSystem gsInstance;


    [Header("Recoil")]
    public Transform recoilPosition;
    public Transform rotationPoint;

    public float positionalRecoilSpeed;// = 8f;
    public float rotationalRecoilSpeed;// = 8f;

    public float positionalReturnSpeed;// = 18f;
    public float rotationalReturnSpeed;// = 38f;

    public Vector3 recoilRotation;// = new Vector3(10, 4, 6);
    public Vector3 recoilKickBack;// = new Vector3(.015f, 0f, -0.2f);

    public float gunSoundIntensity;
}
