using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    private Vector3 playerMovementInput;
    private bool isGrounded;
    [SerializeField] private LayerMask FloorMask;


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
    }

    private void Update()
    {
        // Input 
        playerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

        // Chequear suelo
        isGrounded = Physics.CheckSphere(Feets.position, GroundCheckDistance, FloorMask);

        // Pasar input al objeto Movimiento
        movimiento.Mover(playerMovementInput);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            movimiento.Jump();
        }
    }

}

