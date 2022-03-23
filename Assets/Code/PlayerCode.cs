using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCode : MonoBehaviour
{
    int bulletForce = 500;
    NavMeshAgent _navAgent;
    Camera mainCam;

    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public Transform gun;
    

    // Start is called before the first frame update
    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        mainCam = Camera.main;
        // StartCoroutine(GoRandomPoint());
    }

    // Update is called once per frame
    void Update()
    {
        if (!SliderMinigame.isMiniGameActivated)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, 200))
                {
                    _navAgent.destination = hit.point;
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                lookMouse();

                GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, transform.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(gun.forward * bulletForce);
            }
        }
        else
        {

        }


    }

    void FixedUpdate()
    {
        lookMouse();
    }

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
