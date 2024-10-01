using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Datlt/Change Move Speed", 500)]
public class ChangeMoveSpeed : Leaf
{
	[SerializeField]
	private float moveSpeed;

	private IMovable movable;

	private void Start()
	{
		movable = GetComponent<IMovable>();
		if (movable == null)
		{
			Debug.LogError($"Gameobject: {gameObject.name} doesn't have {typeof(IMovable).Name}");
		}
	}

	public override NodeResult Execute()
	{
        if (movable == null) return NodeResult.failure;
        movable.ChangeMoveSpeed(moveSpeed);
		return NodeResult.success;
	}
}
