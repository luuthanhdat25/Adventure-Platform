using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator _animator;
    private string current_state;
    private bool is_right_position = true;
    public string CurrentState
    {
        get => current_state;
        set => current_state = value;
    }
    
    public bool IsRighPosition => is_right_position;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    
    public void ChangeAnimationState(string new_state)
    {
        if (current_state == new_state)
        {
            return;
        }
        int new_state_hash = Animator.StringToHash(new_state);

        if (!_animator.HasState(0, new_state_hash))
        {
            Debug.LogWarning("State " + new_state + " does not exist in the Animator.");
            return;
        }

        _animator.Play(new_state);
        current_state = new_state;
    }
    public void Flip()
    {
        is_right_position = !is_right_position;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    
}
