using System.Collections;
using DJM.CoreUtilities.BaseClasses;
using UnityEngine;

namespace DJM.CoreUtilities
{
    internal sealed class DJMComponentContext : PersistantSingletonComponent<DJMComponentContext>
    {
        internal T InstantiateChild<T>(T prefab) where T : MonoBehaviour
        {
            return Instantiate(prefab, transform);
        }
        
        internal T InstantiateChild<T>() where T : MonoBehaviour
        {
            var obj = new GameObject(typeof(T).Name);
            return obj.AddComponent<T>();
        }
        
        internal void StartTheCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}