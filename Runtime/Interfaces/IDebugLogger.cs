namespace DJM.CoreServices
{
    /// <summary>
    /// Centralized service for logging debug messages with different severity levels.
    /// </summary>
    public interface IDebugLogger
    {
        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="context">The context in which the error occurred.</param>
        public void LogError(string message, string context);
        
        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="context">The context in which the warning was generated.</param>
        public void LogWarning(string message, string context);
        
        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="context">The context related to the informational message.</param>
        public void LogInfo(string message, string context);
    }
}