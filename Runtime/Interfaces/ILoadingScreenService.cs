using System.Collections;

namespace DJM.CoreServices
{
    /// <summary>
    /// Service for controlling loading screen components.
    /// </summary>
    public interface ILoadingScreenService
    {
        public IEnumerator Show();
        
        public IEnumerator Hide();
        
        public void SetLoadProgress(float progress);
        
        public IEnumerator CompleteLoadProgress();
    }
}