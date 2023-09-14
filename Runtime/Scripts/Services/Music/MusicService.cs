using System;
using DG.Tweening;
using DJM.CoreUtilities.MonoBehaviors.Audio;
using UnityEngine;

namespace DJM.CoreUtilities.Services.Music
{
    /// <summary>
    /// Handles various audio events and transitions for music tracks.
    /// </summary>
    internal sealed class MusicService : IMusicService
    {
        private readonly AudioSourcePool _audioSourcePool;
        private readonly AudioSource _audioSource;

        private Sequence _trackOperation;

        internal float Volume => _audioSource.volume;
        internal bool IsPaused { get; private set; }
        
        internal MusicService(AudioSourcePool audioSourcePool)
        {
            _audioSourcePool = audioSourcePool 
                ? audioSourcePool 
                : throw new ArgumentException("AudioSource Pool cannot be null.", nameof(audioSourcePool));
            
            _audioSource = _audioSourcePool.GetAudioSource();
            _audioSource.loop = true;
        }
        
        public void SetMute(bool mute) => _audioSource.mute = mute;
        
        public void SetVolume(float volume) => _audioSource.volume = volume;
        
        public void PlayTrack(AudioClip track, float fadeOutDuration, float fadeInDuration)
        {
            if (track is null)
            {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                DJMLogger.LogError("Attempted to play null track.", nameof(MusicService));
#endif
                return;
            }
            
            // force complete existing music operations
            _trackOperation?.Complete();

            // no fading - immediately stop track and start new one
            if (fadeOutDuration <= 0f && fadeInDuration <= 0f)
            {
                _audioSource.Stop();
                _audioSource.clip = track;
                _audioSource.Play();
                return;
            }
            
            _trackOperation = DOTween.Sequence();
            
            // set volume to 0
            if (fadeOutDuration > 0f) _trackOperation.Append(_audioSource.DOFade(0f, fadeOutDuration * Volume));
            else _trackOperation.AppendCallback(() => _audioSource.volume = 0f);
            
            // stop current track, set new track, play new track
            _trackOperation.AppendCallback(() =>
            {
                _audioSource.Stop();
                
                //_audioSource.loop = true;
                _audioSource.clip = track;
                
                _audioSource.Play();
            });
            
            // set volume to original value
            if (fadeInDuration > 0f) _trackOperation.Append(_audioSource.DOFade(Volume, fadeInDuration * Volume));
            else _trackOperation.AppendCallback(() => _audioSource.volume = Volume);
            
            // dispose of sequence
            _trackOperation.AppendCallback(() => _trackOperation = null);
        }
        
        public void StopTrack(float fadeOutDuration)
        {
            // force complete existing music operations
            _trackOperation?.Complete();
            
            if(!_audioSource.isPlaying) return;
            
            // no fade duration, immediate stop
            if (fadeOutDuration <= 0f)
            {
                _audioSource.Stop();
                return;
            }

            _trackOperation = DOTween.Sequence();
                
            _trackOperation.Append(_audioSource.DOFade(0f, fadeOutDuration * Volume));
            _trackOperation.AppendCallback(() =>
            {
                _audioSource.Stop();
                _audioSource.volume = Volume;
                _trackOperation = null;
            });
        }
        
        public void PauseTrack(float fadeOutDuration)
        {
            // force complete existing music operations
            _trackOperation?.Complete();
            
            if(!_audioSource.isPlaying) return;
            
            // no fade duration, immediate pause
            if (fadeOutDuration <= 0f)
            {
                _audioSource.Pause();
                IsPaused = true;
                return;
            }

            _trackOperation = DOTween.Sequence();
                
            _trackOperation.Append(_audioSource.DOFade(0f, fadeOutDuration * Volume));
            _trackOperation.AppendCallback(() =>
            {
                _audioSource.Pause();
                IsPaused = true;
                _audioSource.volume = Volume;
                _trackOperation = null;
            });
        }
        
        public void ResumeTrack(float fadeInDuration)
        {
            // force complete existing music operations
            _trackOperation?.Complete();
            
            if(_audioSource.isPlaying || !IsPaused) return;
            
            // no fade duration - resume immediately
            if (fadeInDuration <= 0f)
            {
                _audioSource.UnPause();
                IsPaused = false;
                return;
            }
            
            _trackOperation = DOTween.Sequence();
                
            _trackOperation.Append(_audioSource.DOFade(Volume, fadeInDuration * Volume));
            _trackOperation.AppendCallback(() =>
            {
                _audioSource.UnPause();
                IsPaused = false;
                _audioSource.volume = Volume;
                _trackOperation = null;
            });
        }
    }
}