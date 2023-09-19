using UnityEngine;

namespace DJM.CoreServices.Services.ApplicationController
{
    public sealed class ApplicationControllerService : IApplicationController
    {
        /// <inheritdoc/>
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        /// <inheritdoc/>
        public void PauseGame() => Time.timeScale = 0f;
        
        /// <inheritdoc/>
        public void UnPauseGame() => Time.timeScale = 1f;
    }
}