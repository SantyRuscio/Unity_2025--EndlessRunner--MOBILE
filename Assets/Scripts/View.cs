using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public Animator animator;
    public Transform feet;
    public LayerMask groundMask;
    public float checkRadius = 0.2f;

    [SerializeField] CapsuleCollider playerCollider;
    private float originalHeight;
    private Vector3 originalCenter; 

    private bool isGrounded;
    private bool isRolling;

    void Start()
    {
        originalHeight = playerCollider.height;
        originalCenter = playerCollider.center;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(feet.position, checkRadius, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("JumpTrigger");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded && !isRolling)
        {
            animator.SetTrigger("RollTrigger");
            isRolling = true;

            playerCollider.height = originalHeight * 0.5f;       // ACHICAR COLIDER 
            playerCollider.center = originalCenter * 0.5f;       // EN UN FUTURO EN OTRO CODIGO
        }
        if (isRolling)
        {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
            if (!state.IsName("Roll")) 
            {
                isRolling = false;
                playerCollider.height = originalHeight;
                playerCollider.center = originalCenter;
            }
        }
    }
    public void EndRoll()
    {
        isRolling = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (feet != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(feet.position, checkRadius);
        }
    }
}
