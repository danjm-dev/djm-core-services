using DJM.CoreUtilities.API;
using UnityEngine;

namespace DJM.CoreUtilities.Components.Audio
{
    internal sealed class PlayAudioEffectOnDestroy : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        private void OnDestroy()
        {
            DJMSound.PlayTransientSound(audioClip);
            Destroy(this);
        }
    }
}