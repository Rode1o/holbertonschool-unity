using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    private int nextScene;
    private int totalScenes;

    // Start is called before the first frame update
    void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        totalScenes = SceneManager.sceneCountInBuildSettings - 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Next()
    {
        if (nextScene > totalScenes)
        {
            MainMenu();
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
