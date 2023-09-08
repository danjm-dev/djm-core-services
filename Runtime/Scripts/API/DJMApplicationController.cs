using UnityEngine;

namespace DJM.CoreUtilities
{
    public static class DJMApplicationController
    {
        public static void QuitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        
        public static void PauseGame() => Time.timeScale = 0f;
        public static void UnPauseGame() => Time.timeScale = 1f;
    }
}