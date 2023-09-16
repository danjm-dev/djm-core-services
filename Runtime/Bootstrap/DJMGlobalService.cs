using System;

namespace DJM.CoreServices.Bootstrap
{
    internal sealed class DJMGlobalService
    {
        private static DJMGlobalService _instance;
        internal static DJMGlobalService Instance => _instance ?? throw new InvalidOperationException($"{nameof(DJMGlobalService)} not initialized.");
        
        public readonly IResolvableContainer ResolvableContainer;
        
        private DJMGlobalService(IResolvableContainer resolvableContainer) => ResolvableContainer = resolvableContainer;

        internal static void Initialize(IResolvableContainer resolvableContainer)
        {
            if (_instance is not null)
            {
                throw new InvalidOperationException($"{nameof(DJMGlobalService)} has already been initialized.");
            }
            
            _instance = new DJMGlobalService(resolvableContainer);
        }
    }
}