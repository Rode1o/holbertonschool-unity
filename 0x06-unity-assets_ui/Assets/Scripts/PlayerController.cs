using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 move;
    private bool groundedPlayer;
    private float playerSpeed = 5.0f;
    private float jumpHeight = 5.0f;
    private float gravityValue = -9.81f;

    private void Start()
    {

    }

    void Update()
    {
        Movement();
        Rotate();
        FallOff();
    }

    void Movement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move = transform.rotation * move;
        
            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump"))
            {
                move.y = jumpHeight;
            }
        }

        move.y += gravityValue * Time.deltaTime;
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 4f, 0));
    }

    void FallOff()
    {
        if (transform.position.y <= -25)
        {
            transform.position = new Vector3(0, 75, 0);
        }
    }
}
