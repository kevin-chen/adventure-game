using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSliderMinigame : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PublicVars.isMiniGameActivated)
        {
            print("Touching Player");
            SliderMinigame script = gameObject.GetComponent<SliderMinigame>();
            //Destroy(gameObject);
            script.TogglePauseGame();
        }
    }
}
