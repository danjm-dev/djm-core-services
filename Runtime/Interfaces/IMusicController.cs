using UnityEngine;

namespace DJM.CoreServices.Interfaces
{
    public interface IMusicController
    {
        public void SetMute(bool mute);
        public void SetVolume(float volume);
        public void PlayTrack(AudioClip track, float fadeOutDuration, float fadeInDuration);
        public void StopTrack(float fadeOutDuration);
        public void PauseTrack(float fadeOutDuration);
        public void ResumeTrack(float fadeInDuration);
    }
}