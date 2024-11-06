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
    [SerializeField] private PlayerHealth characterHealth;
    [SerializeField] private SkillController skillController;



    public bool isAttackCombo;
    private int combo;
    private AbsAnimator animator;
    public bool isPerformingSkill;
    private bool canPerformingSkill = true;
    private Coroutine resetComboCoroutine;
    private SpriteRenderer spriteRenderer;
    public bool isAttaking;
    private BoxCollider2D boxCollider;
    public Action OnSlash;


    public void Start()
    {
        characterHealth.OnDead += DeadHandler;
    }

    protected override void LoadComponents()
    {
        LoadComponent(ref playerController, gameObject);
        LoadComponent(ref spriteRenderer, gameObject);
        LoadComponent(ref boxCollider,gameObject);
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
        PlayerSingleton.Instance.DeductStamina(10);
        isAttackCombo = false;
        OnSlash.Invoke();
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
        PlayerSingleton.Instance.DeductStamina(20);
        Vector3 attackPos = new Vector3(attackPoint.position.x, attackPoint.position.y);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos, attackRadius, enemyLayer);

        canPerformingSkill = false;

        if (!isPerformingSkill)
        {
            SkillDTO selectedSkill = GetSelectedSkill();
            if (selectedSkill != null)
            {
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
        if (!isAttaking)
        {
            StartCoroutine(StaminaControl());
        }

    }

    private IEnumerator StaminaControl()
    {
        if (PlayerSingleton.Instance.IsOutOfStamina())
        {
            yield return new WaitForSeconds(2.0f);
        }
        StartCoroutine(ResetStamina());
    }
    private IEnumerator ResetStamina()
    {

        while (!PlayerSingleton.Instance.IsFullStamina())
        {
            PlayerSingleton.Instance.AddStamina(1);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void DeadHandler()
    {
        Debug.Log("Dead");
        spriteRenderer.color = new Color(0xFF, 0x9A, 0x9A, 0xFF);
        //this.gameObject.transform.rotation.z += 90;
        boxCollider.enabled = false;
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(1.5f);
    }
}