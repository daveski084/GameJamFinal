using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ButtonManager : MonoBehaviour
{


    public void OnQuittoMainButtonPressed()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void OnGoBackToMainPause()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu"); 
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit(); 
    }

    public void OnStartButtonPressed()
    {
        SceneManager.LoadScene("Game"); 
    }

    public void OnInstructionsButtonPressed()
    {
        SceneManager.LoadScene("Instructions"); 
    }

    public void OnCreditsButtonPressed()
    {
        SceneManager.LoadScene("Credits"); 
    }
    
}
