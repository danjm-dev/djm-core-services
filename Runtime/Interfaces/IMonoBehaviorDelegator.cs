using System.Collections;
using UnityEngine;

namespace DJM.CoreServices
{
    public interface IMonoBehaviorDelegator
    {
        public Coroutine DelegateStartCoroutine(IEnumerator routine);
        public void DelegateStopCoroutine(IEnumerator routine);
    }
}