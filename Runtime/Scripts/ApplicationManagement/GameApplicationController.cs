using DJM.CoreUtilities.EventManagement;
using UnityEngine;

namespace DJM.CoreUtilities.ApplicationManagement
{
    public sealed class GameApplicationController : MonoBehaviour
    {
        private void Start()
        {
            GameEvents.Subscribe<GameApplicationEvents.QuitGameEvent>(Quit);
            GameEvents.Subscribe<GameApplicationEvents.PauseGameEvent>(Pause);
            GameEvents.Subscribe<GameApplicationEvents.UnPauseGameEvent>(UnPause);
        }
        
        private void OnDestroy()
        {
            GameEvents.Unsubscribe<GameApplicationEvents.QuitGameEvent>(Quit);
            GameEvents.Unsubscribe<GameApplicationEvents.PauseGameEvent>(Pause);
            GameEvents.Unsubscribe<GameApplicationEvents.UnPauseGameEvent>(UnPause);
        }

        private static void Quit(GameApplicationEvents.QuitGameEvent quitGameEventEvent)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        
        private static void Pause(GameApplicationEvents.PauseGameEvent pauseGameEventEvent) => Time.timeScale = 0f;
        
        private static void UnPause(GameApplicationEvents.UnPauseGameEvent unpauseGameEventEvent) => Time.timeScale = 1f;
    }
}