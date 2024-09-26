using AbstractClass;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Datlt/Attack (Abs Controller)", 200)]
public class Attack : Leaf
{
    private EnemyController controller;

    private void Start()
    {
        controller = GetComponent<AbsController>() as EnemyController;
        if (controller == null)
        {
            Debug.LogError($"Gameobject: {gameObject.name} doesn't have {typeof(EnemyController).Name}");
        }
    }

    public override NodeResult Execute()
    {
        if (controller == null) return NodeResult.failure;
        controller.PlayAttackAnimation();
        return NodeResult.success;
    }
}
