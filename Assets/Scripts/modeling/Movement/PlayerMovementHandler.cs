using System.Collections;
using System.Collections.Generic;
using modeling.Defination;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementHandler : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed;
    [SerializeField] private float jump_forced = 5f;
    [SerializeField] private Transform ground_check;
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private float dashing_velocity, dashing_time,dash_cooldown;
    private bool is_dashing;
    private bool can_dash = true;
    
    

    private Rigidbody2D _rigidbody2D;
    private PlayerDefination _playerDefination;
    private TrailRenderer _trailRenderer;
    
    private bool is_moving_right = true;
    

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }


    void Update()
    {
        if(is_dashing) return;
        ActionHandler();
    }

   
    public void ActionHandler()
    {
        _rigidbody2D.velocity = new Vector2(horizontal * speed, _rigidbody2D.velocity.y);
        if (!is_moving_right && horizontal > 0)
        {
            Flip();
        }else if (is_moving_right && horizontal < 0)
        {
            Flip();
        }
    }
    public bool IsGround()
    {
        return Physics2D.OverlapCircle(ground_check.position, 0.2f, ground_layer);
    }

    private void Flip()
    {
        is_moving_right = !is_moving_right;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1f;
        transform.localScale = currentScale;
    }
    public void MovementHandler(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        
        if (context.performed && IsGround())
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,jump_forced);
        }
        if (context.canceled && _rigidbody2D.velocity.y > 0f)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.1f);
        }  
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (can_dash)
        {
            StartCoroutine(DashHandler());
        }
    }

    private IEnumerator DashHandler()
    {
        can_dash = false;
        is_dashing = true;
        float original_gravity = _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.velocity = new Vector2(transform.localScale.x * dashing_velocity,0f);
        //add blur animation code in hear
        yield return new WaitForSeconds(dashing_time);
        //cancel blur animation code in hear
        is_dashing = false;
        yield return new WaitForSeconds(dash_cooldown);
        can_dash = true;

    }
    
}
