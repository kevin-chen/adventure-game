using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour
{
    public float oscillateSpeed = 1;
    private Vector3 originAngle;
    private Vector3 from;
    private Vector3 to;
    void Start()
    {
        originAngle = transform.rotation.eulerAngles;
        from = originAngle - new Vector3(0f, 60f, 0);
        to = originAngle + new Vector3(0f, 60f, 0);

    }

    private void Update() {
        float pingpong = (Mathf.Sin(Time.time * oscillateSpeed * Mathf.PI * 2.0f) + 1.0f) / 2.0f;

        transform.eulerAngles = Vector3.Lerp(from, to, pingpong);
    }

}
