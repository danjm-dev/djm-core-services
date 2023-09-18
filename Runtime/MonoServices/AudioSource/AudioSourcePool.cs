using System.Collections.Generic;
using DJM.DependencyInjection;
using UnityEngine;

namespace DJM.CoreServices.MonoServices.AudioSource
{
    public sealed class AudioSourcePool : MonoBehaviour
    {
        private ILoggerService _loggerService;
        
        [Min(1)]public int initialPoolSize = 10;
        [Min(1)]public int maxPoolSize = 25;

        private readonly List<UnityEngine.AudioSource> _audioSources = new();
        private readonly Stack<UnityEngine.AudioSource> _availableAudioSources = new();

        [Inject]
        private void Construct(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }
        
        private void Start()
        {
            for (var i = 0; i < initialPoolSize; i++)
            {
                var audioSource = AddAudioSource();
                _availableAudioSources.Push(audioSource);
            }
        }

        private UnityEngine.AudioSource AddAudioSource(bool disable = true)
        {
            var newAudioSource = gameObject.AddComponent<UnityEngine.AudioSource>();
            _audioSources.Add(newAudioSource);
            newAudioSource.playOnAwake = false;
            if(disable) newAudioSource.enabled = false;
            return newAudioSource;
        }

        public UnityEngine.AudioSource GetAudioSource()
        {
            // return audio source from pool if available
            if (_availableAudioSources.Count > 0)
            {
                var audioSource = _availableAudioSources.Pop();
                audioSource.enabled = true;
                return audioSource;
            }
            
            // pool empty - create new one
            var newAudioSource = AddAudioSource(false);
            
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (_audioSources.Count > maxPoolSize) LogExceededMaxPoolSize(_audioSources.Count);
#endif          
            return newAudioSource;
        }

        public void ReleaseAudioSource(UnityEngine.AudioSource audioSource)
        {
            if (audioSource.transform != transform)
            {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                LogTriedToReleaseForeignAudioSource();
#endif
                return;
            }
            
            if (_availableAudioSources.Count < maxPoolSize)
            {
                ResetAudioSource(audioSource);
                audioSource.enabled = false;
                _availableAudioSources.Push(audioSource);
                return;
            }

            _audioSources.Remove(audioSource);
            Destroy(audioSource);
            
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            LogDestroyedExcessAudioSource();
#endif
        }

        private static void ResetAudioSource(UnityEngine.AudioSource audioSource)
        {
            audioSource.Stop();
            
            audioSource.clip = null;
            audioSource.volume = 1f;
            audioSource.pitch = 1f;
            audioSource.loop = false;
            audioSource.mute = false;
            audioSource.spatialBlend = 0f;
            audioSource.priority = 128;
            audioSource.playOnAwake = false;
        }


        // logging

        private void LogExceededMaxPoolSize(int currentPoolSize)
        {
            _loggerService.LogWarning($"Exceeding max pool size, max: {maxPoolSize}, current: {currentPoolSize} ", nameof(AudioSourcePool));
        }
        
        private void LogTriedToReleaseForeignAudioSource()
        {
            _loggerService.LogError("Tried to release audio source from another game object.", nameof(AudioSourcePool));
        }
        
        private void LogDestroyedExcessAudioSource()
        {
            _loggerService.LogInfo("Destroyed excess Audio Source.", nameof(AudioSourcePool));
        }
    }
}