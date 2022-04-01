using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackStamina : MonoBehaviour
{
    public int maxStamina = 100;
	//public int currentHealth;

	public StaminaBar staminaBar;

    // Start is called before the first frame update
    void Start()
    {
		//currentHealth = maxHealth;
        PublicVars.stamina = maxStamina;
		staminaBar.SetMaxStamina(maxStamina);


        //healthBar.SetMaxHealth(100);
        //healthBar.SetHealth(PublicVars.health);
    }

    // Update is called once per frame
    void Update()
    {
        //staminaBar.SetStamina(PublicVars.stamina);
        SetStamina();
        //StopAllCoroutines();
        if (PublicVars.stamina >= 100) {
            StopAllCoroutines();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {   //(KeyCode.Space)) {
            DecreaseStamina();
        }
        /*
        if (Input.GetKeyUp(KeyCode.Space)) {
            IncreaseStamina();
        } */
        if (PublicVars.stamina < 10) {
            StartCoroutine(regenerateStamina());
            //IncreaseStamina();
        }

    }

    public void SetStamina() {
        staminaBar.SetStamina(PublicVars.stamina);
    }

    public void DecreaseStamina() {
        PublicVars.stamina -= (500 * Time.deltaTime); //(200 * Time.deltaTime);
        staminaBar.SetStamina(PublicVars.stamina);
    }

    public void IncreaseStamina() {
        PublicVars.stamina += (700 * Time.deltaTime);
        staminaBar.SetStamina(PublicVars.stamina);
    }

    IEnumerator regenerateStamina() {
        yield return new WaitForSeconds(5f);
        PublicVars.stamina += 10;
        staminaBar.SetStamina(PublicVars.stamina);
    }

}
