using AbstractClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyController : EnemyController
{
    [SerializeField]
    private float attackRadius = 2f;

    [SerializeField]
    private Transform attackPoint;

    private Vector2 GetAttackPointPosition()
    {
        if (attackPoint == null) return Vector2.zero;
        return attackPoint.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetAttackPointPosition(), attackRadius);
    }

    public override float GetAttackRange()
    {
        return Vector2.Distance(centerPoint.position, GetAttackPointPosition());
    }

    public override void TraceDamage()
    {
        Debug.Log("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(GetAttackPointPosition(), attackRadius, playerLayerMark);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            var beAttackController = enemy.GetComponent<AbsController>();
            if (beAttackController != null)
            {
                AbsDamageSender.CollisionWithController(beAttackController);
            }
        }
    }

    public override void Move(Vector2 direction)
    {
        absMovement.Move(direction, moveSpeed);

        if (direction != Vector2.zero)
        {
            absAnimator.SetBool(EnemyAnimatorParameterEnum.IsWalking.ToString(), true);
        }
        else
        {
            absAnimator.SetBool(EnemyAnimatorParameterEnum.IsWalking.ToString(), false);
        }

        if (!isMovingRight && direction.x > 0)
        {
            Flip();
        }
        else if (isMovingRight && direction.x < 0)
        {
            Flip();
        }
    }
}

