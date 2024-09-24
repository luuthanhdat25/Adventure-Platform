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
        rigidbody2D.velocity = new Vector2(moveDirectionOrDestination.x * speed, rigidbody2D.velocity.y);
    }

    public override void Rotate(Vector3 rotateDirection)
    {
        Vector3 currentScale = transform.parent.localScale;
        currentScale.x *= -1f;
        transform.parent.localScale = currentScale;
    }
}
