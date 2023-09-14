using UnityEngine;

namespace DJM.CoreUtilities.Services.SoundController
{
    internal static class SoundControllerEvent
    {
        internal readonly struct PlayTransientSound
        {
            internal readonly AudioClip TransientSound;
            internal readonly float VolumeScale;
            internal readonly float Pitch;
            internal PlayTransientSound(AudioClip transientSound, float volumeScale, float pitch)
            {
                TransientSound = transientSound;
                VolumeScale = volumeScale;
                Pitch = pitch;
            }
        }

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
    }
}