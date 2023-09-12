using UnityEngine;

namespace DJM.CoreUtilities.Audio
{
    public static class MusicEvents
    {
        internal readonly struct SetTrack
        {
            public readonly AudioClip AudioClip;
            public SetTrack(AudioClip audioClip) => AudioClip = audioClip;
        }
        
        internal readonly struct Play { }
        internal readonly struct Stop { }

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