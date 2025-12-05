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
        if (_animator == null) return;
        _animator.SetTrigger("JumpTrigger");
    }


    public void Roll()
    {
        if (_animator == null) return;
        _animator.SetTrigger("RollTrigger");
    }

    public void Collisioner()
    {
        if (_animator == null) return;
        _animator.SetTrigger("CollisionTrigger");
    }

    public void RewindAnim()
    {
        if (_animator == null) return;

        _animator.ResetTrigger("JumpTrigger");
        _animator.ResetTrigger("RollTrigger");
        _animator.ResetTrigger("CollisionTrigger");

        _animator.Play("Run");
    }

}
