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
    }
}