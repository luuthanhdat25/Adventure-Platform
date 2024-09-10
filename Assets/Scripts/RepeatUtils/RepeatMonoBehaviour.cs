using System.Collections.Generic;
using UnityEngine;

namespace RepeatUtils
{
    public class RepeatMonoBehaviour : MonoBehaviour
    {
        protected virtual void Reset()
        {
            this.LoadComponents();
            this.ResetValue();
        }
    
        protected virtual void Awake()
        {
            this.LoadComponents();
            this.ResetValue();
        }
    
        protected virtual void ResetValue()
        {
            // For override
        }

        protected virtual void LoadComponents()
        {
            // For override
        }

        public static T LoadComponent<T>(ref T component, GameObject gameObj) where T : Component
        {
            if (component != null) return component;

            component = gameObj.GetComponent<T>();
            if (component != null) return component;

            Debug.LogError($"Component [{typeof(T).Name}] doesn't have in GameObject [{gameObj.name}]");
            return null;
        }

        public static T LoadComponentInChild<T>(ref T component, GameObject gameObjParent) where T : Component
        {
            if (component != null) return component;

            component = gameObjParent.GetComponentInChildren<T>();
            if(component != null) return component;

            Debug.LogError($"Component [{typeof(T).Name}] doesn't have in GameObject [{gameObjParent.name}] child");
            return null;
        }

        public static T LoadComponentInParent<T>(ref T component, GameObject gameObj) where T : Component
        {
            if (component != null) return component;

            Transform parent = gameObj.transform.parent;
            while (parent != null)
            {
                T foundComponent = parent.GetComponent<T>();
                if (foundComponent != null)
                {
                    component = foundComponent;
                    return component;
                }
                parent = parent.parent;
            }

            Debug.LogError($"Component [{typeof(T).Name}] not found in parents of GameObject [{gameObj.name}]");
            return null;
        }
    }
}
