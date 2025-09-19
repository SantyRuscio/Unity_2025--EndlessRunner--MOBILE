using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Rendering;

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
    [SerializeField] private float OverlapRadio = 10f;


    //mobile
    [SerializeField] private float deadZone = 0.1f;

    private Movement movimiento; //composición

    private void Awake()
    {
        movimiento = new Movement(PlayerBody, transform, Speed, ForwardSpeed, JumpForce, acceleration, maxForwardSpeed); //Composicion
        inputManager.OnJump += Jump;
        inputManager.OnMove += Move;
        inputManager.OnRoll += Roll;
        inputManager.OnTouchAndClick += CheackWall;
    }
    private void Start()
    {
        originalHeight = playerCollider.height;
        originalCenter = playerCollider.center;
    }

    #region Collisioner Detecter y Trigger Event Manager
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            OnCollsionDead();
            StartCoroutine(DeadTimeLapse()); 
        }
    }

    private void OnCollsionDead()
    {
        view.Collisioner();
    }

    private IEnumerator DeadTimeLapse()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("2 segundos después");
        EventManager.Trigger(TypeEcvents.GameOver);
    }
    #endregion

    private void CheackWall()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, OverlapRadio);

        foreach (Collider hit in hits)
        {
            var ParedTouch = hit.GetComponent<ParedTouch>();

            if (ParedTouch != null)
            {
                ParedTouch.Execute();
            }
        }
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
#if UNITY_ANDROID || UNITY_IOS
        // Aplicar deadZone
        if (Mathf.Abs(dirHorizontal) < deadZone)
            dirHorizontal = 0f;
#endif

        // Construir input y mandar a Movement
        playerMovementInput = new Vector3(dirHorizontal, 0f, 0f);
        movimiento.Move(playerMovementInput);
    }

    void Roll()
    {
        if ( ! CheckIsGrounded() )  return;
        playerCollider.height = originalHeight * 0.7f;
        playerCollider.center = originalCenter * 0.9f;
        view.Roll();
    }

    public void OnRollEnd()
    {
        playerCollider.height = originalHeight;
        playerCollider.center = originalCenter;
    }
}