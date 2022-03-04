using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject player;
    public GameObject timerCanvas;

    public void CutsceneCameraEvent()
    {
        mainCamera.SetActive(true);
        timerCanvas.SetActive(true);
        player.GetComponent<PlayerController>().enabled = true;
        this.gameObject.SetActive(false);
    }
}
