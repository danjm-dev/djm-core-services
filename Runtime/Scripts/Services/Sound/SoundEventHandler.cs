using System;
using DJM.CoreUtilities.Services.Events;

namespace DJM.CoreUtilities.Services.Sound
{
    internal sealed class SoundEventHandler : IDisposable
    {
        private readonly IEventManagerService _eventManagerService;
        
        private readonly ISoundService _soundService;

        internal SoundEventHandler(IEventManagerService eventManagerService, ISoundService soundService)
        {
            _eventManagerService = eventManagerService;
            _soundService = soundService;
            Initialize();
        }
        
        private void Initialize()
        {
            _eventManagerService.Subscribe<SoundEvent.SetMute>(OnSetMute);
            _eventManagerService.Subscribe<SoundEvent.SetVolume>(OnSetVolume);
            _eventManagerService.Subscribe<SoundEvent.PlayTransientSound>(OnPlayTransientSound);
        }

        public void Dispose()
        {
            _eventManagerService.Unsubscribe<SoundEvent.SetMute>(OnSetMute);
            _eventManagerService.Unsubscribe<SoundEvent.SetVolume>(OnSetVolume);
            _eventManagerService.Unsubscribe<SoundEvent.PlayTransientSound>(OnPlayTransientSound);
        }

        private void OnSetMute(SoundEvent.SetMute eventData)
        {
            _soundService.SetMute(eventData.Mute);
        }

        private void OnSetVolume(SoundEvent.SetVolume eventData)
        {
            _soundService.SetVolume(eventData.Volume);
        }

        private void OnPlayTransientSound(SoundEvent.PlayTransientSound eventData)
        {
            _soundService.PlayTransientSound(eventData.TransientSound, eventData.VolumeScale, eventData.Pitch);
        }
    }
}