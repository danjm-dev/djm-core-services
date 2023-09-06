using System;

namespace DJM.CoreUtilities
{
    public interface ISceneLoaderEvents
    {
        public event Action Start;
        public event Action FadeInStart;
        public event Action FadeInEnd;
        public event Action LoadStart;
        public event Action<float> LoadProgress;
        public event Action LoadEnd;
        public event Action Activate;
        public event Action FadeOutStart;
        public event Action FadeOutEnd;
        public event Action End;
    }
}