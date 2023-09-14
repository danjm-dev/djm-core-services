using DJM.CoreUtilities.ServiceContext;
using DJM.CoreUtilities.Services.Events;
using DJM.CoreUtilities.Services.Music;
using UnityEngine;

namespace DJM.CoreUtilities
{
    public static class DJMMusic
    {
        private static readonly IEventManagerService EventManagerService = DJMServiceContext.Instance.EventManagerService;
            
        public static void Mute()
        {
            EventManagerService.TriggerEvent(new MusicEvent.SetMute(true));
        }
        
        public static void UnMute()
        {
            EventManagerService.TriggerEvent(new MusicEvent.SetMute(false));
        }
        
        public static void SetVolume(float volume)
        {
            EventManagerService.TriggerEvent(new MusicEvent.SetVolume(Mathf.Clamp01(volume)));
        }
        
        public static void PlayTrack(AudioClip track, float fadeOutDuration = 0f, float fadeInDuration = 0f)
        {
            EventManagerService.TriggerEvent(new MusicEvent.PlayTrack(track, fadeOutDuration, fadeInDuration));
        }
            
        public static void StopTrack(float fadeOutDuration = 0f)
        {
            EventManagerService.TriggerEvent(new MusicEvent.StopTrack(fadeOutDuration));
        }
        
        public static void PauseTrack(float fadeOutDuration = 0f)
        {
            EventManagerService.TriggerEvent(new MusicEvent.PauseTrack(fadeOutDuration));
        }
            
        public static void ResumeTrack(float fadeInDuration = 0f)
        {
            EventManagerService.TriggerEvent(new MusicEvent.ResumeTrack(fadeInDuration));
        }
    }
}