using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Loading");
    }
    public void OptionsMenu()
    {
        
    }
    public void Exit()
    {
        Application.Quit();
    }
}
