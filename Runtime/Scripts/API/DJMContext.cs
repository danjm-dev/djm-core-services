using System.Collections;
using DJM.CoreUtilities.Configuration;
using UnityEngine;

namespace DJM.CoreUtilities
{
    public static class DJMContext
    {
        internal static T Add<T>(T prefab) where T : MonoBehaviour
        {
            return DJMContextComponent.Instance.InstantiatePrefabAsChild(prefab);
        }
        
        internal static T Add<T>() where T : MonoBehaviour
        {
            return DJMContextComponent.Instance.AddComponentToNewChildGameObject<T>();
        }
        
        public static void DelegateStartCoroutine(IEnumerator coroutine)
        {
            DJMContextComponent.Instance.StartCoroutine(coroutine);
        }
        
        public static void DelegateStopCoroutine(IEnumerator coroutine)
        {
            DJMContextComponent.Instance.StopCoroutine(coroutine);
        }
    }
}