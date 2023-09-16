using System;

namespace DJM.CoreServices.Services.MusicController
{
    internal sealed class MusicControllerEventHandler : IInitializable, IDisposable
    {
        private readonly IEventManager _eventManager;
        private readonly IMusicController _musicController;

        public MusicControllerEventHandler(IEventManager eventManager, IMusicController musicController)
        {
            _eventManager = eventManager;
            _musicController = musicController;
        }
        
        public void Initialize()
        {
            // music events
            _eventManager.Subscribe<MusicControllerEvent.SetMute>(OnSetMute);
            _eventManager.Subscribe<MusicControllerEvent.SetVolume>(OnSetVolume);
            _eventManager.Subscribe<MusicControllerEvent.PlayTrack>(OnPlayTrack);
            _eventManager.Subscribe<MusicControllerEvent.StopTrack>(OnStopTrack);
            _eventManager.Subscribe<MusicControllerEvent.PauseTrack>(OnPauseTrack);
            _eventManager.Subscribe<MusicControllerEvent.ResumeTrack>(OnResumeTrack);
        }

        public void Dispose()
        {
            // music events
            _eventManager.Unsubscribe<MusicControllerEvent.SetMute>(OnSetMute);
            _eventManager.Unsubscribe<MusicControllerEvent.SetVolume>(OnSetVolume);
            _eventManager.Unsubscribe<MusicControllerEvent.PlayTrack>(OnPlayTrack);
            _eventManager.Unsubscribe<MusicControllerEvent.StopTrack>(OnStopTrack);
            _eventManager.Unsubscribe<MusicControllerEvent.PauseTrack>(OnPauseTrack);
            _eventManager.Unsubscribe<MusicControllerEvent.ResumeTrack>(OnResumeTrack);
        }
        
        private void OnSetMute(MusicControllerEvent.SetMute eventData)
        { 
            _musicController.SetMute(eventData.Mute);
        }
        
        private void OnSetVolume(MusicControllerEvent.SetVolume eventData)
        { 
           _musicController.SetVolume(eventData.Volume);
        }

        private void OnPlayTrack(MusicControllerEvent.PlayTrack eventData)
        {
            _musicController.PlayTrack(eventData.Track, eventData.FadeOutDuration, eventData.FadeInDuration);
        }

        private void OnStopTrack(MusicControllerEvent.StopTrack eventData)
        {
            _musicController.StopTrack(eventData.FadeOutDuration);
        }

        private void OnPauseTrack(MusicControllerEvent.PauseTrack eventData)
        {
            _musicController.PauseTrack(eventData.FadeOutDuration);
        }

        private void OnResumeTrack(MusicControllerEvent.ResumeTrack eventData)
        {
            _musicController.ResumeTrack(eventData.FadeInDuration);
        }
    }
}