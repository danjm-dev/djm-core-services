namespace DJM.CoreServices.Services.SceneLoader
{
    public static class SceneLoaderEvent
    {
        public readonly struct LoadStarted { }
        
        public readonly struct LoadProgress
        {
            public readonly float Progress;
            public LoadProgress(float progress) => Progress = progress;
        }
        
        public readonly struct LoadEnded
        {
            public readonly bool WasCancelled;
            public LoadEnded(bool wasCancelled) => WasCancelled = wasCancelled;
        }
        
        public readonly struct ActivatingNewScene { }
    }
}