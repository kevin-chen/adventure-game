using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour
{
    public float oscillateSpeed = 1;

    public Vector3 from = new Vector3(0f, -60f, 0);
    public Vector3 to = new Vector3(0f, 60f, 0);
    void Start()
    {

    }

    private void Update() {
        float pingpong = (Mathf.Sin(Time.time * oscillateSpeed * Mathf.PI * 2.0f) + 1.0f) / 2.0f;

        //float mapped = oscillate(Time.unscaledTime, oscillateSpeed, 200);
        transform.eulerAngles = Vector3.Lerp(from, to, pingpong);
    }

}
