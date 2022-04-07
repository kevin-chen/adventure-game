using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PickUp : MonoBehaviour
{
    public float throwForce = 600;
    Vector3 objectPos;
    Vector3 startPos;
    float distance;

    public bool canHold = true;
    public GameObject item;
    public GameObject tempParent;
    public bool isHolding = false;

    AudioSource aud;
    public AudioClip explosion;

    public float timeUntilExplosion = 30;

    public TextMeshProUGUI countdown;

    // set tempParent to Bomb position under playerModel

    void Start() {
        aud = GetComponent<AudioSource>();
        startPos = item.transform.position;
    } 

 // Update is called once per frame
    void Update () {
        Timer();

        countdown.text = "0:" + (int)(timeUntilExplosion);

        distance = Vector3.Distance (item.transform.position, tempParent.transform.position);
        if (distance >= 5f) 
        {
            isHolding = false;
        }

  //Check if isholding
        if (isHolding) {
            item.GetComponent<Rigidbody> ().velocity = Vector3.zero;
            item.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
            item.transform.SetParent (tempParent.transform);
            item.transform.position = tempParent.transform.position;
            //StartCoroutine(Explode());

            // if (timeUntilExplosion > 0) {
            //     timeUntilExplosion -= Time.deltaTime;    
            //     countdown.text = (int)(timeUntilExplosion / 10) + ":" + (timeUntilExplosion % 10 * 100);
            // }
            // else {
            //     Explode();
            //     countdown.text = "0:00";
            //     aud.PlayOneShot(explosion);
            // }

            if (Input.GetKeyDown(KeyCode.E)) {
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;
                //StopAllCoroutines();
                //aud.PlayOneShot(explosion);
                //StartCoroutine(Explode_After_Throw());
            }
        }
        else 
        {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
        }
    }

    void OnMouseDown() 
    {
        if (distance <= 5f)
        {
            isHolding = true;
            PublicVars.isPickedUp = true;
            item.GetComponent<Rigidbody> ().useGravity = false;
            item.GetComponent<Rigidbody> ().detectCollisions = true;

        }
    }
 
//this will later be modified to decrease the player's health if the
//player is one of the nearby objects

    void Explode() {
        Collider[] nearby = Physics.OverlapSphere(item.transform.position, 25);
        //GameObject[] nearby = Physics.OverlapSphere(item.transform.position, 25);
        foreach (var obj in nearby) {
            if (obj.CompareTag("ThingToDestroy")) {
                Destroy(obj.gameObject);
            }
        }
        Destroy(item.gameObject);
    }

    void Timer(){
        if(PublicVars.isPickedUp){
            if (timeUntilExplosion > 0) {
                timeUntilExplosion -= Time.deltaTime;    
                countdown.text = (int)(timeUntilExplosion / 10) + ":" + (timeUntilExplosion % 10 * 100);
            }
            else {
                Explode();
                countdown.text = "0:00";
                aud.PlayOneShot(explosion);
            }
        }
    }
    // IEnumerator Explode_After_Throw() {
    //     yield return new WaitForSeconds(0.5f);
    //     Collider[] nearby = Physics.OverlapSphere(item.transform.position, 25);
    //     //GameObject[] nearby = Physics.OverlapSphere(item.transform.position, 25);
    //     foreach (var obj in nearby) {
    //         if (!obj.gameObject.CompareTag("Player") && !obj.CompareTag("Floor")) {
    //             Destroy(obj.gameObject);
    //         }
    //     }
    //     //aud.PlayOneShot(explosion);
    //     Destroy(item.gameObject);
    // }


}
