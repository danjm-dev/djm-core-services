namespace DJM.CoreServices.Interfaces
{
    public interface ISceneLoader
    {
        public void LoadScene(string sceneName);
        public void CancelLoadingScene();
    }
}