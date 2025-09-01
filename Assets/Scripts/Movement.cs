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

    private float maxForwardSpeed;
    private float acceleration;

    public Movement(Rigidbody rb, Transform transform, float speed, float forwardSpeed, float jumpForce, float acceleration, float maxForwardSpeed)
    {
        playerBody = rb;
        playerTransform = transform;
        this.speed = speed;
        this.forwardSpeed = forwardSpeed;
        this.jumpForce = jumpForce;

        this.acceleration = acceleration;
        this.maxForwardSpeed = maxForwardSpeed;
    }

    public void Mover(Vector3 input)
    {
        // Incrementar velocidad hacia adelante con el tiempo
        forwardSpeed = Mathf.Min(forwardSpeed + acceleration * Time.deltaTime, maxForwardSpeed);

        // Movimiento lateral (X, Z por input)
        Vector3 moveVector = new Vector3(input.x, 0f, input.z) * speed;

        // Movimiento hacia adelante (con velocidad ya acelerada)
        moveVector += playerTransform.forward * forwardSpeed;

        // Aplicar velocidad
        playerBody.velocity = new Vector3(moveVector.x, playerBody.velocity.y, moveVector.z);
    }

    public void Jump()
    {
        playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
