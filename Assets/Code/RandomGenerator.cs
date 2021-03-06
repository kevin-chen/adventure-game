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

    public GameObject numPrefab;
    public GameObject door;
    public GameObject key;

    public GameObject guard;


    //==========location=========
    public Vector3 code1;
    public Vector3 code2;
    public Vector3 code3;
    public Vector3 code4;

    public Transform priPos;

    public Transform numPos;
    public Transform kdPos;

    public Transform guardPos;

    //========cooldown=========
    float timer = 3;
    float originTimer = 3;
    
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
            PublicVars.passcode[i] = Random.Range(1, 4);
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
            Vector3 doorPos = kdPos.Find("d" + i).transform.position;
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
                    setSpecial("diamond", i, iconPos, doorPos, check);
                    check++;
                } else if (i == PublicVars.prisoners[1]){
                    setSpecial("heart", i, iconPos, doorPos, check);
                    check++;
                } else if (i == PublicVars.prisoners[2]){
                    setSpecial("club", i, iconPos, doorPos, check);
                    check++;
                }
            }
        }
        // instantiate passcode
        for (int i = 0; i < 3; i++){
            int num = PublicVars.passcode[i];
            print("num " + i + "=" + num);
            Vector3 singlePos = numPos.Find("" + i).transform.position + new Vector3(0, 0.001f, 0);
            Vector3 diff = new Vector3(1, 0, 0);
            Instantiate(numPrefab.transform.Find("rom" + (i + 1)), singlePos - diff, Quaternion.Euler(90,0,0));
            Instantiate(numPrefab.transform.Find("" + num), singlePos + diff, Quaternion.Euler(90,0,0));
        }
        Vector3 lastPos = numPos.Find("3").transform.position;
        Vector3 ldiff = new Vector3(1, 0, 0);
        Instantiate(numPrefab.transform.Find("rom4"), lastPos + ldiff, Quaternion.Euler(0,180,0));
        Instantiate(numPrefab.transform.Find("" + PublicVars.passcode[3]), lastPos - ldiff, Quaternion.Euler(0,180,0));
    }

    //set key and door for special three
    void setSpecial(string symbol, int i, Vector3 iconPos, Vector3 doorPos, int check){
        Transform symbolObj = randomIcon.transform.Find(symbol);
        Vector3 keyPos = kdPos.Find("k" + check).transform.position;
        // set door name/code
        kdPos.Find("d" + i).name = symbol;
        // set door symbol sprite
        Instantiate(symbolObj, iconPos, symbolObj.rotation);
        // set key
        GameObject newKey = Instantiate(key, keyPos, transform.rotation);
        // set key symbol sprite
        Instantiate(symbolObj, keyPos + new Vector3(0, .001f, 0), symbolObj.rotation);
        // set key name/code
        newKey.name = symbol;
        print(symbol);
    }




    //area

    void Update()
    {
        
    }

    private void FixedUpdate() {
        if(PublicVars.isPickedUp && GameObject.Find("Guards").transform.childCount <= PublicVars.guardLimit){
            if(timer >= 0){
                timer -= Time.deltaTime;
            }else{
                Instantiate(guard, guardPos.Find("g" + Random.Range(1, 3)).transform.position, transform.rotation);
                timer = originTimer;
            }
        }
    }
}
