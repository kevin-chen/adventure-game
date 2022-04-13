using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cdoor : MonoBehaviour
{
    public PrisonerCode prisonerScript;
    private GameObject[] prisoners;
    public string code;
    private void Start() {

        
        
        
    }

    // Update is called once per frame
    private void FixedUpdate() {
        code = transform.name;
        if(Input.GetKeyDown("k")){
            print(checkAllRelease());
        }

    }

    int checkAllRelease(){
        prisoners = GameObject.FindGameObjectsWithTag("Prisoner");
        int count = 0;
        foreach(GameObject i in prisoners){
            prisonerScript = i.GetComponent<PrisonerCode>();
            if(prisonerScript.isFree){
                //print(prisonerScript.isFree);
                count++;
                //print(count);
            }
        }
        
        return count;
    }
    void OnCollisionEnter(Collision other)
    {
        print(code);
        if (other.gameObject.CompareTag("Player") && other.gameObject.transform.Find("hand").Find(code)) {
            Destroy(other.gameObject.transform.Find("hand").Find(code).gameObject);
            Destroy(transform.gameObject);
            //transform.Translate(500, 0, 0);

        }

        if(other.gameObject.CompareTag("Player") && code == "onetotwo" && checkAllRelease() >= 3){
            Destroy(transform.gameObject);
            //transform.Translate(500, 0, 0);
        }
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            print("a");
        }
    }
}
