using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public int maxHealth = 100;
	//public int currentHealth;

	public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
		//currentHealth = maxHealth;
        PublicVars.health = maxHealth;
		healthBar.SetMaxHealth(maxHealth);


        //healthBar.SetMaxHealth(100);
        //healthBar.SetHealth(PublicVars.health);
    }

    // Update is called once per frame
    void Update()
    {
        
		if (Input.GetKeyDown(KeyCode.Space))
		{
			TakeDamage(20);
		}
        healthBar.SetHealth(PublicVars.health);
    }

	void TakeDamage(int damage)
	{
		//currentHealth -= damage;
        PublicVars.health -= damage;

		//healthBar.SetHealth(PublicVars.health);
	}
}
