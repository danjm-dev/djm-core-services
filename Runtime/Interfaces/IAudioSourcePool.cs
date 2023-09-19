using UnityEngine;

namespace DJM.CoreServices
{
    /// <summary>
    /// Service for managing a pool of audio source components.
    /// </summary>
    public interface IAudioSourcePool
    {
        /// <summary>
        /// Retrieves an available AudioSource from the pool or creates a new one if none are available.
        /// </summary>
        /// <returns>An enabled AudioSource component.</returns>
        public AudioSource GetAudioSource();
        
        /// <summary>
        /// Releases an AudioSource back into the pool or destroys it if the pool has reached its maximum capacity.
        /// </summary>
        /// <param name="audioSource">The AudioSource component to release.</param>
        public void ReleaseAudioSource(AudioSource audioSource);
    }
}