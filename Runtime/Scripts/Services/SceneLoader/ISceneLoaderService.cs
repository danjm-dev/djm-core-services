namespace DJM.CoreUtilities.Services.SceneLoader
{
    public interface ISceneLoaderService
    {
        public void LoadScene(string sceneName);
        public void CancelLoadingScene();
    }
}