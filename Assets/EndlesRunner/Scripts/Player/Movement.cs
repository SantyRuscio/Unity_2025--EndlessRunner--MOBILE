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
        // Movimiento lateral solo en X (ignorar Z del input)
        Vector3 moveVector = new Vector3(input.x * _speed, playerBody.velocity.y, 0f);

        // Aplicar velocidad al Rigidbody
        playerBody.velocity = moveVector;
    }



    public void Jump()
    {
        playerBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}
