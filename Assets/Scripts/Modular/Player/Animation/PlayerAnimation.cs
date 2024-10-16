using AbstractClass;
using Manager;
using RepeatUtils.DesignPattern.SingletonPattern;
using System;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerAnimation : AbsAnimator
{
    [SerializeField] 
    private Animator animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadComponent(ref animator, gameObject);
    }

    public override void SetBool(string parraName, bool value)
    {
        animator.SetBool(parraName, value);
    }

    public override void SetTrigger(string parraName)
    {
        animator.SetTrigger(parraName);
    }

    public override void SetFloat(string paraName, float value)
    {
        animator.SetFloat(paraName, value);
    }

    public override void CrossFade(string toString, float d)
    {
        animator.CrossFade(toString,d);
    }
}
