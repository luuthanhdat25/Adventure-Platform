using AbstractClass;
using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Datlt/Move To Position", 100)]
public class MoveToPosition : Leaf
{
    [SerializeField]
    private Vector2Reference targetPosition;

	[SerializeField]
    private float minDistance = 0f;
    
    private IMovable movable;
    private Vector2 moveDirection;

    private void Start()
    {
        movable = GetComponent<AbsController>() as EnemyController;
        if (movable == null)
        {
            Debug.LogError($"Gameobject: {gameObject.name} doesn't have {typeof(EnemyController).Name}");
        }
    }
        
    public override NodeResult Execute()
    {
        if (movable == null) return NodeResult.failure;

        if (!IsTargetReached())
        {
            moveDirection = targetPosition.Value - (Vector2)transform.position; 
            movable.Move(moveDirection);
            return NodeResult.running;
        }
        else
        {
            movable.Move(Vector2.zero);
            return NodeResult.success;
        }
    }

    private bool IsTargetReached()
    {
        return Vector2.Distance(targetPosition.Value, transform.position) <= minDistance;
    }
}
