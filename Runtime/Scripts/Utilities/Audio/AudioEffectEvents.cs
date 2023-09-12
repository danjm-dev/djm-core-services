using UnityEngine;

namespace DJM.CoreUtilities.Audio
{
    internal sealed class AudioEffectEvents
    {
        internal readonly struct PlayClip
        {
            public readonly AudioClip AudioClip;
            public PlayClip(AudioClip audioClip) => AudioClip = audioClip;
        }

        internal readonly struct SetMute
        {
            public readonly bool Mute;
            public SetMute(bool mute) => Mute = mute;
        }

        internal readonly struct SetVolume
        {
            public readonly float Volume;
            public SetVolume(float volume) => Volume = volume;
        }
    }
}