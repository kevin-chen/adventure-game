using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PickUp : MonoBehaviour
{
    public float throwForce = 600;
    Vector3 objectPos;
    public float distance;
    public GameObject bomb;
    public Transform bombPos;
    public bool isHolding = false;
    Rigidbody _bombRig;

    AudioSource aud;
    public AudioClip explosion;
    public float timeUntilExplosion = 15;
    public TextMeshProUGUI countdown;
    public Camera mainCam;


    void Start() {
        aud = GetComponent<AudioSource>();
        bomb = GameObject.FindGameObjectWithTag("bomb");
        _bombRig = bomb.GetComponent<Rigidbody>();
        
    } 

    private void FixedUpdate() {
        if(bomb){
            Timer();
        }
    }
    void Update () {
        
        if(bomb){

            distance = Vector3.Distance(bomb.transform.position, bombPos.position);
            if (distance >= 5f) 
            {
                isHolding = false;
            }

    //Check if isholding
            if (isHolding) {
                if (Input.GetKeyDown(KeyCode.E)) {

                    _bombRig.isKinematic = false;
                    bomb.transform.SetParent(null);
                    bomb.GetComponent<Rigidbody>().AddForce(bombPos.transform.forward * throwForce);
                    isHolding = false;

                }
            }

            if (Input.GetMouseButtonDown(0) && distance <= 5)
            {
                RaycastHit hit;
                if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, 200))
                {
                    if(hit.collider.CompareTag("bomb")){
                        PublicVars.isPickedUp = true;
                        _bombRig.isKinematic = true;
                        isHolding = true;
                        bomb.transform.position = bombPos.position;
                        bomb.transform.parent = transform;
                    }
                }
            }
        }

    }


    // void OnMouseDown() 
    // {
    //     if (distance <= 5f)
    //     {
    //         if(gameObject.name == "bomb"){
    //             print("click");
    //             _bombRig = gameObject.GetComponent<Rigidbody>();
    //             _bombRig.isKinematic = true;
    //             isHolding = true;
    //             PublicVars.isPickedUp = true;
    //             gameObject.transform.parent = transform;
    //             gameObject.transform.position = transform.Find("Bombposition").position;
    //             // gameObject.GetComponent<Rigidbody>().useGravity = false;
    //             // gameObject.GetComponent<Rigidbody>().detectCollisions = true;
    //         }

    //     }
    // }
 
//this will later be modified to decrease the player's health if the
//player is one of the nearby objects

    void Explode() {
        Collider[] nearby = Physics.OverlapSphere(bomb.transform.position, 25);
        //GameObject[] nearby = Physics.OverlapSphere(bomb.transform.position, 25);
        foreach (var obj in nearby) {
            if (obj.CompareTag("ThingToDestroy")) {
                Destroy(obj.gameObject);
            }
        }
        Destroy(bomb.gameObject);
    }

    void Timer(){
        if(PublicVars.isPickedUp){
            if (timeUntilExplosion > 0) {
                timeUntilExplosion -= Time.deltaTime;    
                countdown.text = Mathf.Round(timeUntilExplosion) + ":" + (Mathf.Round(timeUntilExplosion * 100) % 100);

            }
            else {
                Explode();
                countdown.text = "0:00";
                aud.PlayOneShot(explosion);
            }
        }
    }
}
