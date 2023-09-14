using System;
using DJM.CoreUtilities.Services.Events;

namespace DJM.CoreUtilities.Services.Music
{
    internal sealed class MusicEventHandler : IDisposable
    {
        private readonly IEventManagerService _eventManagerService;
        private readonly IMusicService _musicService;

        internal MusicEventHandler(IEventManagerService eventManagerService, IMusicService musicService)
        {
            _eventManagerService = eventManagerService;
            _musicService = musicService;
            Initialize();
        }
        
        private void Initialize()
        {
            // music events
            _eventManagerService.Subscribe<MusicEvent.SetMute>(OnSetMute);
            _eventManagerService.Subscribe<MusicEvent.SetVolume>(OnSetVolume);
            _eventManagerService.Subscribe<MusicEvent.PlayTrack>(OnPlayTrack);
            _eventManagerService.Subscribe<MusicEvent.StopTrack>(OnStopTrack);
            _eventManagerService.Subscribe<MusicEvent.PauseTrack>(OnPauseTrack);
            _eventManagerService.Subscribe<MusicEvent.ResumeTrack>(OnResumeTrack);
        }

        public void Dispose()
        {
            // music events
            _eventManagerService.Unsubscribe<MusicEvent.SetMute>(OnSetMute);
            _eventManagerService.Unsubscribe<MusicEvent.SetVolume>(OnSetVolume);
            _eventManagerService.Unsubscribe<MusicEvent.PlayTrack>(OnPlayTrack);
            _eventManagerService.Unsubscribe<MusicEvent.StopTrack>(OnStopTrack);
            _eventManagerService.Unsubscribe<MusicEvent.PauseTrack>(OnPauseTrack);
            _eventManagerService.Unsubscribe<MusicEvent.ResumeTrack>(OnResumeTrack);
        }
        
        private void OnSetMute(MusicEvent.SetMute eventData)
        { 
            _musicService.SetMute(eventData.Mute);
        }
        
        private void OnSetVolume(MusicEvent.SetVolume eventData)
        { 
           _musicService.SetVolume(eventData.Volume);
        }

        private void OnPlayTrack(MusicEvent.PlayTrack eventData)
        {
            _musicService.PlayTrack(eventData.Track, eventData.FadeOutDuration, eventData.FadeInDuration);
        }

        private void OnStopTrack(MusicEvent.StopTrack eventData)
        {
            _musicService.StopTrack(eventData.FadeOutDuration);
        }

        private void OnPauseTrack(MusicEvent.PauseTrack eventData)
        {
            _musicService.PauseTrack(eventData.FadeOutDuration);
        }

        private void OnResumeTrack(MusicEvent.ResumeTrack eventData)
        {
            _musicService.ResumeTrack(eventData.FadeInDuration);
        }
    }
}