using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerSO", menuName = "ScriptableObjects/PlayerSO", order = 3)]
public class PlayerSO : ScriptableObject
{
    public int Health;
    public float MoveSpeed;
}
