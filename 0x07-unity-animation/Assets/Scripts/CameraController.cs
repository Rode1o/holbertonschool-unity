using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float Y_MIN = 0.0f;
    private const float Y_MAX = 50.0f;

    public Transform player;
    private Transform camTransform;
    public bool isInverted = false;
    private Camera cam;

    private float distance = 6.3f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 150f;
    private float sensivityY = 100f;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
        isInverted = (PlayerPrefs.GetInt("Inverted") == 1 ? true : false);
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X") * sensivityX * Time.deltaTime;
        currentY += Input.GetAxis("Mouse Y") * (isInverted ? 1 : -1) * sensivityY * Time.deltaTime;
        currentY = Mathf.Clamp(currentY, Y_MIN, Y_MAX);
    }
    
    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = player.position + rotation * dir;
        camTransform.LookAt(player.position);
    }
}