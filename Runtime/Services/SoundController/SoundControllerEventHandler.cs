using System;

namespace DJM.CoreUtilities.Services.SoundController
{
    internal sealed class SoundControllerEventHandler : IDisposable
    {
        private readonly IEventManager _eventManager;
        
        private readonly ISoundController _soundController;

        internal SoundControllerEventHandler(IEventManager eventManager, ISoundController soundController)
        {
            _eventManager = eventManager;
            _soundController = soundController;
            Initialize();
        }
        
        private void Initialize()
        {
            _eventManager.Subscribe<SoundControllerEvent.SetMute>(OnSetMute);
            _eventManager.Subscribe<SoundControllerEvent.SetVolume>(OnSetVolume);
            _eventManager.Subscribe<SoundControllerEvent.PlayTransientSound>(OnPlayTransientSound);
        }

        public void Dispose()
        {
            _eventManager.Unsubscribe<SoundControllerEvent.SetMute>(OnSetMute);
            _eventManager.Unsubscribe<SoundControllerEvent.SetVolume>(OnSetVolume);
            _eventManager.Unsubscribe<SoundControllerEvent.PlayTransientSound>(OnPlayTransientSound);
        }

        private void OnSetMute(SoundControllerEvent.SetMute eventData)
        {
            _soundController.SetMute(eventData.Mute);
        }

        private void OnSetVolume(SoundControllerEvent.SetVolume eventData)
        {
            _soundController.SetVolume(eventData.Volume);
        }

        private void OnPlayTransientSound(SoundControllerEvent.PlayTransientSound eventData)
        {
            _soundController.PlayTransientSound(eventData.TransientSound, eventData.VolumeScale, eventData.Pitch);
        }
    }
}