using System.Collections;
using UnityEngine;

namespace DJM.CoreUtilities
{
    public static class DJMContext
    {
        public static T Add<T>(T prefab) where T : MonoBehaviour
        {
            return DJMPersistentComponentContext.Instance.InstantiatePrefabAsChild(prefab);
        }
        
        public static T Add<T>() where T : MonoBehaviour
        {
            return DJMPersistentComponentContext.Instance.CreateComponentOnNewGameObjectAsChild<T>();
        }
        
        public static void DelegateStartCoroutine(IEnumerator coroutine)
        {
            DJMPersistentComponentContext.Instance.StartCoroutine(coroutine);
        }
        
        public static void DelegateStopCoroutine(IEnumerator coroutine)
        {
            DJMPersistentComponentContext.Instance.StopCoroutine(coroutine);
        }
    }
}