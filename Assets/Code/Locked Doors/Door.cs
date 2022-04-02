using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Transform myTrans;
    public Rigidbody myBody;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        myTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && (PublicVars.hasFirstKey == true))
        {
            print("Touching Player");
            //transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
            myTrans.Translate(500, 0, 0);
            //myBody.AddForce(500,0,0);
            //Destroy(gameObject);
        }
    }

    private void OnCollisionExit(Collision other) {
        //print("Touching Player exit");
    }

}
