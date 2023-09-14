using System.Collections;
using DJM.CoreUtilities.Components.MockMonoBehaviourComponents;
using DJM.CoreUtilities.ServiceContext;
using UnityEngine;

namespace DJM.CoreUtilities.Misc.BaseClasses
{
    public abstract class CoroutineHandler
    {
        private readonly CoroutineComponent _coroutineComponent = DJMServiceContext.Instance.GetMonoBehaviorService<CoroutineComponent>();


        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return _coroutineComponent.DelegateStartCoroutine(routine);
        }
        
        public void StopCoroutine(IEnumerator routine)
        {
            _coroutineComponent.DelegateStopCoroutine(routine);
        }
    }
}