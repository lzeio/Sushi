using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    public float shakeTimer;
   
    public static CameraShake csInstance;
    private void Awake()
    {
        cinemachineVirtualCamera= GetComponent<CinemachineVirtualCamera>();
        csInstance = this;
    }

    public void ShakeCamera(float intensity, float duration)
    {
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = duration;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                 cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                 cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }      
    }
}
//public IEnumerator Shake (float duration, float magnitude)
    //{
    //    Vector3 originalPos = transform.localPosition;
    //    float elapsed = 0.0f;
    //    while (elapsed<duration)
    //    {
    //        float x = Random.Range(-5f, 5f) * magnitude;
    //        float y = Random.Range(-5f, 5f) * magnitude;

    //        transform.position = new Vector3(x, y, originalPos.z);

    //        elapsed += Time.deltaTime;

    //        yield return null;
    //    }

    //    transform.position = originalPos;
    //}
    // Start is called before the first frame update