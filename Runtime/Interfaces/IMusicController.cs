using UnityEngine;

namespace DJM.CoreServices
{
    public interface IMusicController
    {
        public void Mute();
        public void UnMute();
        public void SetVolume(float volume);
        public void PlayTrack(AudioClip track, float fadeOutDuration = 0f, float fadeInDuration = 0f);
        public void StopTrack(float fadeOutDuration = 0f);
        public void PauseTrack(float fadeOutDuration = 0f);
        public void ResumeTrack(float fadeInDuration = 0f);
    }
}