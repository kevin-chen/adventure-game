using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    // Start is called before the first frame update
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
        foreach(int i in PublicVars.passcode){
            PublicVars.passcode[i] = Random.Range(0, 9);
        }

        //prisioner
        List<int> prisoners = new List<int>{0, 1, 2, 3, 4, 5, 6, 7};
        foreach(int i in PublicVars.prisoners){
            int temp = Random.Range(0, 7);
            while(FindNum(PublicVars.prisoners, temp)){
                temp = Random.Range(0, 7);
            }
            prisoners.Remove(temp);
            PublicVars.prisoners[i] = temp;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
