using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCode : MonoBehaviour
{
    //=======navigating========
    NavMeshAgent _navAgent;
    Camera mainCam;

    //=======shooting==========
    int bulletForce = 500;
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public Transform gun;

    public float shoot_cooldown = 1;
    private float origin_shootcool;


    //========running==========
    public int speed_multipler = 2;


    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        mainCam = Camera.main;
        // StartCoroutine(GoRandomPoint());

        // shoot_cool
        origin_shootcool = shoot_cooldown;

    }


    void Update()
    {
        if (!PublicVars.isMiniGameActivated)
        {
            // navigating
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, 200))
                {
                    _navAgent.destination = hit.point;
                }
            }

            // shooting
            if(shoot_cooldown >= 0){
                shoot_cooldown -= Time.deltaTime;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                lookMouse();
                GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, transform.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(gun.forward * bulletForce);
                shoot_cooldown = origin_shootcool;
            }


            // running
            if (Input.GetKeyDown("left shift"))
            {
                _navAgent.speed *= speed_multipler;
            }
            if (Input.GetKeyUp("left shift"))
            {
                _navAgent.speed /= speed_multipler;
            }
        }
    }

    void FixedUpdate()
    {
        lookMouse();
    }

    // gun direction
    private void lookMouse()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, 200))
        {
            Vector3 target = hit.point;
            target.y = spawnPoint.position.y;
            gun.LookAt(target);
        }
    }

    // enemy AI?
    IEnumerator GoRandomPoint()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Vector3 point = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
            _navAgent.destination = point;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            PublicVars.keyNum += 1;
            Destroy(other.gameObject);
        }
    }
}
