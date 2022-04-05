using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    public Transform recoilPosition;
    public Transform rotationPoint;

    public float positionalRecoilSpeed = 8f;
    public float rotationalRecoilSpeed = 8f;

    public float positionalReturnSpeed = 18f;
    public float rotationalReturnSpeed = 38f;

    public Vector3 recoilRotation = new Vector3(10, 4, 6);
    public Vector3 recoilKickBack = new Vector3(.015f, 0f, -0.2f);

    Vector3 rotationalRecoil, positionalRecoil, rot;
    bool shooting;
    public bool allowButtonHold;

    // Start is called before the first frame update
    void Start()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Fire();
        }
        if(shooting)
        {
            Debug.Log("Called");
            Fire();
        }
    }

    public void Fire()
    {
        rotationalRecoil += new Vector3(-recoilRotation.x, UnityEngine.Random.Range(-recoilRotation.y, recoilRotation.y), UnityEngine.Random.Range(-recoilRotation.z, recoilRotation.z));
        positionalRecoil += new Vector3(UnityEngine.Random.Range(-recoilKickBack.x, recoilKickBack.x), UnityEngine.Random.Range(-recoilKickBack.y, recoilKickBack.y), recoilKickBack.z);
    }

    private void FixedUpdate()
    {
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalRecoilSpeed * Time.fixedDeltaTime);
        rot = Vector3.Slerp(rot, rotationalRecoil, rotationalRecoilSpeed * Time.fixedDeltaTime);
        rotationPoint.localRotation = Quaternion.Euler(rot);

    }
}
