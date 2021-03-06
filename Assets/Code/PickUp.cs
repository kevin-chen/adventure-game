using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class PickUp : MonoBehaviour
{
    public float throwForce = 600;
    Vector3 objectPos;
    public float distance;
    public GameObject bomb;
    public GameObject bombPrefab;
    public Transform bombSpawn;
    public Transform bombPos;
    public bool isHolding = false;
    Rigidbody _bombRig;

    AudioSource aud;
    public AudioClip explosion;
    public float timeUntilExplosion = 15;
    private float originalTime;
    public TextMeshProUGUI countdown;
    public Camera mainCam;

    private NavMeshAgent _nmAgent;




    void Start() {

        originalTime = timeUntilExplosion;
        aud = GetComponent<AudioSource>();
        //bomb = GameObject.FindGameObjectWithTag("bomb");
        _nmAgent = GetComponent<NavMeshAgent>();
        //_bombRig = bomb.GetComponent<Rigidbody>();

        countdown.gameObject.SetActive(false);
        transform.Find("Gun Pivot").gameObject.SetActive(false);

        
    } 

    private void FixedUpdate() {
        // bomb = GameObject.FindGameObjectWithTag("bomb");
        // _bombRig = bomb.GetComponent<Rigidbody>();
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
                    bomb.GetComponent<Collider>().enabled = true;
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
                        bomb = hit.collider.gameObject;
                        _bombRig = bomb.GetComponent<Rigidbody>();
                        hit.collider.enabled = false;
                        transform.Find("Gun Pivot").gameObject.SetActive(true);
                        countdown.gameObject.SetActive(true);
                        PublicVars.isPickedUp = true;
                        _bombRig.isKinematic = true;
                        isHolding = true;
                        PublicVars.shootable = true;
                        bomb.transform.position = bombPos.position;
                        bomb.transform.parent = transform;
                    }
                }
            }
        }

    }

 
//this will later be modified to decrease the player's health if the
//player is one of the nearby objects

    void Explode() {
        Collider[] nearby = Physics.OverlapSphere(bomb.transform.position, 25);
        //GameObject[] nearby = Physics.OverlapSphere(bomb.transform.position, 25);
        foreach (var obj in nearby) {
            if(obj.CompareTag("ThingToDestroy")){
                obj.transform.name = "b";
                Destroy(obj.gameObject);
            }
            print(obj.tag);
        }
        foreach (var obj in nearby) {
            if(obj.transform.name == "b"){
                countdown.gameObject.SetActive(false);
                PublicVars.isPickedUp = false;
                Destroy(bomb.gameObject);
                transform.Find("Gun Pivot").gameObject.SetActive(false);
                PublicVars.shootable = false;
                _nmAgent.SetDestination(obj.transform.position);
                StartCoroutine(wait2sec());

            }
        }
        countdown.gameObject.SetActive(false);
        PublicVars.isPickedUp = false;
        Destroy(bomb.gameObject);
        print("lost");
        _nmAgent.Warp(PublicVars.checkPoint);
        bomb = Instantiate(bombPrefab, bombSpawn.position, Quaternion.Euler(0,0,0));
        _bombRig = bomb.GetComponent<Rigidbody>();
        timeUntilExplosion = originalTime;
            
    }

    void Timer(){
        if(PublicVars.isPickedUp){
            if (timeUntilExplosion > 0) {
                timeUntilExplosion -= Time.deltaTime;    
                countdown.text = Mathf.Round(timeUntilExplosion) + ":" + (Mathf.Round(timeUntilExplosion * 100) % 100);

            }
            else {
                Explode();
                countdown.text = "";
                aud.PlayOneShot(explosion);
            }
        }
    }


    IEnumerator wait2sec(){
        yield return new WaitForSeconds(2f);
        transform.Find("Gun Pivot").gameObject.SetActive(false);
        SceneManager.LoadScene("WinScreen");
    }
}
