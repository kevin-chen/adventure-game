using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void Replay() {
        SceneManager.LoadScene("jail");
        PublicVars.health = 1000;
    }
}