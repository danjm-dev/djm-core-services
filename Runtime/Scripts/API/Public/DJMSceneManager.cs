using DJM.CoreUtilities.SceneLoading;
using UnityEngine.SceneManagement;

namespace DJM.CoreUtilities
{
    public static class DJMSceneManager
    {
        public static void LoadScene(string sceneName, SceneLoadSequenceConfig loadSequenceConfig)
        {
            if (loadSequenceConfig is null) LoadScene(sceneName);
            else SceneLoadSequenceRunner.Run(sceneName, loadSequenceConfig);
        }
        
        public static void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
    }
}