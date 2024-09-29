using System.Collections;
using System.Collections.Generic;
using AbstractClass;
using Manager;
using UnityEngine;

public class PlayerMovement : AbsMovement
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForced = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public float dashingVelocity, dashingTime, dashCooldown;
    [SerializeField] private float checkGround;
    [SerializeField] private PlayerAnimation playerAnimation;



    private bool isJumping = false;
    public bool isDashing = false;
    private bool canDash = true;
    public int combo;
    public bool isAttackCombo = false;
    private bool isGroundedCheck = true;
    private bool isMovingRight = true;


    public Rigidbody2D rigidbody2D;
    private TrailRenderer trailRenderer;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void ActionHanndler()
    {
        if (isDashing) return;
        isGroundedCheck = IsGround();
        var inputVector = InputManager.Instance.GetRawInputNormalized();
        bool userPressedDashButton = InputManager.Instance.IsDashInputTrigger();
        MoveHandler(inputVector,speed);
        DashHandler(userPressedDashButton);
        JumpHandler(inputVector, jumpForced);
        Combo();
    }

    private void JumpHandler(Vector3 jumpDirectionOrDestination, float jumpForced)
    {
        if (jumpDirectionOrDestination.y > 0 && isGroundedCheck)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForced);
        }
    }

    public override void Move(Vector3 moveDirectionOrDestination, float speed)
    {
        rigidbody2D.velocity = new Vector2(moveDirectionOrDestination.x * speed, rigidbody2D.velocity.y);
    }

    private void MoveHandler(Vector3 moveDirectionOrDestination, float speed)
    {
        Move(moveDirectionOrDestination, speed);
        if (!isMovingRight && moveDirectionOrDestination.x > 0)
        {
            isMovingRight = !isMovingRight;
            Flip();
        }
        else if (isMovingRight && moveDirectionOrDestination.x < 0)
        {
            isMovingRight = !isMovingRight;
            Flip();
        }

    }

    public bool IsGround()
    {
        var x = new Vector3(transform.position.x, transform.position.y + checkGround);
        return Physics2D.OverlapCircle(x, 0.2f, groundLayer);
    }
    //public void OnDrawGizmos()
    //{
    //    var x = new Vector3(transform.position.x, transform.position.y + checkGround);
    //    Gizmos.DrawWireSphere(x, 0.2f);
    //}

    public override void Rotate(Vector3 rotateDirection)
    {
        throw new System.NotImplementedException();
    }

    public void DashHandler(bool isPressButton)
    {
        if (isPressButton)
        {
            if (canDash)
            {
                StartCoroutine(Dash());
            }
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rigidbody2D.gravityScale = 0f;
        rigidbody2D.velocity = new Vector2(transform.localScale.x * dashingVelocity, 0f);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        rigidbody2D.gravityScale = 1f;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void StartCombo()
    {
        isAttackCombo = false;
        combo++;
        if(combo ==3 )
        {
            EndCombo();
        }
    }
    public void EndCombo()
    {
        isAttackCombo = false;
        combo = 0;  
    }
    public void Combo()
    {
        Debug.Log(isAttackCombo);
        if(!isAttackCombo && InputManager.Instance.IsAttackHold())
        {
            isAttackCombo = true;
        }
    }
}
