using System.Collections;
using UnityEngine;

namespace DJM.CoreServices.MonoBehaviorDelegator
{
    internal sealed class MonoBehaviorDelegatorService : MonoBehaviour, IMonoBehaviorDelegator
    {
        public Coroutine DelegateStartCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }

        public void DelegateStopCoroutine(IEnumerator routine)
        {
            StopCoroutine(routine);
        }
    }
}