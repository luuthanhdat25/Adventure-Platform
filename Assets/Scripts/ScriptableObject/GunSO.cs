using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New GunSO", menuName = "ScriptableObjects/GunSO", order = 0)]
    public class GunSO : ScriptableObject
    {
        public string Name;
        public Sprite ImageSprite;
        public GameObject Prefab;
        public ProjectileSO ProjectileSO;
        public SoundSO ShootSoundSO;

        [Space]
        [Header("Stats")]
        [Range(0.1f, 100f)] 
        public float FireRate;

        [Range(1, 1000)]
        public int Damage;
    }
}

