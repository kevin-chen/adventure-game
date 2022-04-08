using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class PrisonerCode : MonoBehaviour
{
    //=======
    NavMeshAgent _navAgent;
    GameObject player;


    //=======random moving within range========
    public Vector3 movingCenter;
    public Vector3 movingDiff;
    private Vector3 respawnPos;

    public float moving_Cooldown = 5.5f;
    public float moving_xRange = 3;
    public float moving_zRange = 3;
    public float xMovement;
    public float zMovement;

    //==========detect&arrest==================
    public LayerMask playerMask;
    private NavMeshAgent _playerAgent;

    private float originSpd;
    private float newSpd;
    //==========animator=====================
    Animator _ani;
    //============release====================
    public bool isFree = false;
    public string pName;
    //==========combate===============
    public LayerMask guardMask;


    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        _playerAgent = player.GetComponent<NavMeshAgent>();
        _ani = GetComponent<Animator>();
        movingCenter = transform.position;
        originSpd = _navAgent.speed;
        newSpd = originSpd * 2f;

        pName = PublicVars.names[Random.Range(0,39)];
        //GameObject newName = Instantiate(pName, transform.position, transform.rotation);
    }

    private void FixedUpdate() {



    }


    private void Update() {
        //animation
        _ani.SetBool("IsMoving", _navAgent.velocity != Vector3.zero);
        
        if(!isFree && _navAgent.velocity == Vector3.zero){

                //reset speed
                _navAgent.speed = originSpd;
                // destination
                xMovement = Mathf.Clamp(Random.Range(-moving_xRange - movingDiff.x, moving_xRange - movingDiff.x), -moving_xRange *2, moving_xRange*2);
                zMovement = Mathf.Clamp(Random.Range(-moving_zRange - movingDiff.z, moving_zRange - movingDiff.z), -moving_zRange *2, moving_zRange*2);
                Vector3 dest =  movingCenter + new Vector3(xMovement, 0, zMovement);
                // adjust difference
                movingDiff = dest - movingCenter;
                _navAgent.SetDestination(dest);
        }
        else if(isFree && PublicVars.isPickedUp){
            if(Vector3.Distance(GameObject.FindGameObjectWithTag("Guard").transform.position, player.transform.position) <= 5){
                _navAgent.SetDestination(GameObject.FindGameObjectWithTag("Guard").transform.position);
            }else{
                _navAgent.SetDestination(player.transform.position);
            }
        }
        else if(isFree && (Vector3.Distance(player.transform.position, transform.position) >= 3f) ){
            _navAgent.SetDestination(player.transform.position);
        }
        else if(isFree && (Vector3.Distance(player.transform.position, transform.position) < 3f ) ){
            _navAgent.SetDestination(transform.position);
        }




        if(true){
            RaycastHit hit;
            Ray detectRay = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward);
            if (Physics.Raycast(detectRay, out hit, 3f, playerMask))
            {
                print(pName + " is free");
                // PublicVars.pRelease[pNum] = true;
                isFree = true;
                _navAgent.speed = newSpd;
            }

        }

    }


    private void OnCollisionEnter(Collision other) {
        if(PublicVars.isPickedUp && other.gameObject.CompareTag("Guard")){
            Destroy(other.gameObject);
        }
    }


}
