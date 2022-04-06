using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasscodeMinigame : MonoBehaviour
{

    public int answer = 4301;
    int sum = 0;
    int count = 0;

    public GameObject miniGameUI;
    public TextMeshProUGUI codeUI;

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
        if (buttonNum == -1) {
            sum = 0;
            count = 0;
        }
        else if (count < 4) {
            print("Button Pressed: " + buttonNum);
            sum = (sum * 10) + buttonNum;
            count += 1;
            print("Current Sum: " + sum);
        }

        if (count >= 4) { // Done Entering Code
            if (answer == sum) { // Correct Code
                print("Correct Code!!");
                PublicVars.isPasscodeMiniGamePassed = true;
                StartCoroutine(FlashIsCorrectIndicator(true));
            } else { // Wrong Code
                print("Wrong Code!!");
                PublicVars.isPasscodeMiniGamePassed = false;
                StartCoroutine(FlashIsCorrectIndicator(false));
            }
            StartCoroutine(FinishMinigame());
        } 

        if (sum != 0) codeUI.text = "" + sum;
        else codeUI.text = "";
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

    IEnumerator FlashIsCorrectIndicator(bool isCorrect) {
        if (!isCorrect) {
            codeUI.color = Color.red;
        } else {
            codeUI.color = Color.green;
            PublicVars.isPasscodeMiniGamePassed = true;
        }
        yield return new WaitForSecondsRealtime(1);
        // codeUI.color = Color.white;
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
        if (PublicVars.isPasscodeMiniGamePassed)
        {
            Destroy(gameObject);
        }
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
