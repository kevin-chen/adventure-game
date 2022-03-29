using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMinigame : MonoBehaviour
{
    public GameObject miniGameUI;
    public Transform gameBar;
    public Transform goalBar;
    Vector2 imageStartPos;

    Image gameBarImage;

    bool isPlayerStopped = false;
    int oscillateSpeed = 10;
    int goalPadding = 10;

    void Start()
    {
        imageStartPos = gameBar.position;
        FindInitialGoalPosition();
        gameBarImage = gameBar.GetComponent<Image>();
    }

    void Update()
    {
        if (!PublicVars.isMiniGameActivated)
        {
            //print("not active");
            CheckActivateGame();
        }
        else
        {
            // print("active");
            OscillateBar();
            CheckStopBar();
        }
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
            restartGame();
            ActivateSliderMinigame();
        }
    }

    public static float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    float oscillate(float time, float speed, float scale)
    {
        return Mathf.Cos(time * speed / Mathf.PI) * scale;
    }

    void FindInitialGoalPosition()
    {
        int randomRange = Random.Range(-100, 100);
        Vector2 goalPos = imageStartPos;
        goalPos.x += randomRange;
        goalBar.position = goalPos;
    }

    void CheckStopBar()
    {
        // Ping Pong
        if (Input.GetKeyDown(KeyCode.Space) && !isPlayerStopped)
        {
            isPlayerStopped = true;
            float goalBarLeft = goalBar.position.x - goalPadding;
            float goalBarRight = goalBar.position.x + goalPadding;
            float gameBarPos = gameBar.position.x;
            if (gameBarPos > goalBarLeft && gameBarPos < goalBarRight)
            {
                print("Winner");
                gameBarImage.color = Color.green;
                PublicVars.isSliderMiniGamePassed = true;
            } else {
                gameBarImage.color = Color.red;
            }
            StartCoroutine(FinishMinigame());
        }
    }

    IEnumerator FinishMinigame()
    {
        yield return new WaitForSecondsRealtime(1);
        print("Done waiting");
        TogglePauseGame();
    }

    void OscillateBar()
    {
        if (!isPlayerStopped)
        {
            int maxOscillateVal = 10;
            float pingpong = Mathf.PingPong(Time.unscaledTime, maxOscillateVal);
            // float mapped = Map(pingpong, 0, maxOscillateVal, -2, 2);
            float mapped = oscillate(Time.unscaledTime, oscillateSpeed, 200);
            // print("\nFactor: " + mapped);
            Vector2 pos = imageStartPos;
            // print("Before: " + miniGameBar.position.x);
            pos.x += mapped;
            gameBar.position = pos;
            // print("After: " + miniGameBar.position.x);
        }
    }

    void ActivateSliderMinigame() {
        PublicVars.isMiniGameActivated = true;
        miniGameUI.SetActive(true);
        // TogglePauseGame();
    }

    void DeactivateSliderMinigame() {
        PublicVars.isMiniGameActivated = false;
        miniGameUI.SetActive(false);
        Destroy(gameObject);
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

    void restartGame(){
        if(!PublicVars.isSliderMiniGamePassed){
            isPlayerStopped = false;
            imageStartPos = gameBar.position;
            FindInitialGoalPosition();
            gameBarImage = gameBar.GetComponent<Image>();
        }
        else StartCoroutine(FinishMinigame());
    }
}
