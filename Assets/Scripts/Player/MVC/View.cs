using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public Animator animator;

    public void Jump()
    {
        animator.SetTrigger("JumpTrigger");
    }

    public void Roll()
    {
        animator.SetTrigger("RollTrigger");
    }

    public void Collisioner()
    {
       animator.SetTrigger("CollisionTrigger");
    }

}
