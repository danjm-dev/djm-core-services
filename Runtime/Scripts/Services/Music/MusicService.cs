using System;
using DG.Tweening;
using DJM.CoreUtilities.MonoBehaviors.Audio;
using DJM.CoreUtilities.Services.Logger;
using UnityEngine;

namespace DJM.CoreUtilities.Services.Music
{
    public sealed class MusicService : IMusicService
    {
        private readonly AudioSourcePool _audioSourcePool;
        private readonly ILoggerService _loggerService;
        
        private AudioSource _audioSource;

        private Sequence _trackOperation;

        public float Volume { get; private set; }
        public bool IsPlaying => _audioSource is not null && _audioSource.isPlaying;
        
        public MusicService(AudioSourcePool audioSourcePool, ILoggerService loggerService)
        {
            _audioSourcePool = audioSourcePool 
                ? audioSourcePool 
                : throw new ArgumentException("AudioSource Pool cannot be null.", nameof(audioSourcePool));
            
            _loggerService = loggerService ?? throw new ArgumentException("AudioSource Pool cannot be null.", nameof(audioSourcePool));
            Volume = 1f;
        }
        
        public void SetMute(bool mute) => _audioSource.mute = mute;
        
        public void SetVolume(float volume)
        {
            Volume = volume;
            if(_audioSource is not null) _audioSource.volume = Volume;
        }

        public void PlayTrack(AudioClip track, float fadeOutDuration, float fadeInDuration)
        {
            if (track is null)
            {
                _loggerService.LogError($"Track can not be null, {nameof(track)}", nameof(MusicService));
                return;
            }
            
            // force complete existing music operations
            _trackOperation?.Complete();
            _trackOperation = DOTween.Sequence();

            if (_audioSource is null)
            {
                _audioSource = _audioSourcePool.GetAudioSource();
                _audioSource.loop = true;
            }
            else if (_audioSource.isPlaying)
            {
                if (fadeOutDuration > 0f) _trackOperation.Append(_audioSource.DOFade(0f, fadeOutDuration * Volume));
                _trackOperation.AppendCallback(() => _audioSource.Stop());
            }
            
            _trackOperation.AppendCallback(() =>
            {
                _audioSource.clip = track;
                _audioSource.volume = 0f;
                _audioSource.Play();
            });
            
            if (fadeInDuration > 0f) _trackOperation.Append(_audioSource.DOFade(Volume, fadeInDuration * Volume));
            else _trackOperation.AppendCallback(() => _audioSource.volume = Volume);
            
            _trackOperation.AppendCallback(() => _trackOperation = null);
        }
        
        public void StopTrack(float fadeOutDuration)
        {
            // force complete existing music operations
            _trackOperation?.Complete();
            
            if(_audioSource is null) return;
            
            // source not playing - release
            if (!_audioSource.isPlaying)
            {
                ReleaseAudioSource();
                return;
            }
            
            // source playing, but no fade duration - release
            if (fadeOutDuration <= 0f)
            {
                _audioSource.Stop();
                ReleaseAudioSource();
                return;
            }

            // fade out source then release
            _trackOperation = DOTween.Sequence();
            _trackOperation.Append(_audioSource.DOFade(0f, fadeOutDuration * Volume));
            _trackOperation.AppendCallback(() =>
            {
                _audioSource.Stop();
                ReleaseAudioSource();
                _trackOperation = null;
            });
        }
        
        public void PauseTrack(float fadeOutDuration)
        {
            // force complete existing music operations
            _trackOperation?.Complete();
            
            if(_audioSource is null || !_audioSource.isPlaying) return;
            
            // no fade duration, immediate pause
            if (fadeOutDuration <= 0f)
            {
                _audioSource.Pause();
                return;
            }

            _trackOperation = DOTween.Sequence();
                
            _trackOperation.Append(_audioSource.DOFade(0f, fadeOutDuration * Volume));
            _trackOperation.AppendCallback(() =>
            {
                _audioSource.Pause();
                _audioSource.volume = Volume;
                _trackOperation = null;
            });
        }
        
        public void ResumeTrack(float fadeInDuration)
        {
            // force complete existing music operations
            _trackOperation?.Complete();
            
            if(_audioSource is null || _audioSource.isPlaying) return;
            
            // no fade duration - resume immediately
            if (fadeInDuration <= 0f)
            {
                _audioSource.UnPause();
                return;
            }
            
            _trackOperation = DOTween.Sequence();

            _trackOperation.AppendCallback(() =>
            {
                _audioSource.volume = 0f;
                _audioSource.UnPause();
            });
            _trackOperation.Append(_audioSource.DOFade(Volume, fadeInDuration * Volume));
            _trackOperation.AppendCallback(() => _trackOperation = null);
        }
        
        
        private void ReleaseAudioSource()
        {
            _audioSourcePool.ReleaseAudioSource(_audioSource);
            _audioSource = null;
        }
    }
}