using System;
using UnityEngine;

namespace DJM.CoreServices.Bootstrap
{
    internal sealed class PersistantContextComponent : MonoBehaviour
    {
        private void Awake() => DontDestroyOnLoad(gameObject);

        internal Action OnDestroyContext;

        internal T InstantiatePrefabAsChild<T>(T prefab) where T : MonoBehaviour
        {
            return Instantiate(prefab, transform);
        }
        
        internal T AddComponentToNewChildGameObject<T>() where T : MonoBehaviour
        {
            var obj = new GameObject(typeof(T).Name)
            {
                transform = { parent = transform }
            };
            return obj.AddComponent<T>();
        }
        
        internal object AddComponentToNewChildGameObject(Type type)
        {
            var obj = new GameObject(type.Name)
            {
                transform = { parent = transform }
            };
            return obj.AddComponent(type);
        }
    }
}