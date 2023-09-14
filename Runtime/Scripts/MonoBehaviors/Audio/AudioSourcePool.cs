using System.Collections.Generic;
using UnityEngine;

namespace DJM.CoreUtilities.MonoBehaviors.Audio
{
    public sealed class AudioSourcePool : MonoBehaviour
    {
        [Min(1)]public int initialPoolSize = 10;
        [Min(1)]public int maxPoolSize = 25;

        private readonly List<AudioSource> _audioSources = new();
        private readonly Stack<AudioSource> _availableAudioSources = new();

        private void Awake()
        {
            for (var i = 0; i < initialPoolSize; i++)
            {
                var audioSource = AddAudioSource();
                _availableAudioSources.Push(audioSource);
            }
        }

        private AudioSource AddAudioSource(bool disable = true)
        {
            var newAudioSource = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(newAudioSource);
            newAudioSource.playOnAwake = false;
            if(disable) newAudioSource.enabled = false;
            return newAudioSource;
        }

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
            
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (_audioSources.Count > maxPoolSize) LogExceededMaxPoolSize(maxPoolSize, _audioSources.Count);
#endif          
            return newAudioSource;
        }

        public void ReleaseAudioSource(AudioSource audioSource)
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

        private static void LogExceededMaxPoolSize(int maxPoolSize, int currentPoolSize)
        {
            DJMLogger.LogWarning($"Exceeding max pool size, max: {maxPoolSize}, current: {currentPoolSize} ", nameof(AudioSourcePool));
        }
        
        private static void LogTriedToReleaseForeignAudioSource()
        {
            DJMLogger.LogError("Tried to release audio source from another game object.", nameof(AudioSourcePool));
        }
        
        private static void LogDestroyedExcessAudioSource()
        {
            DJMLogger.LogInfo("Destroyed excess Audio Source.", nameof(AudioSourcePool));
        }
    }
}