using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class TrackStamina : MonoBehaviour
{
    public int maxStamina = 100;
	//public int currentHealth;

	public StaminaBar staminaBar;
    private GameObject player;
    private NavMeshAgent _nmAgent;
    public float originSpeed;
    public int speed_multipler = 2;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _nmAgent = player.GetComponent<NavMeshAgent>();
        originSpeed = _nmAgent.speed;
		//currentHealth = maxHealth;
        PublicVars.stamina = maxStamina;
		staminaBar.SetMaxStamina(maxStamina);


        //healthBar.SetMaxHealth(100);
        //healthBar.SetHealth(PublicVars.health);
    }


    void Update()
    {
        //staminaBar.SetStamina(PublicVars.stamina);
        //SetStamina();
        //StopAllCoroutines();
        if (PublicVars.stamina >= 100) {
            StopAllCoroutines();
            _nmAgent.speed = originSpeed;
        }
        else if (PublicVars.stamina < 0) {
            _nmAgent.speed = 0;
            StartCoroutine(regenerateStamina());
            //IncreaseStamina();
        }
        else if(PublicVars.stamina < 100){
            IncreaseStamina();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && _nmAgent.velocity != Vector3.zero)
        {
            print("shift down");
            _nmAgent.speed = originSpeed * speed_multipler;
        }
        if (Input.GetKey(KeyCode.LeftShift)){
            print("shift hold");
            DecreaseStamina();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && _nmAgent.velocity != Vector3.zero)
        {
            print("shift up");
            _nmAgent.speed = originSpeed;
        }


    }

    public void SetStamina() {
        staminaBar.SetStamina(PublicVars.stamina);
    }

    public void DecreaseStamina() {
        PublicVars.stamina -= (25 * Time.deltaTime); //(200 * Time.deltaTime);
        staminaBar.SetStamina(PublicVars.stamina);
        print("b");
    }

    public void IncreaseStamina() {
        PublicVars.stamina += (10 * Time.deltaTime);
        staminaBar.SetStamina(PublicVars.stamina);
    }

    IEnumerator regenerateStamina() {
        yield return new WaitForSeconds(3f);
        print("a");
        PublicVars.stamina += 10;
        staminaBar.SetStamina(PublicVars.stamina);
    }

}
