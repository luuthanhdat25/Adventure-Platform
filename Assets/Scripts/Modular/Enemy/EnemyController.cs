using AbstractClass;
using MBT;
using UnityEngine;

public class EnemyController : AbsController
{
    [SerializeField] 
    private float baseMoveSpeed = 2f;

	[SerializeField]
	private float chaseMoveSpeed = 4f;

	[SerializeField]
    private Transform centerPoint;
    
    [Header("[Attack]")]
    [SerializeField]
    private float attackRadius = 2f;

    [SerializeField]
    private Transform attackPoint;

    [SerializeField]
    private LayerMask playerLayerMark;

    private bool isMovingRight = true;
    private EnemyAnimator enemyAnimator;
    private EnemyMoveStateEnum moveState = EnemyMoveStateEnum.Normal;

	private void Start()
    {
        enemyAnimator = absAnimator as EnemyAnimator;
    }

    public float GetAttackRange()
    {
        return Vector2.Distance(centerPoint.position, GetAttackPointPosition());
    }

	public void MoveHorizontal(Vector2 inputDirection)
    {
        if(moveState == EnemyMoveStateEnum.Normal)
        {
            absMovement.Move(inputDirection, baseMoveSpeed);
        }
        else
        {
            absMovement.Move(inputDirection, chaseMoveSpeed);
        }
            
        if (inputDirection != Vector2.zero)
        {
            enemyAnimator.SetBool(EnemyAnimatorParameterEnum.IsWalking.ToString(), true);
        }
        else
        {
            enemyAnimator.SetBool(EnemyAnimatorParameterEnum.IsWalking.ToString(), false);
        }

        if (!isMovingRight && inputDirection.x > 0)
        {
            Flip();
        }
        else if (isMovingRight && inputDirection.x < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isMovingRight = !isMovingRight;
        absMovement.Rotate(Vector2.left);
    }

    public void PlayAttackAnimation()
    {
        absAnimator.SetTrigger(EnemyAnimatorParameterEnum.Attack.ToString());
    }

    public void TraceDamage()
    {
        Debug.Log("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(GetAttackPointPosition(), attackRadius, playerLayerMark);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            var beAttackController = enemy.GetComponent<AbsController>();
            if(beAttackController != null)
            {
                AbsDamageSender.CollisionWithController(beAttackController);
            }
        }
    }

	private Vector2 GetAttackPointPosition() => attackPoint.position;

	public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetAttackPointPosition(), attackRadius);
    }

    public void ChangeMoveState(EnemyMoveStateEnum enemyMoveStateEnum)
    {
        this.moveState = enemyMoveStateEnum;
    }
}
