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

    [SerializeField] private SkillTreeUI skillTreeUI;

    [SerializeField]
    private playerSO playerSO;
    [SerializeField]
    private SkillSO skillSO1;
    [SerializeField]
    private SkillSO skillSO2;



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
        absAnimator.SetBool(PlayerAnimationParameter.IsDash.ToString(), playerMovement.IsDashing);

        absAnimator.SetFloat(PlayerAnimationParameter.JumpVectorVertical.ToString(), playerMovement.GetVelocity().y);


        if (playerMovement.IsDashing) return;
        var inputVector = InputManager.Instance.GetRawInputNormalized();
        if (!isOpenTabUpgrade)
        {
            playerMovement.Move(inputVector, speed);
        }
        if (inputVector.x > 0 && !isMovingRight || inputVector.x < 0 && isMovingRight)
        {
            
            isMovingRight = !isMovingRight;
            absMovement.Flip();
        }

        if (InputManager.Instance.IsDashInputTrigger() && !isOpenTabUpgrade )
        {
            playerMovement.Dash();
        }

        if (InputManager.Instance.IsAttackPressed() && !isOpenTabUpgrade && IsGround())
        {
            playerCombo.HandleCombo();
        }
        Debug.Log($"jump count: {jumpCount}");
        if (IsGround())
        {
            jumpCount = 0;
            absAnimator.SetFloat(PlayerAnimationParameter.PlayerSpeed.ToString(), Math.Abs(inputVector.x));
        }
        if (InputManager.Instance.IsJumpInputTrigger() && jumpCount < 2)
        {
            jumpCount++;
            playerMovement.JumpHandler(inputVector.y);
        }
        if (InputManager.Instance.IsPerformSkillPressed() && !isOpenTabUpgrade && IsGround())
        {
            if(playerSO.currentSelectedSkill == skillSO1)
            {
                absAnimator.SetTrigger(PlayerAnimationParameter.AttackFire.ToString());
            } else
            {
                absAnimator.SetTrigger(PlayerAnimationParameter.AttackWater.ToString());
            }
        }
        if (InputManager.Instance.IsTabIsOpenedPressed())
        {
            if (!isOpenTabUpgrade)
            {
                skillTreeUI.OpenTabUpgrade();
                isOpenTabUpgrade = true;
            }
            else
            {
                skillTreeUI.CloseTabUpgrade();
                isOpenTabUpgrade= false;
            }
        }


        absAnimator.SetBool(PlayerAnimationParameter.IsJump.ToString(), !IsGround());
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

