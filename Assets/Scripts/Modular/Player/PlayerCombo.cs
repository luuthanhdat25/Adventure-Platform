using AbstractClass;
using RepeatUtils;
using System.Collections;
using UnityEngine;

public class PlayerCombo : RepeatMonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField] 
    private Transform attackPoint;

    [SerializeField] 
    private float attackRadius;

    [SerializeField] 
    private LayerMask enemyLayer;

    private bool isAttackCombo;
    private int combo;
    private AbsAnimator animator;
    private Coroutine resetComboCoroutine;

    protected override void LoadComponents()
    {
        LoadComponent(ref playerController, gameObject);
        animator = playerController.AbsAnimator;
    }

    public void HandleCombo()
    {
        if (!isAttackCombo)
        {
            animator.SetTrigger(PlayerAnimationParameter.IsAttack.ToString());
            animator.SetFloat(PlayerAnimationParameter.AttackComboNumber.ToString(), combo);
            isAttackCombo = true;
            if (resetComboCoroutine != null)
                StopCoroutine(resetComboCoroutine);
            resetComboCoroutine = StartCoroutine(ResetComboAfterDelay());
            PerformAttack();
        }
    }

    public void StartCombo()
    {
        isAttackCombo = false;
        combo++;
        if (combo == 3)
            EndCombo();
    }

    public void EndCombo()
    {
        isAttackCombo = false;
        combo = 0;
    }

    private void PerformAttack()
    {

        Vector3 attackPos = new Vector3(attackPoint.position.x, attackPoint.position.y);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos, attackRadius, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hitted");
            var enemyController = enemy.GetComponent<AbsController>();
            if (enemyController != null)
            {
                // AbsDamageSender.CollisionWithController(enemyController);
            }
        }
    }



    public void OnDrawGizmos()
    {
        var x = new Vector3(attackPoint.position.x, attackPoint.position.y);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(x, attackRadius);
    }
    private IEnumerator ResetComboAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        isAttackCombo = false;
        resetComboCoroutine = null;
    }
}
