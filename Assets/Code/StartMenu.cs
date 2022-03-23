using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void StartGame()
    {
        // PublicVars.ResetLife();
        // PublicVars.Reset();
        SceneManager.LoadScene("SampleSceneLevel1");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
