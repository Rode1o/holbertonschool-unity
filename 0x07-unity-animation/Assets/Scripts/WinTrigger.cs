using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject winCanvas;

    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerController>().enabled = false;
        Camera.main.GetComponent<CameraController>().enabled = false;
        winCanvas.SetActive(true);
        //timerText.color = Color.green;
        //timerText.fontSize = 60;
    }
}
