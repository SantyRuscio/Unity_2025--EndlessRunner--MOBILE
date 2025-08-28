using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPllayer : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float XRot;

    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private Transform Feets;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;

    [SerializeField] private float Speed = 5f;
    [SerializeField] private float Sensitivity = 2f;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private float GroundCheckDistance = 0.2f;

    private bool isGrounded;

    private void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // 👇 Chequear si está en el suelo
        isGrounded = Physics.CheckSphere(Feets.position, GroundCheckDistance, FloorMask);

        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    private void MovePlayerCamera()
    {
        XRot -= PlayerMouseInput.y * Sensitivity;
        XRot = Mathf.Clamp(XRot, -90f, 90f); 

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(XRot, 0f, 0f);
    }
}


