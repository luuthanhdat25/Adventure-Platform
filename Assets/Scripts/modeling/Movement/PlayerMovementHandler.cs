using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jump_forced = 5f;
    [SerializeField] private KeyCode jump_key;
    [SerializeField] private KeyCode move_left;
    [SerializeField] private KeyCode move_right;
    

    private Rigidbody2D _rigidbody2D;
    
    

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
       ActionHandler();
    }

    public void ActionHandler()
    {
        _rigidbody2D.velocity = new Vector2(
            MovementHandler() * speed,
            JumpHandler() * jump_forced);
    }

    private float JumpHandler()
    {
        float jump_result = 0f;
        if (Input.GetKey(jump_key))
        {
            jump_result = 1f;
        }
        return jump_result;
    }
    private float MovementHandler()
    {
        float move_direction = 0f;
        if (Input.GetKey(move_right))
        {
            move_direction = 1f;
        }
        else if (Input.GetKey(move_left))
        {
            
            move_direction = -1f;
        }

        return move_direction;
    }
    
}
