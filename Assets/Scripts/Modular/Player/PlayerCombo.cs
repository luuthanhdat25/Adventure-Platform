using System;
using AbstractClass;
using RepeatUtils;
using System.Collections;
using System.Threading;
using ScriptableObjects;
using UnityEngine;

public class PlayerCombo : RepeatMonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private Transform attackPoint;

    [SerializeField] private float attackRadius;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private CharacterHealth characterHealth;
                    
    public bool isAttackCombo;
    private int combo;
    private AbsAnimator animator;
    public bool isPerformingSkill;
    private bool canPerformingSkill = true;
    private Coroutine resetComboCoroutine;
    public bool isAttaking;
    private SkillController skillController;

    public void Start()
    {
        skillController = new SkillController();
        characterHealth.OnDead += DeadHandler;
    }

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
            {
                StopCoroutine(resetComboCoroutine);
                isAttaking = true;
            }

            resetComboCoroutine = StartCoroutine(ResetComboAfterDelay());
            isAttaking = false;
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
        combo = 0;
        isAttackCombo = false;
    }

    public void StartSkill() => isPerformingSkill = true;

    public void EndSkill() => isPerformingSkill = false;

    public void SkillHandle()
    {
        if (!canPerformingSkill) return;
        StartCoroutine(PerformSkill());
    }
    private SkillDTO GetSelectedSkill()
    {
        return skillController.GetCurrentSKill().Result;
    }
    private IEnumerator PerformSkill()
    {
        Vector3 attackPos = new Vector3(attackPoint.position.x, attackPoint.position.y);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos, attackRadius, enemyLayer);

        canPerformingSkill = false;

        if (!isPerformingSkill)
        {
            SkillDTO selectedSkill = GetSelectedSkill();
            Debug.Log(selectedSkill.skillName);
            animator.SetTrigger(GetSkillAnimationTrigger(selectedSkill));
            foreach (var item in hitEnemies)
            {
                Debug.Log("Skill hitted");
            }

            yield return new WaitForSeconds(selectedSkill.cooldown);

            EndSkillAction();
        }
    }

    private string GetSkillAnimationTrigger(SkillDTO skill)
    {
        return skill.skillName == SkillAbilityEnum.AttackFire
            ? PlayerAnimationParameter.AttackFire.ToString()
            : PlayerAnimationParameter.AttackWater.ToString();
    }

    private void EndSkillAction()
    {
        isPerformingSkill = false;
        canPerformingSkill = true;
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


    private void IsAttacking()
    {
        isAttaking = true;
    }

    public void OnDrawGizmos()
    {
        var x = new Vector3(attackPoint.position.x, attackPoint.position.y);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(x, attackRadius);
    }

    private IEnumerator ResetComboAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isAttackCombo = false;
        resetComboCoroutine = null;
        isAttaking = false;
    }


    private void DeadHandler()
    {
        Debug.Log("Dead");
    }
}