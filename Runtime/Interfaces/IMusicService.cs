using UnityEngine;

namespace DJM.CoreServices
{
    /// <summary>
    /// Service that provides audio playback functionality for music.
    /// </summary>
    public interface IMusicService
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
        /// Gets a value indicating whether music is currently being played.
        /// </summary>
        public bool IsPlaying { get; }
        
        /// <summary>
        /// Mutes the music.
        /// </summary>
        public void Mute();
        
        /// <summary>
        /// Unmutes the music.
        /// </summary>
        public void UnMute();
        
        /// <summary>
        /// Sets the volume level for the music.
        /// </summary>
        /// <param name="volume">The new volume level, ranging from 0 to 1.</param>
        public void SetVolume(float volume);
        
        /// <summary>
        /// Plays a specified audio track.
        /// </summary>
        /// <param name="track">The <see cref="AudioClip"/> to be played.</param>
        /// <param name="fadeOutDuration">The fade-out duration for the previous track.</param>
        /// <param name="fadeInDuration">The fade-in duration for the new track.</param>
        public void PlayTrack(AudioClip track, float fadeOutDuration = 0f, float fadeInDuration = 0f);
        
        /// <summary>
        /// Stops the currently playing track.
        /// </summary>
        /// <param name="fadeOutDuration">The fade-out duration for the track to be stopped.</param>
        public void StopTrack(float fadeOutDuration = 0f);
        
        /// <summary>
        /// Pauses the currently playing track.
        /// </summary>
        /// <param name="fadeOutDuration">The fade-out duration before the pause.</param>
        public void PauseTrack(float fadeOutDuration = 0f);
        
        /// <summary>
        /// Resumes playback of a paused track.
        /// </summary>
        /// <param name="fadeInDuration">The fade-in duration for resuming the track.</param>
        public void ResumeTrack(float fadeInDuration = 0f);
    }
}