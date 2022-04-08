using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour
{
    public float oscillateSpeed = 1;
    private Vector3 originAngle;
    public Vector3 from;
    public Vector3 to;

    public float Angle = 60f;
    void Start()
    {
        originAngle = transform.rotation.eulerAngles;
        from = originAngle - new Vector3(0f, Angle, 0);
        to = originAngle + new Vector3(0f, Angle, 0);

    }

    private void Update() {
        float pingpong = (Mathf.Sin(Time.time * oscillateSpeed * Mathf.PI * 2.0f) + 1.0f) / 2.0f;

        transform.eulerAngles = Vector3.Lerp(from, to, pingpong);
    }

}
