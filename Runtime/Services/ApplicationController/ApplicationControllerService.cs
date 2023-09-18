using UnityEngine;

namespace DJM.CoreServices.Services.ApplicationController
{
    internal sealed class ApplicationControllerService : IApplicationController
    {
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void PauseGame() => Time.timeScale = 0f;
        
        public void UnPauseGame() => Time.timeScale = 1f;
    }
}