using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;
    private Vector3 move;
    private bool groundedPlayer;
    private float playerSpeed = 5.0f;
    private float jumpHeight = 5.0f;
    private float gravityValue = -9.81f;
    private float sensitivity = 150f;

    private void Start()
    {

    }

    void Update()
    {
        Movement();
        FallOff();
    }

    void Movement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            if (Input.GetKey("w") | Input.GetKey("a") | Input.GetKey("s") | Input.GetKey("d"))
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move = transform.rotation * move;
        
            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetTrigger("Jump");
                //anim.ResetTrigger("Jump");
                move.y = jumpHeight;
            }
        }

        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0);
        move.y += gravityValue * Time.deltaTime;
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    void FallOff()
    {
        if (transform.position.y <= -25)
        {
            transform.position = new Vector3(0, 75, 0);
        }
    }
}
