using AbstractClass;
using UnityEngine;

public class EnemyAnimator : AbsAnimator
{
    [SerializeField]
    private Animator animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadComponent(ref animator, gameObject);
    }

    public override void SetTrigger(string parraName) => animator.SetTrigger(parraName);
    public override void SetBool(string parraName, bool value) => animator.SetBool(parraName, value);
}
