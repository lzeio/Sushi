using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    public GameObject[] weapons;

   void Update()
   {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Q");
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("E");
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
            weapons[2].SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("R");
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(true);
        }
   }
}
