using DJM.CoreUtilities.Components.BaseClasses;
using UnityEngine;

namespace DJM.CoreUtilities.Configuration
{
    internal sealed class DJMContextComponent : PersistantSingletonComponent<DJMContextComponent>
    {
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
    }
}