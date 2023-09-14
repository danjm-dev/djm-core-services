using DJM.CoreServices.API;
using UnityEngine;

namespace DJM.CoreServices.Temp.Components.Audio
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