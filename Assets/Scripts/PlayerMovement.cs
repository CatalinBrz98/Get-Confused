using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 12f, gravity = 19.62f;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
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
        velocity.y -= gravity * Time.deltaTime * Time.fixedDeltaTime;
        controller.Move(velocity);
    }
}
