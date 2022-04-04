using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
public float throwForce = 600;
 Vector3 objectPos;
 float distance;

 public bool canHold = true;
 public GameObject item;
 public GameObject tempParent;
 public bool isHolding = false;

 AudioSource aud;
 public AudioClip explosion;

// set tempParent to Bomb position under playerModel

void Start() {
    aud = GetComponent<AudioSource>();
} 

 // Update is called once per frame
 void Update () {

  distance = Vector3.Distance (item.transform.position, tempParent.transform.position);
  if (distance >= 5f) 
  {
   isHolding = false;
  }
  


  //Check if isholding
  if (isHolding == true) {
   item.GetComponent<Rigidbody> ().velocity = Vector3.zero;
   item.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
   item.transform.SetParent (tempParent.transform);
   StartCoroutine(Explode());

   if (Input.GetKeyDown(KeyCode.E)) {
    item.GetComponent<Rigidbody> ().AddForce (tempParent.transform.forward * throwForce);
    isHolding = false;
    StopAllCoroutines();
    aud.PlayOneShot(explosion);
    StartCoroutine(Explode_After_Throw());
   }
  }
  else 
  {
   objectPos = item.transform.position;
   item.transform.SetParent (null);
   item.GetComponent<Rigidbody> ().useGravity = true;
   item.transform.position = objectPos;
  }
 }

 void OnMouseDown() 
 {
  if (distance <= 5f)
  {
   isHolding = true;
   item.GetComponent<Rigidbody> ().useGravity = false;
   item.GetComponent<Rigidbody> ().detectCollisions = true;
   
  }
 }
 
//this will later be modified to decrease the player's health if the
//player is one of the nearby objects

IEnumerator Explode() {
        yield return new WaitForSeconds(20);
        Collider[] nearby = Physics.OverlapSphere(item.transform.position, 25);
        //GameObject[] nearby = Physics.OverlapSphere(item.transform.position, 25);
        foreach (var obj in nearby) {
            if (!obj.gameObject.CompareTag("Player") && !obj.CompareTag("Floor")) {
                Destroy(obj.gameObject);
            }
        }
        Destroy(item.gameObject);
    }

    IEnumerator Explode_After_Throw() {
        yield return new WaitForSeconds(0.5f);
        Collider[] nearby = Physics.OverlapSphere(item.transform.position, 25);
        //GameObject[] nearby = Physics.OverlapSphere(item.transform.position, 25);
        foreach (var obj in nearby) {
            if (!obj.gameObject.CompareTag("Player") && !obj.CompareTag("Floor")) {
                Destroy(obj.gameObject);
            }
        }
        //aud.PlayOneShot(explosion);
        Destroy(item.gameObject);
    }

 /*
 void OnMouseUp() 
 {
  isHolding = false;
 } */
}
