using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Breath : MonoBehaviour
{
    public ParticleSystem fireVFX; 
    public KeyCode activationKey = KeyCode.F; 
    public KeyCode desactivationKey = KeyCode.G; 

    void Update()
    {
        if (Input.GetKeyDown(activationKey))
        {

            fireVFX.Play();
        }if (Input.GetKeyDown(desactivationKey))
        {

            fireVFX.Stop();
        }
    }
}
