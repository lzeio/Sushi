using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[CreateAssetMenu(fileName ="Gun",menuName ="GunSystem")]
public class GunData : ScriptableObject
{
    [Header("Gun Data")]
    public int damage;
    public float timeBetweenShooting, spreadX, spreadY, range, reloadTime, timeBetweenShots;
    public int magazineSize, totalMags, bulletsPerTap;
    public int totalBullets;
    public bool allowButtonHold;
    public int bulletsLeft, bulletsShot;



    [Header("Recoil Data")]
    public float positionalRecoilSpeed;// = 8f;
    public float rotationalRecoilSpeed;// = 8f;

    public float positionalReturnSpeed;// = 18f;
    public float rotationalReturnSpeed;// = 38f;

    public Vector3 recoilRotation;// = new Vector3(10, 4, 6);
    public Vector3 recoilKickBack;// = new Vector3(.015f, 0f, -0.2f);

    public float soundDetectionRadius;

    public Vector3 rotationalRecoil, positionalRecoil, rot;


}
