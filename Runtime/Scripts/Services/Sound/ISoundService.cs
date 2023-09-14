using UnityEngine;

namespace DJM.CoreUtilities.Services.Sound
{
    public interface ISoundService
    {
        public void SetMute(bool mute);
        public void SetVolume(float volume);
        public void PlayTransientSound(AudioClip transientSound, float volumeScale, float pitch);
    }
}