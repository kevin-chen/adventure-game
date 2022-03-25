using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardCode : MonoBehaviour
{
    NavMeshAgent _navAgent;
    GameObject player;


    //=======random moving within range========
    private Vector3 MovingCenter;
    private Vector3 respawnPos;

    public float moving_Cooldown = 4f;
    public float moving_xRange = 3;
    public float moving_zRange = 3;


    //==========detect===================
    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        MovingCenter = new Vector3(0, 0, 0);
        StartCoroutine(GoRandomPoint());

    }

    private void FixedUpdate() {
        if(PublicVars.isDetected){
            transform.Find("head").gameObject.SetActive(true);
        }
        else{
            transform.Find("head").gameObject.SetActive(false);
        }

    }

    IEnumerator FindPlayer() {
        while (PublicVars.isDetected) {
            yield return new WaitForSeconds(0.5f);
            _navAgent.destination = player.transform.position;
        }
        StartCoroutine(GoRandomPoint());
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet")) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    IEnumerator GoRandomPoint()
    {

        while(!PublicVars.isDetected){
            yield return new WaitForSeconds(moving_Cooldown);

            if(PublicVars.isDetected) break;
            float xMovement = Random.Range(-moving_xRange - MovingCenter.x, moving_xRange - MovingCenter.x);
            float zMovement = Random.Range(-moving_zRange - MovingCenter.z , moving_zRange - MovingCenter.z);

            Vector3 point = new Vector3(xMovement, 0, zMovement);
            MovingCenter += point;
            _navAgent.destination = point;
        }
        StartCoroutine(FindPlayer());
    }


    
}
