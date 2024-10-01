using UnityEngine;

public interface IMovable 
{
    void ChangeMoveSpeed(float speed);
    void Move(Vector2 direction);
}
