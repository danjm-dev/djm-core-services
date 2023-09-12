using System;
using DG.Tweening;
using UnityEngine;

namespace DJM.CoreUtilities.Audio
{
    /// <summary>
    /// Handles various audio events and transitions for music tracks.
    /// </summary>
    internal sealed class MusicEventHandler
    {
        /// <summary>
        /// The AudioSource component used for music playback.
        /// </summary>
        private readonly AudioSource _audioSource;

        private Sequence _pauseTrackTween;
        private Sequence _resumeTrackTween;
        private Sequence _playTrackTween;
        private Sequence _stopTrackTween;
        
        /// <summary>
        /// Gets the current volume of the AudioSource.
        /// </summary>
        internal float Volume => _audioSource.volume;
        
        /// <summary>
        /// Gets a value indicating whether the AudioSource is currently playing.
        /// </summary>
        internal bool IsPlaying => _audioSource.isPlaying;
        
        /// <summary>
        /// Gets or sets a value indicating whether the AudioSource is paused.
        /// </summary>
        internal bool IsPaused { get; private set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MusicEventHandler"/> class.
        /// </summary>
        /// <param name="audioSource">The AudioSource component to manage.</param>
        internal MusicEventHandler(AudioSource audioSource)
        {
            _audioSource = audioSource 
                ? audioSource 
                : throw new ArgumentException("AudioSource cannot be null.");
        }

        /// <summary>
        /// Mutes or unmutes the AudioSource.
        /// </summary>
        /// <param name="mute">Whether to mute the AudioSource.</param>
        internal void SetMute(bool mute) => _audioSource.mute = mute;
        
        /// <summary>
        /// Sets the volume of the AudioSource.
        /// </summary>
        /// <param name="volume">The volume level to set.</param>
        internal void SetVolume(float volume) => _audioSource.volume = volume;
        
        /// <summary>
        /// Pauses the currently playing track with an optional fade-out.
        /// </summary>
        /// <param name="fadeOutDuration">The duration of the fade-out effect. Set to 0 for immediate pause.</param>
        internal void PauseTrack(float fadeOutDuration)
        {
            // force complete any existing music operations
            _pauseTrackTween?.Complete();
            _resumeTrackTween?.Complete();
            _playTrackTween?.Complete();
            _stopTrackTween?.Complete();
            
            if(!_audioSource.isPlaying) return;
            
            // no fade duration, immediate pause
            if (fadeOutDuration <= 0f)
            {
                _audioSource.Pause();
                IsPaused = true;
                return;
            }

            _pauseTrackTween = DOTween.Sequence();
                
            _pauseTrackTween.Append(_audioSource.DOFade(0f, fadeOutDuration * Volume));
            _pauseTrackTween.AppendCallback(() =>
            {
                _audioSource.Pause();
                IsPaused = true;
                _audioSource.volume = Volume;
                _pauseTrackTween = null;
            });
        }
        
        /// <summary>
        /// Resumes a paused track with an optional fade-in.
        /// </summary>
        /// <param name="fadeInDuration">The duration of the fade-in effect. Set to 0 for immediate resume.</param>
        internal void ResumeTrack(float fadeInDuration)
        {
            // force complete any existing music operations
            _pauseTrackTween?.Complete();
            _resumeTrackTween?.Complete();
            _playTrackTween?.Complete();
            _stopTrackTween?.Complete();
            
            if(_audioSource.isPlaying || !IsPaused) return;
            
            // no fade duration - resume immediately
            if (fadeInDuration <= 0f)
            {
                _audioSource.UnPause();
                IsPaused = false;
                return;
            }
            
            _resumeTrackTween = DOTween.Sequence();
                
            _resumeTrackTween.Append(_audioSource.DOFade(Volume, fadeInDuration * Volume));
            _resumeTrackTween.AppendCallback(() =>
            {
                _audioSource.UnPause();
                IsPaused = false;
                _audioSource.volume = Volume;
                _resumeTrackTween = null;
            });
        }
        
        /// <summary>
        /// Plays a new track, optionally fading out the current track and fading in the new one.
        /// </summary>
        /// <param name="fadeOutDuration">The duration of the fade-out for the current track. Set to 0 for immediate stop.</param>
        /// <param name="fadeInDuration">The duration of the fade-in for the new track. Set to 0 for immediate start.</param>
        /// <param name="track">The AudioClip of the new track to play.</param>
        internal void PlayTrack(float fadeOutDuration, float fadeInDuration, AudioClip track)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            
            if(track is null) 
                DJMLogger.Log
                (
                    "Attempted to play null AudioClip",
                    $"{nameof(MusicEventHandler)}/{nameof(PlayTrack)}", 
                    DJMLogger.LogLevel.Error
                );
#endif
            
            // force complete any existing music operations
            _pauseTrackTween?.Complete();
            _resumeTrackTween?.Complete();
            _playTrackTween?.Complete();
            _stopTrackTween?.Complete();

            // no fading - immediately stop track and start new one
            if (fadeOutDuration <= 0f && fadeInDuration <= 0f)
            {
                _audioSource.Stop();
                _audioSource.clip = track;
                _audioSource.Play();
                return;
            }
            
            _playTrackTween = DOTween.Sequence();
            
            // set volume to 0
            if (fadeOutDuration > 0f) _playTrackTween.Append(_audioSource.DOFade(0f, fadeOutDuration * Volume));
            else _playTrackTween.AppendCallback(() => _audioSource.volume = 0f);
            
            // stop current track
            _playTrackTween.AppendCallback(() => _audioSource.Stop());
            
            // set new track
            _playTrackTween.AppendCallback(() => _audioSource.clip = track);

            // play new track
            _playTrackTween.AppendCallback(() => _audioSource.Play());
            
            // set volume to original value
            if (fadeInDuration > 0f) _playTrackTween.Append(_audioSource.DOFade(Volume, fadeInDuration * Volume));
            else _playTrackTween.AppendCallback(() => _audioSource.volume = Volume);
            
            // dispose of sequence
            _playTrackTween.AppendCallback(() => _playTrackTween = null);
        }

        /// <summary>
        /// Stops the currently playing track with an optional fade-out.
        /// </summary>
        /// <param name="fadeOutDuration">The duration of the fade-out effect. Set to 0 for immediate stop.</param>
        internal void StopTrack(float fadeOutDuration)
        {
            // force complete any existing music operations
            _pauseTrackTween?.Complete();
            _resumeTrackTween?.Complete();
            _playTrackTween?.Complete();
            _stopTrackTween?.Complete();
            
            if(!_audioSource.isPlaying) return;
            
            // no fade duration, immediate stop
            if (fadeOutDuration <= 0f)
            {
                _audioSource.Stop();
                return;
            }

            _stopTrackTween = DOTween.Sequence();
                
            _stopTrackTween.Append(_audioSource.DOFade(0f, fadeOutDuration * Volume));
            _stopTrackTween.AppendCallback(() =>
            {
                _audioSource.Stop();
                _audioSource.volume = Volume;
                _stopTrackTween = null;
            });
        }
    }
}