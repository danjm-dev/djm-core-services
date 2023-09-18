namespace DJM.CoreServices
{
    public interface ILoggerService
    {
        public void LogError(string message, string context);
        public void LogWarning(string message, string context);
        public void LogInfo(string message, string context);
    }
}