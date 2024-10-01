using AbstractClass;
using Manager;
using RepeatUtils.DesignPattern.SingletonPattern;
using System;
using UnityEngine;

public class PlayerAnimation : AbsAnimator
{
    [SerializeField] private Animator animator;

    private Rigidbody2D rigidbody2D;
    private PlayerMovement playerMovement;


    private enum PlayerAnimationParameter
    {
        PlayerSpeed,
        IsDash,
        JumpVectorVertical,
        IsJump,
        AttackComboNumber,
        IsAttack,
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RunAnimationHandler();
        DashAnimationHandler();
        JumpAnimationHandler();
        AttackAnimationHandler();
    }

    private void RunAnimationHandler()  
    {
        if (playerMovement.IsGround())
        {
            animator.SetFloat(PlayerAnimationParameter.PlayerSpeed.ToString(), Math.Abs(InputManager.Instance.GetRawInputNormalized().x));
        }
    }
    private void DashAnimationHandler()
    {
        animator.SetBool(PlayerAnimationParameter.IsDash.ToString(), playerMovement.isDashing);
    }
    private void JumpAnimationHandler()
    {
        animator.SetBool(PlayerAnimationParameter.IsJump.ToString(), !playerMovement.IsGround());
        animator.SetFloat(PlayerAnimationParameter.JumpVectorVertical.ToString(),rigidbody2D.velocity.y);

    }
    private void AttackAnimationHandler()
    {
        if (!playerMovement.isAttackCombo && InputManager.Instance.IsAttackPressed())
        {
            animator.SetTrigger(PlayerAnimationParameter.IsAttack.ToString());
            animator.SetFloat(PlayerAnimationParameter.AttackComboNumber.ToString(),playerMovement.combo);
        }
    }

}
