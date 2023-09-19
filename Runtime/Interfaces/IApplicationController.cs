namespace DJM.CoreServices
{
    /// <summary>
    /// Service for controlling the application's game state.
    /// </summary>
    public interface IApplicationController
    {
        /// <summary>
        /// Quits the game. This has different behavior in the Unity editor vs a built game.
        /// </summary>
        /// 
        public void QuitGame();
        
        /// <summary>
        /// Pauses the game by setting the time scale to 0.
        /// </summary>
        public void PauseGame();
        
        /// <summary>
        /// Unpauses the game by setting the time scale to 1.
        /// </summary>
        public void UnPauseGame();
    }
}