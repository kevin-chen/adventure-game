using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePasscodeMinigame : MonoBehaviour
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
        if (other.CompareTag("Player") && PublicVars.hasFirstKey && PublicVars.hasSecondKey && !PublicVars.isMiniGameActivated && !PublicVars.isPasscodeMiniGamePassed)
        {
            print("Touching Player");
            PasscodeMinigame script = gameObject.GetComponent<PasscodeMinigame>();
            // Destroy(gameObject);
            script.TogglePauseGame();
        }
    }
}
