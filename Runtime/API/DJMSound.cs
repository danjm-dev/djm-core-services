using DJM.CoreServices.Bootstrap;
using DJM.CoreServices.Services.SoundController;
using UnityEngine;

namespace DJM.CoreServices.API
{
    /// <summary>
    /// Static utility class for playing and controlling transient sound effects.
    /// </summary>
    public static class DJMSound
    {
        private const float MinimumPitchValue = 0.0001f;
        private const float MaximumPitchValue = 3f;
        
        private static readonly IEventManager EventManager = DJMServiceContext.Instance.EventManager;
        
        private static float _defaultRandomPitchMinimum = 0.95f;
        private static float _defaultRandomPitchMaximum = 1.05f;
        private static float DefaultRandomPitch => Random.Range(_defaultRandomPitchMinimum, _defaultRandomPitchMaximum);
        
        
        public static void SetDefaultRandomPitchRange(float minimumPitch, float maximumPitch)
        {
            _defaultRandomPitchMinimum = Mathf.Clamp(minimumPitch, MinimumPitchValue, MaximumPitchValue);
            _defaultRandomPitchMaximum = Mathf.Clamp(maximumPitch, MinimumPitchValue, MaximumPitchValue);
        }
        
        public static void Mute()
        {
            EventManager.TriggerEvent(new SoundControllerEvent.SetMute(true));
        }
        
        public static void UnMute()
        {
            EventManager.TriggerEvent(new SoundControllerEvent.SetMute(false));
        }
        
        public static void SetVolume(float volume)
        {
            EventManager.TriggerEvent(new SoundControllerEvent.SetVolume(Mathf.Clamp01(volume)));
        }
        
        public static void PlayTransientSound(AudioClip transientSound, float volumeScale = 1f, float pitch = 1f)
        {
            TriggerPlayTransientSoundEvent(transientSound, volumeScale, pitch);
        }
        
        public static void PlayTransientSoundRandomPitch(AudioClip transientSound, float volumeScale = 1f)
        {
            TriggerPlayTransientSoundEvent(transientSound, volumeScale, DefaultRandomPitch);
        }
        
        public static void PlayTransientSoundRandomPitch(AudioClip transientSound, float minPitch, float maxPitch, float volumeScale = 1f)
        {
            var pitch = Random.Range
            (
                Mathf.Clamp(minPitch, MinimumPitchValue, MaximumPitchValue), 
                Mathf.Clamp(maxPitch, MinimumPitchValue, MaximumPitchValue)
            );
            
            TriggerPlayTransientSoundEvent(transientSound, volumeScale, pitch);
        }

        private static void TriggerPlayTransientSoundEvent(AudioClip transientSound, float volumeScale, float pitch)
        {
            if (transientSound is null)
            {
                DJMLogger.LogError("Attempted to play null audio clip", nameof(DJMSound));
                return;
            }

            EventManager.TriggerEvent
            (
                new SoundControllerEvent.PlayTransientSound(transientSound, Mathf.Clamp01(volumeScale), pitch)
            );
        }
    }
}