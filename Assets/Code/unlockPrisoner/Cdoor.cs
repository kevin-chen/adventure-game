using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cdoor : MonoBehaviour
{
    public string code;


    // Update is called once per frame
    private void FixedUpdate() {
        code = transform.name;
    }
    void OnCollisionEnter(Collision other)
    {
        print(code);
        if (other.gameObject.CompareTag("Player") && other.gameObject.transform.Find("hand").Find(code)) {
            Destroy(other.gameObject.transform.Find("hand").Find(code).gameObject);
            transform.Translate(500, 0, 0);
            //Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            print("a");
        }
    }
}
