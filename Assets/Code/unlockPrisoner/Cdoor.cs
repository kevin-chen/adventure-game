using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cdoor : MonoBehaviour
{
    public string code;


    // Update is called once per frame
    private void Start() {
        code = transform.name;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.transform.Find("hand")){
            print("a");
            Destroy(transform.gameObject);
            Destroy(other.collider.transform.Find("hand").Find(code));
        }
    }
}
