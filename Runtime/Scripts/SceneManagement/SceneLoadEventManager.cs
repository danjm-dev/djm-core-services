using System;

namespace DJM.CoreUtilities
{
    internal sealed class SceneLoadEventManager
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

        public void InvokeStart() => Start?.Invoke();
        public void InvokeFadeInStart() => FadeInStart?.Invoke();
        public void InvokeFadeInEnd() => FadeInEnd?.Invoke();
        public void InvokeLoadStart() => LoadStart?.Invoke();
        public void InvokeLoadProgress(float progress) => LoadProgress?.Invoke(progress);
        public void InvokeLoadEnd() => LoadEnd?.Invoke();
        public void InvokeActivate() => Activate?.Invoke();
        public void InvokeFadeOutStart() => FadeOutStart?.Invoke();
        public void InvokeFadeOutEnd() => FadeOutEnd?.Invoke();
        public void InvokeEnd() => End?.Invoke();
    }
}