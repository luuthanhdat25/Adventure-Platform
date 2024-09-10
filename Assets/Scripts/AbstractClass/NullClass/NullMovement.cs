using AbstractClass;
using UnityEngine;

public class NullMovement : AbsMovement
{
    public override void Move(Vector3 moveDirectionOrDestination, float speed)
    {
        return;
    }

    public override void Rotate(Vector3 rotateDirection)
    {
        return;
    }
}
