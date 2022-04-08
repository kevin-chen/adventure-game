using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    public LayerMask player;
    void Start()
    {
        
    }

    private void FixedUpdate() {
        if(true){
            RaycastHit hit;
            Ray detectRay = new Ray(transform.position, transform.forward * 10);
            Debug.DrawRay(transform.position, transform.up * 10);
            if(Physics.Raycast(transform.position, transform.forward, out hit, 10, player)){
                print("detected");
                PublicVars.isDetected = true;
                StartCoroutine(waitToUndetect());
                print("detect end");
            }
        }
    }

    IEnumerator waitToUndetect(){
        while(true){
            if(PublicVars.isDetected){
                yield return new WaitForSeconds(8);
                PublicVars.isDetected = false;
            } else {
                yield return new WaitForSeconds(0.5f);
                RaycastHit hit;
                Ray detectRay = new Ray(transform.position, transform.forward * 10);
                if(Physics.Raycast(transform.position, transform.forward, out hit, 20, player)){
                    print("detected");
                    continue;
                }
                PublicVars.isDetected = false;
                break;
            }
        }
    }
}
