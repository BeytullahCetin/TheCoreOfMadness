using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLureController : LureController
{
    [SerializeField] Light[] lightsToDisable;
    [SerializeField] AudioSource lightLureAudioSource;

    public override void Interaction()
    {
        if (canInteractable && !isUsed)
        {
            lightLureAudioSource.Play();
            
            isUsed = true;

            ActivateLure();
            DisableLights();
        }
    }

    void DisableLights()
    {
        foreach (Light light in lightsToDisable)
        {
            light.enabled = false;
        }
    }

}
