using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardCode : MonoBehaviour
{
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

    //==========detect===================
    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        movingCenter = transform.position;
        //StartCoroutine(GuardDecisionLogic());
    }

    private void FixedUpdate() {
        if(PublicVars.isDetected){
            transform.Find("head").gameObject.SetActive(true);
        }
        else{
            transform.Find("head").gameObject.SetActive(false);
        }

        if(PublicVars.chase_duration < 0){
            if(_navAgent.velocity == new Vector3(0,0,0)){
                xMovement = Random.Range(-moving_xRange - movingDiff.x, moving_xRange - movingDiff.x);
                zMovement = Random.Range(-moving_zRange - movingDiff.z, moving_zRange - movingDiff.z);
                // destination
                Vector3 dest =  movingCenter + new Vector3(xMovement, 0, zMovement);
                // adjust difference
                movingDiff = dest - movingCenter;
                _navAgent.SetDestination(dest);
            }
        }
        else if(PublicVars.chase_duration >= 0){
            _navAgent.destination = player.transform.position;
        }


    }

    IEnumerator GuardDecisionLogic() {
        while (true) {
            bool isPlayerDetected = PublicVars.isDetected;
            if (isPlayerDetected) {
                yield return new WaitForSeconds(0.1f);
                _navAgent.destination = player.transform.position;
            } else {
                yield return new WaitForSeconds(0.4f);
                if(PublicVars.isDetected) continue;
                yield return new WaitForSeconds(moving_Cooldown - 0.4f);

                
                xMovement = Random.Range(-moving_xRange - movingDiff.x, moving_xRange - movingDiff.x);
                zMovement = Random.Range(-moving_zRange - movingDiff.z, moving_zRange - movingDiff.z);
                // destination
                Vector3 dest =  movingCenter + new Vector3(xMovement, 0, zMovement);
                // adjust difference
                movingDiff = dest - movingCenter;
                _navAgent.SetDestination(dest);
            }
        }
    }


    IEnumerator newLogic(){
        while(_navAgent.speed <= 0.1){
            if(PublicVars.isDetected){
                yield return new WaitForSeconds(0.5f);
                _navAgent.destination = player.transform.position;
            } else{
                yield return new WaitForSeconds(0.5f);
                if(PublicVars.isDetected) continue;
                yield return new WaitForSeconds(moving_Cooldown - 0.5f);

                xMovement = Random.Range(-moving_xRange - movingDiff.x, moving_xRange - movingDiff.x);
                zMovement = Random.Range(-moving_zRange - movingDiff.z, moving_zRange - movingDiff.z);
                // destination
                Vector3 dest =  movingCenter + new Vector3(xMovement, 0, zMovement);
                // adjust difference
                movingDiff = dest - movingCenter;
                _navAgent.SetDestination(dest);
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
}
