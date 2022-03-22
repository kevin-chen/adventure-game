using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{

    bool isMiniGameActivated = false;
    public GameObject miniGameUI;
    public Transform miniGameBar;
    public Transform goalBar;
    Vector2 imageStartPos;

    bool isPlayerStopped = false;

    void Start()
    {
        imageStartPos = miniGameBar.position;
        FindInitialGoalPosition();
    }

    void Update()
    {
        OscillateBar();
        CheckActivateGame();
        CheckStopBar();
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
        }
        if (isPlayerStopped)
        {
            float goalBarLeft = goalBar.position.x - 30;
            float goalBarRight = goalBar.position.x + 30;
            float gameBar = miniGameBar.position.x;
            if (gameBar > goalBarLeft && gameBar < goalBarRight)
            {
                print("Winner");
            }
        }
    }

    void OscillateBar()
    {
        if (!isPlayerStopped)
        {
            int maxOscillateVal = 10;
            float pingpong = Mathf.PingPong(Time.time, maxOscillateVal);
            // float mapped = Map(pingpong, 0, maxOscillateVal, -2, 2);
            float mapped = oscillate(Time.time, 7, 200);
            // print("\nFactor: " + mapped);
            Vector2 pos = imageStartPos;
            // print("Before: " + miniGameBar.position.x);
            pos.x += mapped;
            miniGameBar.position = pos;
            // print("After: " + miniGameBar.position.x);
        }
    }

    void CheckActivateGame()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!isMiniGameActivated)
            {
                isMiniGameActivated = true;
                miniGameUI.SetActive(true);
                // Time.timeScale = 0f;
            }
            else
            {
                isMiniGameActivated = false;
                miniGameUI.SetActive(false);
            }
        }
    }
}
