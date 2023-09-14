using System.Collections;
using UnityEngine;

namespace DJM.CoreUtilities.Components.MockMonoBehaviourComponents
{
    internal sealed class CoroutineComponent : MonoBehaviour
    {
        internal Coroutine DelegateStartCoroutine(IEnumerator routine) => StartCoroutine(routine);
        internal void DelegateStopCoroutine(IEnumerator routine) => StopCoroutine(routine);
    }
}