using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    private Quaternion currentRotation;
    private float sensitivity = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + offset;
        
        //Free camera movement
        //currentRotation.x += Input.GetAxis("Mouse X") * sensitivity; 
        //currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity; 
        //currentRotation.x = Mathf.Repeat(currentRotation.x, 360); 
        //currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle); 
        //Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y,currentRotation.x,0);

        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensitivity, Vector3.up) * offset;
        transform.LookAt(player.transform.position);
    }
}
