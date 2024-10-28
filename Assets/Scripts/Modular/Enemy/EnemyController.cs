using AbstractClass;
using System;
using UnityEngine;

public abstract class EnemyController : AbsController, IMovable, IAttack
{
    [SerializeField] 
    protected float baseMoveSpeed = 2f;

	[SerializeField]
    protected Transform centerPoint;
    
    [SerializeField]
    protected LayerMask playerLayerMark;

    protected bool isMovingRight = true;
    protected float moveSpeed;
    protected EnemyHeath enemyHeath;

    protected override void Awake()
    {
        base.Awake();
        enemyHeath = absHealth as EnemyHeath;
    }

    private void Start()
    {
        moveSpeed = baseMoveSpeed;
        enemyHeath.OnDead += EnemyHealth_OnDead;
    }

    private void EnemyHealth_OnDead()
    {
        absAnimator.SetTrigger(EnemyAnimatorParameterEnum.Die.ToString());
        // Turn on Static Rigid
        // Turn off Collider
        // Destroy when end animtion
    }

    public abstract float GetAttackRange();
	public abstract void TraceDamage();
    public abstract void Move(Vector2 direction);

    protected virtual void Flip()
    {
        isMovingRight = !isMovingRight;
        absMovement.Rotate(Vector2.left);
    }

	public void Destroy()
    {
        Destroy(gameObject);
    }

	public virtual void ChangeMoveSpeed(float speed) => this.moveSpeed = speed;

	public virtual void RequestAttack() => absAnimator.SetTrigger(EnemyAnimatorParameterEnum.Attack.ToString());

	public AbsController GetController() => this;
}
