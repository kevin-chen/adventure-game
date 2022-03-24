using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PasscodePressButton : MonoBehaviour
{
    public static event Action<int> ButtonPress = delegate { };
    public int buttonNum;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(PressButton);
    }

    void PressButton() {
        // print("hello press button");
        ButtonPress(buttonNum);
    }

}
