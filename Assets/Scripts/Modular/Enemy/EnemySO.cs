using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    [SerializeField]
    private int maxHealth = 100;

    public int MaxHealth => maxHealth;

    [SerializeField]
    private int score = 100;
    public int Score => score;

    [SerializeField]
    private int expDrop = 100;
    public int ExpDrop => expDrop;
}
