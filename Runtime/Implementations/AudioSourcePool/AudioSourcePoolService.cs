using System.Collections.Generic;
using DJM.DependencyInjection;
using UnityEngine;

namespace DJM.CoreServices.AudioSourcePool
{
    /// <summary>
    /// Manages a pool of audio sources. If manually instantiating, ensure Construct method is called before use.
    /// </summary>
    public sealed class AudioSourcePoolService : MonoBehaviour, IAudioSourcePool
    {
        private IDebugLogger _debugLogger;
        
        [Min(1)]public int initialPoolSize = 10;
        [Min(1)]public int maxPoolSize = 25;

        private readonly List<AudioSource> _audioSources = new();
        private readonly Stack<AudioSource> _availableAudioSources = new();

        /// <summary>
        /// Dependency injection via method, as <see cref="AudioSourcePoolService"/> can not have a constructor.
        /// If manually instantiated, this must be called before use to prevent exceptions.
        /// </summary>
        /// <param name="debugLogger">The debug logger for logging diagnostic information.</param>
        [Inject]
        public void Construct(IDebugLogger debugLogger)
        {
            _debugLogger = debugLogger;
        }
        
        private void Start()
        {
            for (var i = 0; i < initialPoolSize; i++)
            {
                var audioSource = AddAudioSource();
                _availableAudioSources.Push(audioSource);
            }
        }
        
        /// <inheritdoc/>
        public AudioSource GetAudioSource()
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
            
            if (_audioSources.Count > maxPoolSize) LogExceededMaxPoolSize(_audioSources.Count);
            return newAudioSource;
        }

        /// <inheritdoc/>
        public void ReleaseAudioSource(AudioSource audioSource)
        {
            if (audioSource.transform != transform)
            {
                LogTriedToReleaseForeignAudioSource();
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
            
            LogDestroyedExcessAudioSource();
        }
        
        private AudioSource AddAudioSource(bool disable = true)
        {
            var newAudioSource = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(newAudioSource);
            newAudioSource.playOnAwake = false;
            if(disable) newAudioSource.enabled = false;
            return newAudioSource;
        }

        private static void ResetAudioSource(AudioSource audioSource)
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
            _debugLogger.LogWarning($"Exceeding max pool size, max: {maxPoolSize}, current: {currentPoolSize} ", nameof(AudioSourcePoolService));
        }
        
        private void LogTriedToReleaseForeignAudioSource()
        {
            _debugLogger.LogError("Tried to release audio source from another game object.", nameof(AudioSourcePoolService));
        }
        
        private void LogDestroyedExcessAudioSource()
        {
            _debugLogger.LogInfo("Destroyed excess Audio Source.", nameof(AudioSourcePoolService));
        }
    }
}