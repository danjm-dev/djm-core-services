using DJM.CoreServices.PersistantServices;
using UnityEngine;

namespace DJM.CoreServices.Temp.Components.Audio
{
    public sealed class PlayAudioEffectOnStart : MonoBehaviour
    {
        private ITransientSoundService _transientSoundService;
        
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private bool randomisePitch;
        
        private void Awake() => _transientSoundService = DJMServiceLocator.Resolve<ITransientSoundService>();
        private void Start()
        {
            if(randomisePitch) _transientSoundService.PlaySoundRandomPitch(audioClip);
            else _transientSoundService.PlaySound(audioClip);
            Destroy(this);
        }
    }
}