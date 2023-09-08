using DJM.CoreUtilities.Audio;
using UnityEngine;

namespace DJM.CoreUtilities
{
    public static class DJMAudio
    {
        public static void PlayAudioEffect(AudioClip audioClip)
        {
            DJMEvents.TriggerEvent(new AudioEffectEvents.PlayClip(audioClip));
        }
        
        public static void MuteAudioEffects()
        {
            DJMEvents.TriggerEvent(new AudioEffectEvents.Mute());
        }
        
        public static void SetAudioEffectVolume(float volume)
        {
            DJMEvents.TriggerEvent(new AudioEffectEvents.SetVolume(volume));
        }
    }
}