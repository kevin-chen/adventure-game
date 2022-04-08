using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    private Material selfColor;
    public Material deactiveColor;
    public Material activeColor;
    private Vector3 local_pos;
    // private ParticleSystem particle;

    private void Start() {
        local_pos = transform.position;
        selfColor = transform.Find("light").GetComponent<Renderer>().material;
    }

    private void FixedUpdate() {
        if(PublicVars.checkPoint != transform.position){
            selfColor.color = Color.red;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            PublicVars.checkPoint = local_pos;
            print("checked");
            selfColor.color = Color.green;
        }
    }
}
