using AbstractClass;
using ScriptableObjects;
using UnityEngine;

namespace Gun
{
    public class GunController : AbsController
    {
        [Header("Gun Controller")]
        [SerializeField]
        private Transform shootingPoint;

        [SerializeField]
        private GunSO gunSO;

        protected override void Awake()
        {
            base.Awake();
            if (gunSO == null) Debug.LogError($"{gameObject.name} doesn't have GunSO");
        }

        public void SpawnProjectile(Vector3 shootDirection)
        {
            var shootPosition = shootingPoint.position;
            //Spawn 
            var projectile = gunSO.ProjectileSO.Prefab;
        }
    }
}
