using UnityEngine;

namespace DJM.CoreServices
{
    /// <summary>
    /// Service that provides audio playback functionality for transient sounds.
    /// </summary>
    public interface ITransientSoundService
    {
        /// <summary>
        /// Gets a value indicating whether the music is muted.
        /// </summary>
        public bool IsMuted { get; }
        
        /// <summary>
        /// Gets the volume level for the music, ranging from 0 to 1.
        /// </summary>
        public float Volume { get; }
        
        /// <summary>
        /// Mutes transient sounds.
        /// </summary>
        public void Mute();
        
        /// <summary>
        /// Unmutes transient sounds.
        /// </summary>
        public void UnMute();
        
        /// <summary>
        /// Sets the volume level for transient sounds.
        /// </summary>
        /// <param name="volume">The new volume level, ranging from 0 to 1.</param>
        public void SetVolume(float volume);
        
        /// <summary>
        /// Plays an instance of a specified sound.
        /// </summary>
        /// <param name="sound">The <see cref="AudioClip"/> to be played.</param>
        /// <param name="volumeScale">The independent volume scale for this sound.</param>
        /// <param name="pitch">The pitch of the sound.</param>
        public void PlaySound(AudioClip sound, float volumeScale = 1f, float pitch = 1f);
        
        /// <summary>
        /// Plays an instance of a specified sound, with a random pitch.
        /// </summary>
        /// <param name="sound">The <see cref="AudioClip"/> to be played.</param>
        /// <param name="volumeScale">The independent volume scale for this sound.</param>
        /// <param name="minPitch">The minimum pitch of the sound.</param>
        /// /// <param name="maxPitch">The maximum pitch of the sound.</param>
        public void PlaySoundRandomPitch(AudioClip sound, float volumeScale = 1f, float minPitch = 0.95f, float maxPitch = 1.05f);
    }
}