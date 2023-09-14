namespace DJM.CoreUtilities.Services.Logger
{
    internal static class LogEvent
    {
        internal readonly struct Error
        {
            internal readonly string Message;
            internal readonly string Context;
            
            public Error(string message, string context)
            {
                Message = message;
                Context = context;
            }
        }
        
        internal readonly struct Warning
        {
            internal readonly string Message;
            internal readonly string Context;
            
            public Warning(string message, string context)
            {
                Message = message;
                Context = context;
            }
        }
        
        internal readonly struct Info
        {
            internal readonly string Message;
            internal readonly string Context;

            public Info(string message, string context)
            {
                Message = message;
                Context = context;
            }
        }
    }
}