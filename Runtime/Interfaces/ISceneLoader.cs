namespace DJM.CoreUtilities
{
    public interface ISceneLoader
    {
        public void LoadScene(string sceneName);
        public void CancelLoadingScene();
    }
}