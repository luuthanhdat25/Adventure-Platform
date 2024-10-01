using AbstractClass;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Datlt/Attack (Abs Controller)", 200)]
public class Attack : Leaf
{
    private IAttack attacker;

    private void Start()
    {
        attacker = GetComponent<IAttack>();
        if (attacker == null)
        {
            Debug.LogError($"Gameobject: {gameObject.name} doesn't have {typeof(IAttack).Name}");
        }
    }

    public override NodeResult Execute()
    {
        if (attacker == null) return NodeResult.failure;
        attacker.RequestAttack();
        return NodeResult.success;
    }
}
