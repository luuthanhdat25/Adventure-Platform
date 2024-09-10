using UnityEngine;

namespace RepeatUtils.DesignPattern.ObjectPooling
{
    public class OnePrefabObjectPooling : RepeatMonoBehaviour
    {
        [SerializeField] protected Transform prefabTransform;
        [SerializeField] protected Transform poolTransform;

        protected virtual void Start() => prefabTransform.gameObject.SetActive(false);

        public virtual Transform GetTransform()
        {
            Transform newPrefab = GetObjectFromPool();
            newPrefab.SetParent(poolTransform);
            newPrefab.gameObject.SetActive(true);
            return newPrefab;
        }

        protected virtual Transform GetObjectFromPool()
        {
            foreach (Transform objectFromPool in poolTransform)
            {
                if (!objectFromPool.gameObject.activeSelf)
                {
                    return objectFromPool;
                }
            }

            return Instantiate(prefabTransform);
        }

        public virtual void Despawn(Transform obj) => obj.gameObject.SetActive(false);

        public virtual void DespawnAllPool()
        {
            foreach (Transform trans in poolTransform)
            {
                Despawn(trans);
            }
        }
    }
}