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
		Vector2 attackPos = targetPosition.Value;
		var vectorDirection = targetPosition.Value - (Vector2)transform.position;
		var attackRange = attacker.GetAttackRange();
		if (vectorDirection.x < 0)
		{
			attackPos.x += attackRange;
		}
		else
		{
			attackPos.x -= attackRange;
		}
		attackPosition.Value = attackPos;
		DebugShape.DrawCircle(attackPos, 0.5f, 2f, Color.red);
		return NodeResult.success;
	}
}
