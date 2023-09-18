using DJM.CoreServices.API;
using UnityEngine;

namespace DJM.CoreServices.Temp.Components.Audio
{
    public sealed class PlayAudioEffectOnStart : MonoBehaviour
    {
        private ITransientSoundController _transientSoundController;
        
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private bool randomisePitch;
        
        private void Awake() => _transientSoundController = DJMPersistantServices.Resolve<ITransientSoundController>();
        private void Start()
        {
            if(randomisePitch) _transientSoundController.PlaySoundRandomPitch(audioClip);
            else _transientSoundController.PlaySound(audioClip);
            Destroy(this);
        }
    }
}