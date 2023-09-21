namespace DJM.CoreServices
{
    /// <summary>
    /// Service for handling scene loading operations.
    /// </summary>
    public interface ISceneLoader
    {
        /// <summary>
        /// Initiates the asynchronous loading of a specified scene.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        public void LoadScene(string sceneName);
    }
}