using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemAssistant : MonoBehaviour
{

    public bool activateParticleSystem = false;
    public ParticleSystem ps;

    public void FixedUpdate()
    {
        if( activateParticleSystem )
        {
            ps.Play();
            Debug.Log("Activating particle system!");
            activateParticleSystem = false;
        }
    }
}
