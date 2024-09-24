using AbstractClass;
using Manager;
using UnityEngine;

public class EnemyController : AbsController
{
    [SerializeField] 
    private float speed = 10f;

    [SerializeField]
    private float attackRadius = 2f;

    [SerializeField]
    private Vector2 attackOffset;

    [SerializeField]
    private LayerMask playerLayerMark;


    private bool isMovingRight = true;
    private EnemyAnimator enemyAnimator;

    private void Start()
    {
        enemyAnimator = absAnimator as EnemyAnimator;
    }

    /*void FixedUpdate()
    {
        var inputDirection = InputManager.Instance.GetRawInputNormalized();
        absMovement.Move(inputDirection, speed);

        if(inputDirection != Vector2.zero)
        {
            enemyAnimator.SetBool(EnemyAnimatorParameterEnum.IsWalking.ToString(), true);
        }
        else
        {
            enemyAnimator.SetBool(EnemyAnimatorParameterEnum.IsWalking.ToString(), false);
        }

        if (!isMovingRight && inputDirection.x > 0)
        {
            isMovingRight = !isMovingRight;
            absMovement.Rotate(Vector2.left);
        }
        else if (isMovingRight && inputDirection.x < 0)
        {
            isMovingRight = !isMovingRight;
            absMovement.Rotate(Vector2.left);
        }
    }*/

    public void Attack()
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

    private Vector2 GetAttackPointPosition()
    {
        float yPos = transform.position.y + attackOffset.y;
        float xPos = isMovingRight? transform.position.x + attackOffset.x : transform.position.x - attackOffset.x;
        return new Vector2(xPos, yPos);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetAttackPointPosition(), attackRadius);
    }
}
