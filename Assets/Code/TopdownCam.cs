using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownCam : MonoBehaviour
{
    public Transform target;
    public float height = 30f;
    public float distance = 20f;
    private float heightMax = 50f;
    private float heightMin = 20f;
    public float angle = 30f;
    //private float angleLimit = 50f;
    private float adjustMultiplier = 25;
    public float smoothSpd = 0.5f;
    private Vector3 refVelocity;
    void Start()
    {
        SetCam();
    }

    void Update()
    {
        SetCam();
        updateCam();
    }

    void SetCam(){
        if(!target) return;

        Vector3 worldPos = (Vector3.forward * -distance) + (Vector3.up * height);

        Vector3 rotateVec = Quaternion.AngleAxis(angle, Vector3.up) * worldPos;

        Vector3 horizonPos = target.position;
        horizonPos.y = 0f;
        Vector3 finalPos = horizonPos + rotateVec;


        transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref refVelocity, smoothSpd);
        transform.LookAt(horizonPos);
    }


    void updateCam(){

        float difference = Time.deltaTime * adjustMultiplier;
        if(Input.GetKey("1")) angle -= difference;
        if(Input.GetKey("3")) angle += difference;
        if(Input.GetAxis("Mouse ScrollWheel") > 0f && Mathf.Max(height, heightMax) == heightMax){
            height += difference * 3;
            distance += (difference * 3f * 0.7f);
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f && Mathf.Min(height, heightMin) == heightMin){
            height -= difference * 3;
            distance -= (difference * 3f * 0.7f);
        }
    }
}
