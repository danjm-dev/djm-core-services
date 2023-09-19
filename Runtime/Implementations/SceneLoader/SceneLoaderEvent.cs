namespace DJM.CoreServices.SceneLoader
{
    public static class SceneLoaderEvent
    {
        public readonly struct LoadStarted { }
        public readonly struct LoadProgress
        {
            public readonly float Progress;
            public LoadProgress(float progress) => Progress = progress;
        }
        public readonly struct LoadCancelled { }
        public readonly struct ActivatingNewScene { }
    }
}