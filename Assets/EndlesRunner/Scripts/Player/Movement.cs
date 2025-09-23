using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    private Rigidbody playerBody;
    private Transform playerTransform;
    private float _speed;
    private float _forwardSpeed;
    private float _jumpForce;

    private float _maxForwardSpeed;
    private float _acceleration;

    public Movement() {}

    #region Builder
    public Movement SetPlayerBody(Rigidbody rb)
    {
        playerBody = rb;
        return this;
    }
    public Movement SetPlayerTransfomr(Transform transform)
    {
        playerTransform = transform;
        return this;
    }

    public Movement SetPlayerSpeed(float speed)
    {
        _speed = speed;
        return this;
    }
    public Movement SetPlayerForwardSpeed(float forwardSpeed)
    {
        _forwardSpeed = forwardSpeed;
        return this;
    }

    public Movement SetPlayerJumpForce(float jumpForce)
    {
        _jumpForce = jumpForce;
        return this;
    }

    public Movement SetPlayerAcceleration(float acceleration)
    {
        _acceleration = acceleration;
        return this;
    }

    public Movement SetPlayerMaxForwardSpeed(float maxForwardSpeed)
    {
        _maxForwardSpeed = maxForwardSpeed;
        return this;
    }
    #endregion

    public void Move(Vector3 input)
    {
        // Incrementar velocidad hacia adelante con el tiempo
        _forwardSpeed = Mathf.Min(_forwardSpeed + _acceleration * Time.deltaTime, _maxForwardSpeed);

        // Movimiento lateral (X, Z por input)
        Vector3 moveVector = new Vector3(input.x, 0f, input.z) * _speed;

        // Movimiento hacia adelante (con velocidad ya acelerada)
        moveVector += playerTransform.forward * _forwardSpeed;

        // Aplicar velocidad
        playerBody.velocity = new Vector3(moveVector.x, playerBody.velocity.y, moveVector.z);
    }

    public void Jump()
    {
        playerBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}
