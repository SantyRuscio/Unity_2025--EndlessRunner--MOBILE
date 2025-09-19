using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public Animator animator;

    public void JumpAnim()
    {
        animator.SetTrigger("JumpTrigger");
    }

    public void RollAnim()
    {
        animator.SetTrigger("RollTrigger");
    }

    public void CollisionerAnim()
    {
       animator.SetTrigger("CollisionTrigger");
    }

}
