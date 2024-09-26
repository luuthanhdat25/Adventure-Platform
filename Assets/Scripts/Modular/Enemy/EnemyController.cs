using AbstractClass;
using Manager;
using MBT;
using Unity.VisualScripting;
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

    [SerializeField]
    private float perceptionDistance;

    private bool isMovingRight = true;
    private EnemyAnimator enemyAnimator;
    private Blackboard blackboard;

    private void Start()
    {
        enemyAnimator = absAnimator as EnemyAnimator;
        LoadComponent(ref blackboard, gameObject);
        var playerPosition = blackboard.GetVariable<Vector2Variable>("AttackPlayerPosition");
        playerPosition.Value = Random.insideUnitCircle;
    }

    /*void FixedUpdate()
    {
        var inputDirection = InputManager.Instance.GetRawInputNormalized();
        MoveHorizontal(inputDirection);
    }*/

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(GetAttackPointPosition(), transform.right, perceptionDistance, playerLayerMark);
        var playerPosition = blackboard.GetVariable<Vector2Variable>("AttackPlayerPosition");
        var isSeePlayer = blackboard.GetVariable<BoolVariable>("IsSeePlayer");
        if (hit.collider != null)
        {
            //Debug.Log($"Hit {hit.collider.name} at position {hit.point}");
            Debug.DrawRay(GetAttackPointPosition(), transform.right * perceptionDistance, Color.red);
            Debug.DrawRay(GetAttackPosition(hit.point), Vector3.up * 5, Color.black);

            playerPosition.Value = GetAttackPosition(hit.point);
            isSeePlayer.Value = true;
        }
        else
        {
            Debug.DrawRay(GetAttackPointPosition(), transform.right * perceptionDistance, Color.green);
            isSeePlayer.Value = false;
        }
    }

    private Vector2 GetAttackPosition(Vector2 playerPosition)
    {
        var attackPosition = playerPosition;
        attackPosition.y = transform.position.y;
        float attackDiff = attackRadius;
        var vectorDirection = playerPosition - (Vector2)transform.position;
        if(vectorDirection.x < 0)
        {
            attackPosition.x += attackDiff;
        }
        else
        {
            attackPosition.x -= attackDiff;
        }
        return attackPosition;
    }

    public void MoveHorizontal(Vector2 inputDirection)
    {
        absMovement.Move(inputDirection, speed);
            
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
