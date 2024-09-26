using AbstractClass;
using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Datlt/Move To Position (Abs Controller)", 100)]
public class MoveToPosition : Leaf
{
    public Vector2Reference targetPosition;
    public float minDistance = 0f;
    private EnemyController controller;
    private Vector2 moveDirection;

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

        if (!IsTargetReached())
        {
            moveDirection = targetPosition.Value - (Vector2)transform.position; 
            controller.MoveHorizontal(moveDirection);
            return NodeResult.running;
        }
        else
        {
            controller.MoveHorizontal(Vector2.zero);
            return NodeResult.success;
        }
    }

    private bool IsTargetReached()
    {
        return Vector2.Distance(targetPosition.Value, transform.position) <= minDistance;
    }
}
