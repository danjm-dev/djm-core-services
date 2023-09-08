using System.Collections;
using DJM.CoreUtilities.Configuration;
using UnityEngine;

namespace DJM.CoreUtilities
{
    public static class DJMSceneContext
    {
        public static T Add<T>(T prefab) where T : MonoBehaviour
        {
            return DJMSceneContextComponent.Instance.InstantiatePrefabAsChild(prefab);
        }
        
        public static T Add<T>() where T : MonoBehaviour
        {
            return DJMSceneContextComponent.Instance.CreateComponentOnNewGameObjectAsChild<T>();
        }
        
        public static void DelegateStartCoroutine(IEnumerator coroutine)
        {
            DJMSceneContextComponent.Instance.StartCoroutine(coroutine);
        }
        
        public static void DelegateStopCoroutine(IEnumerator coroutine)
        {
            DJMSceneContextComponent.Instance.StopCoroutine(coroutine);
        }
    }
}