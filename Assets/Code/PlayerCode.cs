using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCode : MonoBehaviour
{
    //=======navigating========
    NavMeshAgent _navAgent;
    Camera mainCam;

    public GameObject arrowPrefab;

    public LayerMask ground;

    //=======shooting==========
    int bulletForce = 500;
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public Transform gun;
    public AudioClip alarm;


    public float shoot_cooldown = 1;
    private float origin_shootcool;

    AudioSource aud;
    //========animation==========
    Animator _animator;


    //========running==========
    //public int speed_multipler;

    //=========detect===========
    //=========assassinate=======
    public LayerMask back;

    void Start()
    {
        PublicVars.checkPoint = transform.position;
        _navAgent = GetComponent<NavMeshAgent>();


        mainCam = Camera.main;
        _animator = GetComponent<Animator>();

        // shoot_cool
        origin_shootcool = shoot_cooldown;
        // chase_dur
        //PublicVars.origin_chaseDuration = PublicVars.chase_duration;

        GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<BackgroundMusic>().PlayMusic();
    
        aud = GetComponent<AudioSource>();
        aud.clip = alarm;
    }


    void Update()
    {

        if (!PublicVars.isMiniGameActivated)
        {
            // navigating
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, 200, ground))
                {
                    // delete old arrow
                    GameObject oldArrow = GameObject.FindGameObjectWithTag("Arrow");
                    if(oldArrow){
                        Destroy(oldArrow.gameObject);
                    }
                    // create new arrow and navigate
                    Vector3 dest = hit.point;
                    GameObject newArrow = Instantiate(arrowPrefab, dest + new Vector3(0, .0001f, 0), transform.rotation);
                    _navAgent.destination = dest;
                }
            }

            // shooting
            if(PublicVars.shootable){
                if (shoot_cooldown >= 0)
                {
                    shoot_cooldown -= Time.deltaTime;
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    lookMouse();
                    GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, transform.rotation);
                    newBullet.GetComponent<Rigidbody>().AddForce(gun.forward * bulletForce);
                    shoot_cooldown = origin_shootcool;
                }
            }


            // running


            //print("Velocity: " + (_navAgent.velocity != new Vector3(0,0,0)));
            _animator.SetBool("IsMoving", _navAgent.velocity != new Vector3(0,0,0));


            // if (_navAgent.speed > 0)
            // {
            //     print("Speed: " + _navAgent);
            //     _animator.SetTrigger("Walk");
            //     print("Player Walking");
            // }


            // assassinate
            //if (Input.GetKeyDown("e"))
            if(true)
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position - new Vector3(0, .2f, 0), transform.forward);
                if (Physics.Raycast(ray, out hit, 1.1f, back))
                {
                    print("findback");
                    Destroy(hit.collider.transform.parent.gameObject);
                }
            }


            // in or out the detect zone
            if (true)
            {
                RaycastHit hit;
                Ray detectRay = new Ray(transform.Find("feet").position + new Vector3(0, 0.1f, 0), transform.forward);
                Debug.DrawRay(transform.Find("feet").position, transform.forward);
                if (Physics.Raycast(detectRay, out hit, 1f))
                {
                    if (hit.collider.CompareTag("DetectZone"))
                    {
                        print("detected");
                        aud.PlayOneShot(alarm, 0.3f);
                        PublicVars.isDetected = true;
                        PublicVars.chase_duration = 0;
                    }
                }
                else if (PublicVars.chase_duration <= PublicVars.chase_limit)
                {
                    PublicVars.chase_duration += Time.deltaTime;
                }
                else if (PublicVars.chase_duration > PublicVars.chase_limit)
                {
                    PublicVars.isDetected = false;
                    //aud.Stop();
                }
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FirstKey"))
        {
            PublicVars.keyNum += 1;
            PublicVars.hasFirstKey = true;
            print("this happened");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("SecondKey"))
        {
            PublicVars.keyNum += 1;
            PublicVars.hasSecondKey = true;
            Destroy(other.gameObject);

        }
        else if (other.CompareTag("ThirdKey"))
        {
            PublicVars.keyNum += 1;
            PublicVars.hasThirdKey = true;
            Destroy(other.gameObject);
        }
        
        // if (other.CompareTag("FirstDoor"))
        // {
        //     print("hit door");
        //     //Destroy(other.gameObject);
        // }

    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     // be arrested

    //     if (other.gameObject.CompareTag("Guard"))
    //     {
    //         transform.position = PublicVars.checkPoint;
    //         _navAgent.SetDestination(PublicVars.checkPoint);
    //     }
    // } SEE CODE IN GUARDCODE.C


    IEnumerator waitToUndetect()
    {
        while (true)
        {
            if (PublicVars.isDetected)
            {
                yield return new WaitForSeconds(8);
                PublicVars.isDetected = false;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                RaycastHit hit;
                Ray detectRay = new Ray(transform.Find("feet").position + new Vector3(0, 0.1f, 0), transform.forward);
                if (Physics.Raycast(detectRay, out hit, 0.1f))
                {
                    if (hit.collider.CompareTag("DetectZone"))
                    {
                        //print("detected");
                        continue;
                    }
                }
                PublicVars.isDetected = false;
                break;
            }
        }
    }
}
