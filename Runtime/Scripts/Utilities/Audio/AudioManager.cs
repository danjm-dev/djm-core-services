using System;
using DG.Tweening;
using DJM.CoreUtilities.BaseClasses;
using UnityEngine;

namespace DJM.CoreUtilities.Audio
{
    internal sealed class AudioManager : SingletonComponent<AudioManager>
    {
        private AudioSource _effectsSource;

        
        
        private MusicEventHandler _musicEventHandler;


        public float FXVolume => _effectsSource.volume;

        protected override void Awake()
        {
            base.Awake();
            var musicAudioSource = gameObject.AddComponent<AudioSource>();
            _effectsSource = gameObject.AddComponent<AudioSource>();
            

        }

        private void Start()
        {

            
            DJMEvents.Subscribe<AudioEffectEvents.SetMute>(MuteEffects);
            DJMEvents.Subscribe<AudioEffectEvents.SetVolume>(SetEffectVolume);
            DJMEvents.Subscribe<AudioEffectEvents.PlayClip>(PlayOneShotEffect);
        }

        private void OnDestroy()
        {
            DJMEvents.Unsubscribe<AudioEffectEvents.SetMute>(MuteEffects);
            DJMEvents.Unsubscribe<AudioEffectEvents.SetVolume>(SetEffectVolume);
            DJMEvents.Unsubscribe<AudioEffectEvents.PlayClip>(PlayOneShotEffect);
        }
        
        // music event handlers

        private void OnSetMusicTrack(MusicEvents.SetTrack setTrackEvent)
        {
            
        }
        
        // private void OnPlayMusicEvent(MusicEvents.Play playEvent) => StartMusicTrack();
        // private void OnStopMusicEvent(MusicEvents.Stop stopEvent) => StopMusicTrack(() => { });
        //
        // private void MuteMusic(MusicEvents.SetMute  muteEvent) => _musicSource.mute = muteEvent.Mute;
        // private void SetMusicVolume(MusicEvents.SetVolume volumeEvent) => _musicSource.volume = volumeEvent.Volume;


        // fx event handlers
        private void MuteEffects(AudioEffectEvents.SetMute  muteEvent) => _effectsSource.mute = muteEvent.Mute;
        
        private void SetEffectVolume(AudioEffectEvents.SetVolume volumeEvent) => _effectsSource.volume = volumeEvent.Volume;
        
        private void PlayOneShotEffect(AudioEffectEvents.PlayClip playClipEvent)
        {
            _effectsSource.PlayOneShot(playClipEvent.AudioClip);
        }
        
        // music

        

    }
}