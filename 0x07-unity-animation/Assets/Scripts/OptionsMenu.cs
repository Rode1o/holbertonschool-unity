using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle checkbox;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Inverted"))
        {
            checkbox.isOn = (PlayerPrefs.GetInt("Inverted") == 1 ? true : false);
        }
    }

    // Goes to the previous scene
    public void Back()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Previous"));
    }

    //
    public void Apply()
    {
        PlayerPrefs.SetInt("Inverted", checkbox.isOn ? 1 : 0);
        Back();
    }
}
