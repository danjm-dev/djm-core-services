using DJM.CoreUtilities.API;
using UnityEngine;

namespace DJM.CoreUtilities.Components.Audio
{
    public sealed class PlayAudioEffectOnStart : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        private void Start()
        {
            DJMSound.PlayTransientSound(audioClip);
            Destroy(this);
        }
    }
}