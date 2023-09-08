using DJM.CoreUtilities.BaseClasses;
using DJM.CoreUtilities.Components.BaseClasses;
using UnityEngine;

namespace DJM.CoreUtilities.Configuration
{
    internal sealed class DJMSceneContextComponent : PersistantSingletonComponent<DJMSceneContextComponent>
    {
        internal T InstantiatePrefabAsChild<T>(T prefab) where T : MonoBehaviour
        {
            return Instantiate(prefab, transform);
        }
        
        internal T CreateComponentOnNewGameObjectAsChild<T>() where T : MonoBehaviour
        {
            var obj = new GameObject(typeof(T).Name)
            {
                transform =
                {
                    parent = transform
                }
            };
            return obj.AddComponent<T>();
        }
    }
}