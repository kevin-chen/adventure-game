using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class PublicVars
{

    //==========general settings===========
    public static Vector3 checkPoint;
    public static int keyNum = 0;
    public static int bulletsNum = 0;
    public static float health = 1000; //money
    public static float stamina;
    public static bool shootable = true;

    
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
    //public static bool[] pRelease = {false, false, false};
    public static string[] names = {"Kermit Weaver",//40 random names
        "Lindsay Moore",
        "Ronda Wells",
        "Chester Gomez",
        "Noelle Oneal",
        "Arthur Buck",
        "Sherry Massey",
        "Estela Randall",
        "Williams Mcbride",
        "Janine Middleton",
        "Nickolas Avery",
        "Millie Weber",
        "Alphonso Li",
        "Connie Tyler",
        "Winnie Sloan",
        "Buddy Duncan",
        "Adeline Robertson",
        "Belinda Contreras",
        "Jessie Carson",
        "Lana Knight",
        "Blair Combs",
        "Jefferson Thomas",
        "Bethany Cole",
        "Marcie Hernandez",
        "Nathanial Lowe",
        "Trenton Jensen",
        "Aimee Dyer",
        "Rachel Ferguson",
        "Rocky Bond",
        "Kris Salas",
        "Tyrell Owens",
        "Myrna Leblanc",
        "Fannie Bush",
        "Lupe Carrillo",
        "Ellis Salazar",
        "Jordon Mckee",
        "Millard Robbins",
        "Kathryn Acosta",
        "Cesar Huff",
        "Tommy Lutz"};

    
    //============area2===================
    public static bool isPickedUp = false;
}
