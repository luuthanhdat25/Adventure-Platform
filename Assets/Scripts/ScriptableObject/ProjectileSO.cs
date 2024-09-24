using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New ProjectileSO", menuName = "ScriptableObjects/ProjectileSO", order = 1)]
    public class ProjectileSO : ScriptableObject
    {
        public GameObject Prefab;

        [Range(0.1f, 1000f)]
        public float SpeedMove;
    }
}
