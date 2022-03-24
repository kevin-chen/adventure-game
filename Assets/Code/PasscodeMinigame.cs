using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasscodeMinigame : MonoBehaviour
{

    int answer = 4444;
    int sum = 0;
    int count = 0;

    public GameObject miniGameUI;

    // Start is called before the first frame update
    void Start()
    {
        PasscodePressButton.ButtonPress += PressingButton;
    }

    public void TogglePauseGame()
    {
        print("Toggling Pause");
        if(PublicVars.isMiniGameActivated)
        {
            Time.timeScale = 1f;
            DeactivateSliderMinigame();
        }
        else
        {
            Time.timeScale = 0;
            ActivateSliderMinigame();
        }
    }

    private void PressingButton(int buttonNum) {
        if (count < 4) {
            print("Button Pressed: " + buttonNum);
            sum = (sum * 10) + buttonNum;
            count += 1;
            print("Current Sum: " + sum);
        }

        if (answer == sum) {
            print("HEYY!!");
            StartCoroutine(FinishMinigame());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PublicVars.isMiniGameActivated)
        {
            CheckActivateGame();
        }
        else
        {

        }
    }

    IEnumerator FinishMinigame()
    {
        yield return new WaitForSecondsRealtime(1);
        print("Done waiting");
        TogglePauseGame();
    }

    void ActivateSliderMinigame() {
        PublicVars.isMiniGameActivated = true;
        miniGameUI.SetActive(true);
        // TogglePauseGame();
    }

    void DeactivateSliderMinigame() {
        PublicVars.isMiniGameActivated = false;
        miniGameUI.SetActive(false);
        // TogglePauseGame();
    }

    void CheckActivateGame()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!PublicVars.isMiniGameActivated)
            {
                ActivateSliderMinigame();
                TogglePauseGame();
            }
            else
            {
                DeactivateSliderMinigame();
            }
        }
    }
}
