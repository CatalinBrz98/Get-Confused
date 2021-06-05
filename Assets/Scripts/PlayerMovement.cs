using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f, gravity = 19.62f, jumpForce = 125f;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isJumping = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
            isJumping = true;
    }

    void FixedUpdate()
    {
        isGrounded = (controller.collisionFlags & CollisionFlags.Below) != 0;
        if (isGrounded && velocity.y < 0)
            velocity.y = 0f;

        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * xMovement + transform.forward * zMovement;
        controller.Move(movement * speed * Time.fixedDeltaTime);

        if (isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpForce * gravity / 50000f);
            isJumping = false;
        }

        velocity.y -= gravity * Time.deltaTime * Time.fixedDeltaTime;
        controller.Move(velocity);
    }
}
