using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    //=========prefab===========
    public GameObject randomIcon;

    public GameObject prisoner1;
    public GameObject prisoner2;
    public GameObject prisoner3;

    //==========location=========
    public Vector3 code1;
    public Vector3 code2;
    public Vector3 code3;
    public Vector3 code4;

    public Transform priPos;
    
    bool FindNum(int[] arr, int target){
        foreach(int i in arr){
            if(i == target){
                return true;
            }
        }
        return false;
    }
    void Start()
    {
        //minigame passcode
        for(int i = 0; i < 4; i++){
            PublicVars.passcode[i] = Random.Range(0, 10);
            print(PublicVars.passcode[i]);
        }

        //prisioner
        List<int> prisoners = new List<int>{0, 1, 2, 3, 4, 5, 6, 7};
        for(int i = 0; i < 3; i++){
            int temp = Random.Range(0, 8);
            while(FindNum(PublicVars.prisoners, temp)){
                temp = Random.Range(0, 8);
            }
            print("temp: " + temp + "i: " + i);
            prisoners.Remove(temp);
            PublicVars.prisoners[i] = temp;
        }
        
        //instantiate prisioner
        int check = 0;
        for(int i = 0; i < 8 ; i++){
            int a = Random.Range(0, 3);
            //GameObject newPrisoner;
            //GameObject icon;
            Vector3 singlePos = priPos.Find("pri" + i).transform.position;
            Vector3 iconPos = singlePos + new Vector3(0, .001f, 0);
            switch(a){
                case 0:
                    Instantiate(prisoner1, singlePos, transform.rotation);
                    break;
                case 1:
                    Instantiate(prisoner2, singlePos, transform.rotation);
                    break;
                case 2:
                    Instantiate(prisoner3, singlePos, transform.rotation);
                    break;
            }
            if(check != 3){
                if (i == PublicVars.prisoners[0]){
                    Instantiate(randomIcon.transform.Find("diamond"), iconPos, randomIcon.transform.Find("diamond").rotation);
                    print("diamond");
                    check++;
                } else if (i == PublicVars.prisoners[1]){
                    Instantiate(randomIcon.transform.Find("heart"), iconPos, randomIcon.transform.Find("diamond").rotation);
                    print("heart");
                    check++;
                } else if (i == PublicVars.prisoners[2]){
                    Instantiate(randomIcon.transform.Find("club"), iconPos, randomIcon.transform.Find("diamond").rotation);
                    print("club");
                    check++;
                }

            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
