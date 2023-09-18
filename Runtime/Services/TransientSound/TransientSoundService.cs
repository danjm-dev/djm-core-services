using System;
using System.Collections.Generic;
using DG.Tweening;
using DJM.CoreServices.MonoServices.AudioSourcePool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DJM.CoreServices.Services.TransientSound
{
    public sealed class TransientSoundService : ITransientSoundService
    {
        private const float MinimumPitchValue = 0.0001f;
        private const float MaximumPitchValue = 3f;
        
        private readonly IAudioSourcePool _audioSourcePool;
        private readonly IDebugLogger _debugLogger;
        
        private readonly AudioSource _primaryAudioSource;
        private AudioSource _backupAudioSource;
        
        private readonly List<AudioSource> _activeBackupAudioSources;

        public bool IsMuted => _primaryAudioSource.mute;
        public float Volume => _primaryAudioSource.volume;

        public TransientSoundService(IAudioSourcePool audioSourcePool, IDebugLogger debugLogger)
        {
            _audioSourcePool = audioSourcePool ?? throw new ArgumentException($"{nameof(audioSourcePool)} can not be null.");
            _debugLogger = debugLogger ?? throw new ArgumentException($"{nameof(debugLogger)} can not be null.");
            
            _primaryAudioSource = _audioSourcePool.GetAudioSource();
            _backupAudioSource = audioSourcePool.GetAudioSource();
            _activeBackupAudioSources = new List<AudioSource>();
        }

        public void Mute() => SetMute(true);
        
        public void UnMute() => SetMute(false);
        
        public void SetVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            
            _primaryAudioSource.volume = volume;
            _backupAudioSource.volume = volume;
            foreach (var audioSource in _activeBackupAudioSources) audioSource.volume = volume;
        }

        public void PlaySound(AudioClip sound, float volumeScale, float pitch)
        {
            PlayTransientSound(sound, volumeScale, pitch);
        }

        public void PlaySoundRandomPitch(AudioClip sound, float minPitch, float maxPitch, float volumeScale)
        {

            var min = Mathf.Clamp(minPitch, MinimumPitchValue, MaximumPitchValue);
            var max = Mathf.Clamp(maxPitch, MinimumPitchValue, MaximumPitchValue);

            if (min > max)
            {
                (min, max) = (max, min);
            }
            
            PlayTransientSound(sound, volumeScale, Random.Range(min, max));
        }


        private void SetMute(bool mute)
        {
            _primaryAudioSource.mute = mute;
            _backupAudioSource.mute = mute;
            foreach (var audioSource in _activeBackupAudioSources) audioSource.mute = mute;
        }
        
        private void PlayTransientSound(AudioClip sound, float volumeScale, float pitch)
        {
            if (sound is null)
            {
                _debugLogger.LogError("Attempted to play null audio clip", nameof(TransientSoundService));
                return;
            }
            
            volumeScale = Mathf.Clamp01(volumeScale);
            
            if(Mathf.Approximately(1f, pitch))
                _primaryAudioSource.PlayOneShot(sound, volumeScale);
            else PlayPitchedTransientSound(sound, volumeScale, pitch);
        }

        private void PlayPitchedTransientSound(AudioClip transientSound, float volumeScale, float pitch)
        {
            var audioSource = _backupAudioSource;
            
            audioSource.pitch = pitch;
            _activeBackupAudioSources.Add(audioSource);
            
            var seq = DOTween.Sequence();
            seq.AppendCallback(() => audioSource.PlayOneShot(transientSound, volumeScale));
            seq.AppendInterval(transientSound.length / pitch);
            seq.AppendCallback(() =>
            {
                _activeBackupAudioSources.Remove(audioSource);
                audioSource.pitch = 1f;
                _audioSourcePool.ReleaseAudioSource(audioSource);
            });

            var newBackupAudioSource = _audioSourcePool.GetAudioSource();
            newBackupAudioSource.mute = _primaryAudioSource.mute;
            newBackupAudioSource.volume = _primaryAudioSource.volume;
            _backupAudioSource = newBackupAudioSource;
        }
    }
}