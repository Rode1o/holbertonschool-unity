using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public Text finalTime;
    public GameObject winCanvas;

    private float value;
    private float minutes;
    private float seconds;
    private float milliseconds;

    // Start is called before the first frame update
    private void Start()
    {
        value = 0f;
        minutes = 0f;
        seconds = 0f;
        milliseconds = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        value += Time.deltaTime;

        minutes = Mathf.FloorToInt(value / 60);
        seconds = Mathf.FloorToInt(value % 60);
        milliseconds = (value % 1) * 100;

        timerText.text = string.Format("{0:0}:{1:00}.{2:00}", minutes, seconds, milliseconds);

        if (winCanvas.activeInHierarchy)
        {
            Win();
        }
    }

    //
    public void Win()
    {
        finalTime.text = timerText.text;
        timerText.enabled = false;
        this.enabled = false;
    }
}
