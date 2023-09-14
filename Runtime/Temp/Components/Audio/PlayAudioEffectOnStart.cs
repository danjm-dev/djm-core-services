using DJM.CoreServices.API;
using UnityEngine;

namespace DJM.CoreServices.Temp.Components.Audio
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