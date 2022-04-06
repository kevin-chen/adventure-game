using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ckey : MonoBehaviour
{
    public string code;
    void Start()
    {
        code = transform.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            //if(other.transform.Find("hand").transform.childCount == 0){
            transform.position = other.transform.Find("hand").position;
            transform.parent = other.transform.Find("hand");
            //}
        }
    }
}
