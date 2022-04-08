using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
public class GuardCode : MonoBehaviour
{
    //============UI==========
    public TextMeshProUGUI money;

    
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

    public float detectDistance = 15;
    //==========animator=====================
    Animator _ani;

    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        _playerAgent = player.GetComponent<NavMeshAgent>();
        _ani = GetComponent<Animator>();
        movingCenter = transform.position;
        originSpd = _navAgent.speed;
        newSpd = originSpd * 1.5f;
        //StartCoroutine(GuardDecisionLogic());
    }

    private void FixedUpdate() {
        // show the exclamation mark
        if(PublicVars.isDetected){
            transform.Find("head").gameObject.SetActive(true);
        }
        else{
            transform.Find("head").gameObject.SetActive(false);
        }
    
        // switch between randomly move and chasing
        if(PublicVars.chase_duration > PublicVars.chase_limit){
            if(_navAgent.velocity == Vector3.zero){
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
        }
        else if(PublicVars.chase_duration <= PublicVars.chase_limit 
                && Vector3.Distance(transform.position, player.transform.position) <= detectDistance){
            _navAgent.speed = newSpd;
            _navAgent.destination = player.transform.position;
        }

        setDetectRange();
    }

    private void Update() {
        //animation
        _ani.SetBool("IsMoving", _navAgent.velocity != Vector3.zero);

        //collide and arrest
        if(true){
            RaycastHit hit;
            Ray detectRay = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward);
            if (Physics.Raycast(detectRay, out hit, 1f, playerMask))
            {
                print("arrested");
                //aud.PlayOneShot(alarm);
                hit.collider.isTrigger = true;
                hit.collider.transform.position = PublicVars.checkPoint;
                _playerAgent.SetDestination(PublicVars.checkPoint);
                hit.collider.isTrigger = false;
                PublicVars.health -= 100;
                print(PublicVars.health);
                money.text = " $" + PublicVars.health;
                
            }

        }
    }



    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet")) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }


    void setDetectRange(){
        if(PublicVars.isPickedUp){
            detectDistance = 1000;
        }
        else{
            detectDistance = 15;
        }
    }
}
