using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Loads the corresponding level scene
    public void LevelSelect(int level)
    {
        SceneManager.LoadScene(level);
    }

    // Loads the options scene
    public void Options()
    {
        PlayerPrefs.SetInt("Previous", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Options");
    }

    // Exits the game and prints exited on the console
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exited");
    } 
}
