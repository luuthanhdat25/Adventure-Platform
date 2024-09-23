using System;
using System.Collections;
using System.Collections.Generic;
using AbstractClass;
using Manager;
using modeling.Defination;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : AbsController
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForced = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float dashingVelocity, dashingTime,dashCooldown;
    [SerializeField] private float checkGround;
    private bool isDashing;
    private bool canDash = true;
    private bool isGroundedCheck = true;

    private enum PlayerAnimationEnum
    {
        Idle,
        Run,
        Jump
    }

    private Rigidbody2D rigidbody2D;
    private TrailRenderer trailRenderer;
    
    private bool isMovingRight = true;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        // trailRenderer = GetComponent<TrailRenderer>();
    }
    
    private void FixedUpdate()
    {
        if(isDashing) return;
        var inputDirection = InputManager.Instance.GetRawInputNormalized();
        rigidbody2D.velocity = new Vector2(inputDirection.x * speed, rigidbody2D.velocity.y);
        if (!isMovingRight && inputDirection.x > 0)
        {
            Flip();
        }else if (isMovingRight && inputDirection.x < 0)
        {
            Flip();
        }

        isGroundedCheck = IsGround();
        if (inputDirection.y > 0 && isGroundedCheck)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,jumpForced);
        }

        if (InputManager.Instance.GetDashInputTrigger())
        {
            if (canDash)
            {
                StartCoroutine(DashHandler());
            }
        }
        
    }
    
    public void ActionHandler()
    {
        
    }
    
    public bool IsGround()
    {
        
        Debug.Log(isGroundedCheck);
        var x = new Vector3(transform.position.x,transform.position.y + checkGround);
        return Physics2D.OverlapCircle(x,0.2f,groundLayer) ;
    }

    private void Flip()
    {
        isMovingRight = !isMovingRight;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1f;
        transform.localScale = currentScale;
    }
    private IEnumerator DashHandler()
    {
        canDash = false;
        isDashing = true;
        rigidbody2D.gravityScale = 0f;
        rigidbody2D.velocity = new Vector2(transform.localScale.x * dashingVelocity,0f);
        //add blur animation code in hear
        yield return new WaitForSeconds(dashingTime);
        //cancel blur animation code in hear
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        rigidbody2D.gravityScale = 1f;
        canDash = true;
    }
    
}
