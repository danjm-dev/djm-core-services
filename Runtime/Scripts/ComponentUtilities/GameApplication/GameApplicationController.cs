using UnityEngine;

namespace DJM.CoreUtilities.GameApplication
{
    public sealed class GameApplicationController : MonoBehaviour
    {
        private void Start()
        {
            DJMEvents.Subscribe<GameApplicationEvents.QuitGameEvent>(Quit);
            DJMEvents.Subscribe<GameApplicationEvents.PauseGameEvent>(Pause);
            DJMEvents.Subscribe<GameApplicationEvents.UnPauseGameEvent>(UnPause);
        }
        
        private void OnDestroy()
        {
            DJMEvents.Unsubscribe<GameApplicationEvents.QuitGameEvent>(Quit);
            DJMEvents.Unsubscribe<GameApplicationEvents.PauseGameEvent>(Pause);
            DJMEvents.Unsubscribe<GameApplicationEvents.UnPauseGameEvent>(UnPause);
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