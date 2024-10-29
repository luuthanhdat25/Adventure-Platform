using AbstractClass;
using Manager;
using ScriptableObjects;
using System;
using UI;
using UnityEngine;

public class PlayerController : AbsController
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private PlayerCombo playerCombo;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float checkGroundYOffSet;

    [SerializeField]
    private float groundCheckRadius = 0.2f;

    [SerializeField]
    private SkillTreeUI skillTreeUI;

    private PlayerMovement playerMovement;
    private bool isMovingRight = true;
    private bool isOpenTabUpgrade = false;
    private int jumpCount = 0;

    private void Start()
    {
        playerMovement = absMovement as PlayerMovement;
    }

    private void FixedUpdate()
    {
        UpdateAnimatorParameters();

        if (playerMovement.IsDashing) return;

        Vector2 inputVector = InputManager.Instance.GetRawInputNormalized();

        if (CanMove())
            playerMovement.Move(inputVector, speed);

        if (CanFlip(inputVector.x))
            FlipCharacter();

        if (InputManager.Instance.IsDashInputTrigger() && CanDash())
            playerMovement.Dash();

        if (InputManager.Instance.IsAttackPressed() && CanAttack())
            playerCombo.HandleCombo();

        if (IsGround())
            HandleGroundState(inputVector.x);

        if (InputManager.Instance.IsJumpInputTrigger() && CanJump())
            PerformJump(inputVector.y);

        if (InputManager.Instance.IsPerformSkillPressed() && CanPerformSkill())
            playerCombo.SkillHandle();

        if (InputManager.Instance.IsTabIsOpenedPressed())
            ToggleTabUpgrade();
    }

    private void UpdateAnimatorParameters()
    {
        absAnimator.SetBool(PlayerAnimationParameter.IsDash.ToString(), playerMovement.IsDashing);
        absAnimator.SetFloat(PlayerAnimationParameter.JumpVectorVertical.ToString(), playerMovement.GetVelocity().y);
        absAnimator.SetBool(PlayerAnimationParameter.IsJump.ToString(), !IsGround());
    }

    private bool CanMove()
    {
        return !isOpenTabUpgrade
            && !playerCombo.isAttaking
            && !playerCombo.isPerformingSkill;
    }

    private bool CanFlip(float inputX)
    {
        return (inputX > 0 && !isMovingRight || inputX < 0 && isMovingRight) && CanMove();
    }

    private bool CanDash()
    {
        return !isOpenTabUpgrade
            && !PlayerSingleton.Instance.IsOutOfStamina();
    }

    private bool CanAttack()
    {
        return !isOpenTabUpgrade && IsGround()
            && !playerCombo.isPerformingSkill
            && !PlayerSingleton.Instance.IsOutOfStamina();
    }

    private bool CanJump()
    {
        return jumpCount < 2 && !playerCombo.isAttaking
            && !playerCombo.isPerformingSkill;
    }

    private bool CanPerformSkill()
    {
        return !isOpenTabUpgrade && IsGround()
            && !playerCombo.isAttaking
            && !PlayerSingleton.Instance.IsOutOfStamina();
    }

    private void FlipCharacter()
    {
        isMovingRight = !isMovingRight;
        absMovement.Flip();
    }

    private void PerformJump(float inputY)
    {
        jumpCount++;
        playerMovement.JumpHandler(inputY);
    }

    private void HandleGroundState(float inputX)
    {
        jumpCount = 0;
        absAnimator.SetFloat(PlayerAnimationParameter.PlayerSpeed.ToString(), Math.Abs(inputX));
    }

    private void ToggleTabUpgrade()
    {
        if (!isOpenTabUpgrade)
        {
            skillTreeUI.OpenTabUpgrade();
        }
        else
        {
            skillTreeUI.CloseTabUpgrade();
        }
        isOpenTabUpgrade = !isOpenTabUpgrade;
    }

    public bool IsGround()
    {
        Vector3 groundCheckPos = new Vector3(transform.position.x, transform.position.y + checkGroundYOffSet);
        return Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Vector3 groundCheckPos = new Vector3(transform.position.x, transform.position.y + checkGroundYOffSet);
    //    Gizmos.DrawWireSphere(groundCheckPos, groundCheckRadius);
    //}
}
