using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    private Vector3 local_pos;
    // private ParticleSystem particle;

    private void Start() {
        local_pos = transform.position;
        // particle = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        // particle.Stop();
    }


    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            PublicVars.checkPoint = local_pos;
            // particle.Play();
        }
    }
}
