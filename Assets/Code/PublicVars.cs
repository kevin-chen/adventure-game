using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class PublicVars
{

    //==========general settings===========
    public static Vector3 checkPoint;
    public static int keyNum = 0;
    public static int bulletsNum = 0;
    public static float health;
    public static float stamina;
    public static bool shootable = false;
    
    //========mini game stats============
    public static bool isMiniGameActivated = false;
    public static bool isSliderMiniGamePassed = false;
    public static bool isPasscodeMiniGamePassed = false;
    public static int[] passcode = new int[4]; 


    //============security==============
    public static bool isDetected = false;
    public static float chase_limit = 7;

    public static float chase_duration = chase_limit + 1;
    public static float origin_chaseDuration = 0;

    //============key status=============

    public static bool hasFirstKey = false;

    public static bool hasSecondKey = false;

    public static bool hasThirdKey = false; 
    //============area 1==================
    public static int[] prisoners = {-1, -1,- 1};

    

}
