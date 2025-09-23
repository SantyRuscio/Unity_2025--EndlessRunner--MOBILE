using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View
{
    private Animator _animator;

    public View SetAnimator(Animator animator)
    {
        _animator = animator;
        return this;    
    }
    public void Jump()
    {
        _animator.SetTrigger("JumpTrigger");
    }

    public void Roll()
    {
        _animator.SetTrigger("RollTrigger");
    }

    public void Collisioner()
    {
       _animator.SetTrigger("CollisionTrigger");
    }

}
