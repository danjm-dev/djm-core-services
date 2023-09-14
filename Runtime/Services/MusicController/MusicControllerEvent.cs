using UnityEngine;

namespace DJM.CoreServices.Services.MusicController
{
    internal static class MusicControllerEvent
    {
        internal readonly struct SetMute
        {
            internal readonly bool Mute;
            internal SetMute(bool mute) => Mute = mute;
        }

        internal readonly struct SetVolume
        {
            internal readonly float Volume;
            internal SetVolume(float volume) => Volume = volume;
        }

        internal readonly struct PlayTrack
        {
            internal readonly AudioClip Track;
            internal readonly float FadeOutDuration;
            internal readonly float FadeInDuration;

            internal PlayTrack(AudioClip track, float fadeOutDuration, float fadeInDuration)
            {
                Track = track;
                FadeOutDuration = fadeOutDuration;
                FadeInDuration = fadeInDuration;
            }
        }

        internal readonly struct StopTrack
        {
            internal readonly float FadeOutDuration;
            internal StopTrack(float fadeOutDuration) => FadeOutDuration = fadeOutDuration;
        }

        internal readonly struct PauseTrack
        {
            internal readonly float FadeOutDuration;
            internal PauseTrack(float fadeOutDuration) => FadeOutDuration = fadeOutDuration;
        }

        internal readonly struct ResumeTrack
        {
            internal readonly float FadeInDuration;
            internal ResumeTrack(float fadeInDuration) => FadeInDuration = fadeInDuration;
        }
    }
}