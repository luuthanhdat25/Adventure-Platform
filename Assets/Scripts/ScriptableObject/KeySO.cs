using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New KeySO", menuName = "ScriptableObjects/KeySO", order = 5)]
    public class KeySO : ScriptableObject
    {
        public string Name;
        public Sprite KeyIconSprite;
        public GameObject Prefab;
    }
}
