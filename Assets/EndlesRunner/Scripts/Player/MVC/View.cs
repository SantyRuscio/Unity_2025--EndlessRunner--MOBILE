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

    public void RewindAnim()
    {
        _animator.ResetTrigger("JumpTrigger");
        _animator.ResetTrigger("RollTrigger");
        _animator.ResetTrigger("CollisionTrigger");

        _animator.Play("Run");
    }
}
