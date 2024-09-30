using AbstractClass;
using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Datlt/Calculate Attack Position", 400)]
public class CalculateAttackPosition : Leaf
{
	[SerializeField]
	private Vector2Reference attackPosition;

	[SerializeField]
	private Vector2Reference targetPosition;

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
		var attackPos = targetPosition.Value;
		var vectorDirection = targetPosition.Value - (Vector2)transform.position;
		var attackRange = controller.GetAttackRange();
		if (vectorDirection.x < 0)
		{
			attackPos.x += attackRange;
		}
		else
		{
			attackPos.x -= attackRange;
		}
		attackPosition.Value = attackPos;
		return NodeResult.success;
	}
}
