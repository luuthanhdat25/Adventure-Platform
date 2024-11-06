using AbstractClass;
using UnityEngine;

public class EnemyHeath : CharacterHealth
{
    [SerializeField]
    private EnemySO enemyHealthSO;

    protected override void Awake()
    {
        base.Awake();
        hpMax = enemyHealthSO.MaxHealth;
    }
}
