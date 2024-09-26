using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Datlt/Find Random Patrol Position Ground", 500)]
public class FindRandomPatrolPositionGround : Leaf
{
    public Transform leftRangePosition;
    public Transform rightRangePosition;
    public Vector2Reference blackboardVariable = new Vector2Reference(VarRefMode.DisableConstant);
    private bool isMoveLeft = true;

    public override NodeResult Execute()
    {
        blackboardVariable.Value = new Vector2(
            GetRandomXCoordinate(),
            transform.position.y
        );
        return NodeResult.success;
    }

    private float GetRandomXCoordinate()
    {
        float xRandomPosition;
        if (isMoveLeft)
        {
            xRandomPosition = Random.Range(leftRangePosition.position.x, transform.position.x);
        }
        else
        {
            xRandomPosition = Random.Range(transform.position.x, rightRangePosition.position.x);
        }
        isMoveLeft = !isMoveLeft;
        return xRandomPosition;
    }
}
