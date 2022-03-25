using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour
{
    public int oscillateSpeed = 10;
    void Start()
    {
        StartCoroutine(Oscillate());
    }

    float oscillate(float time, float speed, float scale)
    {
        return Mathf.Cos(time * speed / Mathf.PI) * scale;
    }
    IEnumerator Oscillate(){
        

        while(true){
            //int maxOscillateVal = 10;
            //float pingpong = Mathf.PingPong(Time.unscaledTime, maxOscillateVal);
            float pingpong = (Mathf.Sin (Time.time * oscillateSpeed * Mathf.PI * 2.0f) + 1.0f) / 2.0f;

            //float mapped = oscillate(Time.unscaledTime, oscillateSpeed, 200);
            transform.eulerAngles = Vector3.Lerp (new Vector3(0, -1, 0), new Vector3(0, 1, 0), pingpong);

        }
    }
}
