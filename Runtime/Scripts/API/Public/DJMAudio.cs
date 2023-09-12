using DJM.CoreUtilities.Audio;
using UnityEngine;

namespace DJM.CoreUtilities
{
    public static class DJMAudio
    {
        public static void PlayAudioEffect(AudioClip audioClip)
        {
            DJMEvents.TriggerEvent(new AudioEffectEvents.PlayClip(audioClip));
        }
        
        public static void MuteAudioEffects()
        {
            DJMEvents.TriggerEvent(new AudioEffectEvents.SetMute());
        }
        
        public static void SetAudioEffectVolume(float volume)
        {
            DJMEvents.TriggerEvent(new AudioEffectEvents.SetVolume(volume));
        }
        
        
        
        
        
        
        private static class Music
        {
            public static float DefaultTrackFadeInDuration = 0.5f;
            public static float DefaultTrackFadeOutDuration = 0.5f;
            
            public static void Mute()
            {
            }
            
            public static void UnMute()
            {
            }
            
            public static void SetVolume(float volume)
            {
            }
            
            public static void Pause(float fadeOutDuration = 0f)
            {
            }
            
            public static void Play(float fadeInDuration = 0f)
            {
            }
            
            public static void SetTrack(AudioClip track)
            {
            }
            
            public static void SetTrack(AudioClip track, float fadeOutDuration, float fadeInDuration, bool loop = true)
            {
            }
        }
        
        private static class Effects
        {
            public static float DefaultMinRandomPitch = 0.9f;
            public static float DefaultMaxRandomPitch = 1.1f;
            
            public static void Mute()
            {
            }
            
            public static void SetVolume(float volume)
            {
            }
            
            public static void PlayClip(AudioClip audioClip)
            {
            }
            
            public static void PlayClip(AudioClip audioClip, float pitch)
            {
            }
            
            public static void PlayClipRandomPitch(AudioClip audioClip)
            {
            }
            
            public static void PlayClipRandomPitch(AudioClip audioClip, float minPitch, float maxPitch)
            {
            }
        }
        
        
    }
}