using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{

    bool isMiniGameActivated = false;
    public GameObject miniGameUI;
    public Transform miniGameImage;

    Vector2 imageStartPos;

    bool isPlayerStopped = false;

    void Start()
    {
        imageStartPos = miniGameImage.position;
    }

    void Update()
    {
        // Lerp

        // float pingpong = Mathf.Lerp(-10, 10, Time.deltaTime);
        // print(pingpong);
        // Vector2 pos = miniGameImage.position;
        // pos.x += pingpong;
        // miniGameImage.position = pos;




        // Ping Pong

        if (Input.GetKeyDown(KeyCode.Space) && !isPlayerStopped)
        {
            isPlayerStopped = true;
        }
        if (!isPlayerStopped)
        {
            int maxOscillateVal = 10;
            float pingpong = Mathf.PingPong(Time.time, maxOscillateVal);
            // float mapped = Map(pingpong, 0, maxOscillateVal, -2, 2);
            float mapped = oscillate(Time.time, 7, 200);
            print("\nFactor: " + mapped);
            Vector2 pos = imageStartPos;
            print("Before: " + miniGameImage.position.x);
            pos.x += mapped;
            miniGameImage.position = pos;
            print("After: " + miniGameImage.position.x);
        }



        // Sin Cos Osciallation

        // float cycles = Time.time / 2;
        // const float tau = Mathf.PI * 2;
        // float rawSineWave = Mathf.Sin(cycles * tau);

        // float movementFactor = rawSineWave / 2f + 0.5f;
        // Vector3 movementVector = new Vector3(0,0,0);

        // Vector3 offset = movementFactor * movementVector;
        // miniGameImage.position = offset;


        CheckActivateGame();
    }

    public static float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    float oscillate(float time, float speed, float scale)
    {
        return Mathf.Cos(time * speed / Mathf.PI) * scale;
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
