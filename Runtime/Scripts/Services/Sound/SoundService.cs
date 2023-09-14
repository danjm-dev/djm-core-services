using System;
using System.Collections.Generic;
using DG.Tweening;
using DJM.CoreUtilities.MonoBehaviors.Audio;
using UnityEngine;

namespace DJM.CoreUtilities.Services.Sound
{
    internal sealed class SoundService : ISoundService
    {
        private readonly AudioSourcePool _audioSourcePool;
        
        private readonly AudioSource _primaryAudioSource;
        private AudioSource _backupAudioSource;
        
        private readonly List<AudioSource> _activeBackupAudioSources;

        internal SoundService(AudioSourcePool audioSourcePool)
        {
            _audioSourcePool = audioSourcePool 
                ? audioSourcePool 
                : throw new ArgumentException("AudioSource Pool cannot be null.", nameof(audioSourcePool));
            
            _primaryAudioSource = _audioSourcePool.GetAudioSource();
            _backupAudioSource = audioSourcePool.GetAudioSource();
            _activeBackupAudioSources = new List<AudioSource>();
        }
        
        public void SetMute(bool mute)
        {
            _primaryAudioSource.mute = mute;
            _backupAudioSource.mute = mute;
            foreach (var audioSource in _activeBackupAudioSources) audioSource.mute = mute;
        }

        public void SetVolume(float volume)
        {
            _primaryAudioSource.volume = volume;
            _backupAudioSource.volume = volume;
            foreach (var audioSource in _activeBackupAudioSources) audioSource.volume = volume;
        }

        public void PlayTransientSound(AudioClip transientSound, float volumeScale, float pitch)
        {
            if(Mathf.Approximately(1f, pitch))
                _primaryAudioSource.PlayOneShot(transientSound, volumeScale);
            else PlayPitchedTransientSound(transientSound, volumeScale, pitch);
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