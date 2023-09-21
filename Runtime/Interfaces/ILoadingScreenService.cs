using System.Threading.Tasks;

namespace DJM.CoreServices
{
    /// <summary>
    /// Service for controlling loading screen components.
    /// </summary>
    public interface ILoadingScreenService
    {
        /// <summary>
        /// Shows the loading screen over a duration.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task Show();
        
        /// <summary>
        /// Hides the loading screen over a duration.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task Hide();
        
        /// <summary>
        /// Sets the load progress of the screen.
        /// </summary>
        /// <param name="progress">The load progress value between 0 and 1</param>
        public void SetLoadProgress(float progress);
        
        /// <summary>
        /// Signals to progress screen that loading is complete. Allows it to hold thread until ready.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task CompleteLoadProgress();
    }
}