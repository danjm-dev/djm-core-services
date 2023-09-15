namespace DJM.CoreServices
{
    public interface ISceneLoader
    {
        public void LoadScene(string sceneName);
        public void CancelLoadingScene();
    }
}