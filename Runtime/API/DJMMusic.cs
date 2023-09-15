using DJM.CoreServices.Bootstrap;
using DJM.CoreServices.Services.MusicController;
using UnityEngine;

namespace DJM.CoreServices.API
{
    public static class DJMMusic
    {
        private static readonly IEventManager EventManager = DJMServiceContext.Instance.EventManager;
            
        public static void Mute()
        {
            EventManager.TriggerEvent(new MusicControllerEvent.SetMute(true));
        }
        
        public static void UnMute()
        {
            EventManager.TriggerEvent(new MusicControllerEvent.SetMute(false));
        }
        
        public static void SetVolume(float volume)
        {
            EventManager.TriggerEvent(new MusicControllerEvent.SetVolume(Mathf.Clamp01(volume)));
        }
        
        public static void PlayTrack(AudioClip track, float fadeOutDuration = 0f, float fadeInDuration = 0f)
        {
            EventManager.TriggerEvent(new MusicControllerEvent.PlayTrack(track, fadeOutDuration, fadeInDuration));
        }
            
        public static void StopTrack(float fadeOutDuration = 0f)
        {
            EventManager.TriggerEvent(new MusicControllerEvent.StopTrack(fadeOutDuration));
        }
        
        public static void PauseTrack(float fadeOutDuration = 0f)
        {
            EventManager.TriggerEvent(new MusicControllerEvent.PauseTrack(fadeOutDuration));
        }
            
        public static void ResumeTrack(float fadeInDuration = 0f)
        {
            EventManager.TriggerEvent(new MusicControllerEvent.ResumeTrack(fadeInDuration));
        }
    }
}