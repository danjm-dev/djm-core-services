using DJM.CoreUtilities.BaseClasses;
using UnityEngine;

namespace DJM.CoreUtilities.Audio
{
    internal sealed class AudioManager : SingletonComponent<AudioManager>
    {
        private AudioSource _musicSource;
        private AudioSource _effectsSource;

        protected override void Awake()
        {
            base.Awake();
            _musicSource = gameObject.AddComponent<AudioSource>();
            _effectsSource = gameObject.AddComponent<AudioSource>();
        }

        private void Start()
        {
            DJMEvents.Subscribe<AudioEffectEvents.Mute>(MuteEffects);
            DJMEvents.Subscribe<AudioEffectEvents.SetVolume>(SetEffectVolume);
            DJMEvents.Subscribe<AudioEffectEvents.PlayClip>(PlayOneShotEffect);
        }

        private void OnDestroy()
        {
            DJMEvents.Unsubscribe<AudioEffectEvents.Mute>(MuteEffects);
            DJMEvents.Unsubscribe<AudioEffectEvents.SetVolume>(SetEffectVolume);
            DJMEvents.Unsubscribe<AudioEffectEvents.PlayClip>(PlayOneShotEffect);
        }


        // effects
        private void MuteEffects(AudioEffectEvents.Mute  muteEvent) => _effectsSource.mute = true;
        private void SetEffectVolume(AudioEffectEvents.SetVolume volumeEvent) => _effectsSource.volume = volumeEvent.Volume;
        
        private void PlayOneShotEffect(AudioEffectEvents.PlayClip playClipEvent)
        {
            _effectsSource.PlayOneShot(playClipEvent.AudioClip);
        }
    }
}