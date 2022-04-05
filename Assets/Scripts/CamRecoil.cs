using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRecoil : MonoBehaviour
{
    public float rotationSpeed = 6f;
    public float returnSpeed = 25f;

    public Vector3 recoilRot = new Vector3(2f, 2f, 2f);

    private Vector3 currentRot;
    private Vector3 rot;
    public static CamRecoil crInstance;
    // Start is called before the first frame update
    void Start()
    {
        crInstance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CameraRecoil()
    {
        currentRot += new Vector3(-recoilRot.x, UnityEngine.Random.Range(-recoilRot.y, recoilRot.y), UnityEngine.Random.Range(-recoilRot.z, recoilRot.z));
    }

    private void FixedUpdate()
    {
        currentRot = Vector3.Lerp(currentRot, Vector3.zero, returnSpeed * Time.deltaTime);
        rot = Vector3.Slerp(rot, currentRot, rotationSpeed * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(rot);
    }
}
