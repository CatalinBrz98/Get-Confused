using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private float speed = 12f, gravity = 9.81f, groundDistance = 0.4f, jumpForce = 0.05f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = 0f;

        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * xMovement + transform.forward * zMovement;
        controller.Move(movement * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpForce * gravity / 50000f);

        velocity.y -= gravity * Time.deltaTime * Time.deltaTime;
        controller.Move(velocity);
    }
}
