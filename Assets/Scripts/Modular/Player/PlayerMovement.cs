using AbstractClass;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : AbsMovement
{

    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float dashingVelocity, dashingTime, dashCooldown;

    [SerializeField] private float checkGround;

    [SerializeField] private Rigidbody2D rigidbody2D;

    private bool isJumping, canDash = true;
    private bool isDashing = false;
    public bool IsDashing => isDashing;
    public Action onDash;
    public Action onJump;

    public Vector2 GetVelocity() => rigidbody2D.velocity;

    public void JumpHandler(float inputY)
    {
        if (inputY > 0)
        {
            onJump.Invoke();
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
        }
    }

    public override void Move(Vector3 moveDirection, float speed)
    {
        rigidbody2D.velocity = new Vector2(moveDirection.x * speed, rigidbody2D.velocity.y);
    }

    public void Dash()
    {
        if (!canDash) return;
        onDash.Invoke();
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;
        rigidbody2D.gravityScale = 0f;
        float facingDirection = Mathf.Sign(Mathf.Cos(transform.parent.rotation.eulerAngles.y * Mathf.Deg2Rad));
        rigidbody2D.velocity = new Vector2(facingDirection * dashingVelocity, 0f);
        PlayerSingleton.Instance.DeductStamina(20);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        rigidbody2D.gravityScale = 1f;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
