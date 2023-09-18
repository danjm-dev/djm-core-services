using DJM.CoreServices.API;
using UnityEngine;

namespace DJM.CoreServices.Temp.Components.Audio
{
    internal sealed class PlayAudioEffectOnDestroy : MonoBehaviour
    {
        private ITransientSoundController _transientSoundController;
        
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private bool randomisePitch;
        private void Awake() => _transientSoundController = DJMPersistantServices.Resolve<ITransientSoundController>();

        private void OnDestroy()
        {
            if(randomisePitch) _transientSoundController.PlaySoundRandomPitch(audioClip);
            else _transientSoundController.PlaySound(audioClip);
        }
    }
}