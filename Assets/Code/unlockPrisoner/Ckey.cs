using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ckey : MonoBehaviour
{
    public string code;
    void Start()
    {
        transform.name = code;
        print(transform.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
