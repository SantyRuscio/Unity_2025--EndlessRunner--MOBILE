using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    private Rigidbody playerBody;
    private Transform playerTransform;
    private float speed;
    private float forwardSpeed;
    private float jumpForce;
    public Movement(Rigidbody rb, Transform transform, float speed, float forwardSpeed, float jumpForce)
    {
        playerBody = rb;
        playerTransform = transform;
        this.speed = speed;
        this.forwardSpeed = forwardSpeed;
        this.jumpForce = jumpForce;
    }

    public void Mover(Vector3 input, bool isGrounded)
    {
        // Movimiento lateral (X, Z por input)
        Vector3 moveVector = new Vector3(input.x, 0f, input.z) * speed;

        // Movimiento hacia adelante constante 
        moveVector += playerTransform.forward * forwardSpeed;

        // Aplicar velocidad
        playerBody.velocity = new Vector3(moveVector.x, playerBody.velocity.y, moveVector.z);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
