using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : MonoBehaviour
{
    // Start is called before the first frame update
    Transform myTrans;
    void Start()
    {
        myTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && PublicVars.hasSecondKey == true)
        {
            //print("Touching Player");
            //Destroy(gameObject);
            myTrans.Translate(500, 0, 0);
        }
    }
}
