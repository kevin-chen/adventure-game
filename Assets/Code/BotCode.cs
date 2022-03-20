using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotCode : MonoBehaviour
{

    NavMeshAgent _navAgent;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FindPlayer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FindPlayer() {
        while (true) {
            yield return new WaitForSeconds(0.5f);
            _navAgent.destination = player.transform.position;
        }
    }
}
