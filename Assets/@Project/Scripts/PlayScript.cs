using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour
{

    bool isPaused = false;

    void Start()
    {
        isPaused = false;
    }   

    public void PauseGame()
    {   
        isPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }
    
   

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
  

    public void ExitMenu()
    {
        Application.Quit();
    }
}