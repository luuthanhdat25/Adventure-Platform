using AbstractClass;
using UnityEngine;

public class EnemyMovement : AbsMovement
{
    [SerializeField]
    private new Rigidbody2D rigidbody2D;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadComponentInParent<Rigidbody2D>(ref rigidbody2D);
    }

    public override void Move(Vector3 moveDirectionOrDestination, float speed)
    {
        moveDirectionOrDestination = moveDirectionOrDestination.normalized;
        rigidbody2D.velocity = new Vector2(moveDirectionOrDestination.x * speed, rigidbody2D.velocity.y);
    }

    public override void Rotate(Vector3 rotateDirection)
    {
        Quaternion currentRotation = transform.parent.rotation;
        Quaternion flippedRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y + 180f, currentRotation.eulerAngles.z);
        transform.parent.rotation = flippedRotation;
    }
}
