using AbstractClass;
using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Datlt/Change Move State (AbsController)", 500)]
public class ChangeMoveState : Leaf
{
	[SerializeField]
	private EnemyMoveStateEnum moveState;

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
        controller.ChangeMoveState(moveState);
		return NodeResult.success;
	}
}
