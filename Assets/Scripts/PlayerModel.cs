using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    private Vector3 playerMovementInput;
    [SerializeField] private LayerMask FloorMask;

    [SerializeField] Controller inputManager;
    [SerializeField] View view;

    [SerializeField] CapsuleCollider playerCollider;
    private float originalHeight;
    private Vector3 originalCenter;


    [SerializeField] private Transform Feets;
    [SerializeField] private Rigidbody PlayerBody;

    [SerializeField] private float Speed = 5f;
    [SerializeField] private float acceleration = 0.5f;
    [SerializeField] private float maxForwardSpeed = 20f;
    [SerializeField] private float ForwardSpeed = 10f; 
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private float GroundCheckDistance = 0.2f;

    
    private Movement movimiento; // 👈 composición

    private void Awake()
    {
        movimiento = new Movement(PlayerBody, transform, Speed, ForwardSpeed, JumpForce, acceleration, maxForwardSpeed); //Composicion
        inputManager.OnJump += Jump;
        inputManager.OnMove += Move;
        inputManager.OnRoll += Roll;
    }
    private void Start()
    {
        originalHeight = playerCollider.height;
        originalCenter = playerCollider.center;
    }

    bool CheckIsGrounded()
    {
        // Chequear suelo
        return Physics.CheckSphere(Feets.position, GroundCheckDistance, FloorMask);
    }

    void Jump()
    {
        if ( ! CheckIsGrounded() ) return;
        movimiento.Jump();
        view.Jump();
    }

    void Move(float dirHorizontal)
    {
        playerMovementInput = new Vector3(dirHorizontal, 0f, 0f);
        // Pasar input al objeto Movimiento
        movimiento.Move(playerMovementInput);
    }

    void Roll()
    {
        if ( ! CheckIsGrounded() )  return;
        playerCollider.height = originalHeight * 0.9f;
        playerCollider.center = originalCenter * 0.9f;
        view.Roll();
    }

    public void OnRollEnd()
    {
        playerCollider.height = originalHeight;
        playerCollider.center = originalCenter;
    }

}

