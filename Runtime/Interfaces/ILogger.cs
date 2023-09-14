namespace DJM.CoreUtilities
{
    public interface ILogger
    {
        public void LogError(string message, string context);
        public void LogWarning(string message, string context);
        public void LogInfo(string message, string context);
    }
}