using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misc : MonoBehaviour
{
    // Start is called before the first frame update

    public string lol = "Disable";
    void Start()
    {
        Invoke(lol, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
