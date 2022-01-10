using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu: MonoBehaviour
{
    // Start is called before the first frame update

    public Material trapMat;
    public Material goalMat;
    public Toggle colorblindMode;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMaze()
    {
        trapMat.color = Color.red;
        goalMat.color = Color.green;
        if (colorblindMode.isOn)
        {
            trapMat.color = new Color32(255, 112, 0, 1);
            goalMat.color = Color.blue;
        }
        
        SceneManager.LoadScene("maze");
    }

    public void QuitMaze()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
