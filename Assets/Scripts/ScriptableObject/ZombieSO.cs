using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New ZombieSO", menuName = "ScriptableObjects/ZomebieSO", order = 4)]
    public class ZombieSO : ScriptableObject
    {
        public int Health;
        public int Damage;
        public float MoveSpeed;
    }
}
