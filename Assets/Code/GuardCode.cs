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

    public float moving_Cooldown = 2.5f;
    public float moving_xRange = 5;
    public float moving_zRange = 5;

    // Start is called before the first frame update
    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player");
        MovingCenter = transform.position;
        StartCoroutine(GoRandomPoint());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FindPlayer() {
        while (true) {
            yield return new WaitForSeconds(1.5f);
            _navAgent.destination = player.transform.position;
        }
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
        while (true)
        {
            yield return new WaitForSeconds(moving_Cooldown);
            float xMovement = Random.Range(-moving_xRange, moving_xRange);
            float zMovement = Random.Range(-moving_zRange, moving_zRange);
            Vector3 point = new Vector3(xMovement, 0, zMovement);
            MovingCenter -= point;
            _navAgent.destination = point;
        }
    }
}
