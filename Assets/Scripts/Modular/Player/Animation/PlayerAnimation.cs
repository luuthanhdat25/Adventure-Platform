using AbstractClass;
using Manager;
using RepeatUtils.DesignPattern.SingletonPattern;
using System;
using UnityEngine;

public class PlayerAnimation : AbsAnimator
{
    [SerializeField] private Animator animator;

    private bool isJumping = false;
    private float jumpStartTime = 0f;
    private float jumpDuration = 0f;

    private void Update()
    {
        var inputManagerVector = InputManager.Instance.GetRawInputNormalized();
        UpdateMovementAnimation(inputManagerVector.x);
        JumpAnimationHandler(inputManagerVector.y > 0);
        DashAnimationHandler();
    }

    private enum PlayerAnimationEnum
    {
        Idle,
        Run,
        Jump,
        Dash
    }

    private enum PlayerAnimationParameterEnum
    {
        PlayerSpeed,
        IsJump,
        IsDash,
    }

    public override void PlayAnimation(string animationName, bool loop)
    {
        animator.SetBool("Loop", loop);
        animator.Play(animationName);
    }

    public override void SetTimeScale(float timeScale)
    {
        base.SetTimeScale(timeScale);
    }

    public override void SetBool(string paramName, bool value)
    {
        base.SetBool(paramName, value);
    }

    private void UpdateMovementAnimation(float speed)
    {
        animator.SetFloat(PlayerAnimationParameterEnum.PlayerSpeed.ToString(), Math.Abs(speed));
    }

    public void JumpAnimationHandler(bool isJump)
    {
        if (Singleton<PlayerController>.Instance.IsGround())
        {
            HandleLanding();
        }
        else
        {
            HandleJumping(isJump);
        }

        animator.SetBool(PlayerAnimationParameterEnum.IsJump.ToString(), isJumping);
    }
    private void DashAnimationHandler()
    {
        animator.SetBool(PlayerAnimationParameterEnum.IsDash.ToString(), Singleton<PlayerController>.Instance.isDashing);
        //animator.Play(PlayerAnimationEnum.Dash.ToString(), 0, Singleton<PlayerController>.Instance.dashingTime);
    }
    private void HandleLanding()
    {
        if (isJumping)
        {
            isJumping = false;
            PlayAnimation(PlayerAnimationEnum.Idle.ToString(), true);
        }
    }

    private void HandleJumping(bool isJump)
    {
        if (isJumping)
        {
            float elapsedTime = Time.time - jumpStartTime;
            float normalizedTime = elapsedTime / jumpDuration;
            animator.Play(PlayerAnimationEnum.Jump.ToString(), 0, normalizedTime);
        }
        else if (isJump)
        {
            StartJump();
        }
    }

    private void StartJump()
    {
        isJumping = true;
        jumpStartTime = Time.time;
        jumpDuration = GetJumpDuration();
        PlayAnimation(PlayerAnimationEnum.Jump.ToString(), false);
    }

    private float GetJumpDuration()
    {
        float duration = 0f;
        while (!Singleton<PlayerController>.Instance.IsGround())
        {
            duration += Time.deltaTime;
            if (duration > 1f) break;
        }
        return duration;
    }

    public override float GetAnimationDuration(string animationName, int layerIndex = 0)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }

        Debug.LogWarning($"Animation '{animationName}' not found in Animator.");
        return 0f;
    }
}
