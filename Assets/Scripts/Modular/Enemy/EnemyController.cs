using AbstractClass;
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

	private void Start()
    {
        moveSpeed = baseMoveSpeed;
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
