using AbstractClass;
using Manager;
using System;
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

    private PlayerMovement playerMovement;
    private bool isMovingRight = true;

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

        playerMovement.Move(inputVector, speed);

        if (inputVector.x > 0 && !isMovingRight || inputVector.x < 0 && isMovingRight)
        {
            isMovingRight = !isMovingRight;
            absMovement.Flip();
        }

        if (InputManager.Instance.IsDashInputTrigger())
        {
            playerMovement.Dash();
        }

        if (InputManager.Instance.IsAttackPressed())
        {
            playerCombo.HandleCombo();
        }

        bool isGround = IsGround();
        if (isGround)
        {
            Debug.Log(Math.Abs(inputVector.x));
            playerMovement.JumpHandler(inputVector.y);
            absAnimator.SetFloat(PlayerAnimationParameter.PlayerSpeed.ToString(), Math.Abs(inputVector.x));
        }

        absAnimator.SetBool(PlayerAnimationParameter.IsJump.ToString(), !isGround);
    }

    public bool IsGround()
    {
        Vector3 groundCheckPos = new Vector3(transform.position.x, transform.position.y + checkGroundYOffSet);
        return Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 groundCheckPos = new Vector3(transform.position.x, transform.position.y + checkGroundYOffSet);
        Gizmos.DrawWireSphere(groundCheckPos, groundCheckRadius);
    }
}
