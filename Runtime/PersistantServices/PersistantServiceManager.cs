using System;
using DJM.DependencyInjection;

namespace DJM.CoreServices.PersistantServices
{
    internal sealed class PersistantServiceManager
    {
        private static PersistantServiceManager _instance;
        internal static PersistantServiceManager Instance => _instance ?? throw new InvalidOperationException($"{nameof(PersistantServiceManager)} not initialized.");
        
        public readonly IResolvableContainer Container;
        
        private PersistantServiceManager(IResolvableContainer container) => Container = container;

        internal static void Initialize(IResolvableContainer resolvableContainer)
        {
            if (_instance is not null)
            {
                throw new InvalidOperationException($"{nameof(PersistantServiceManager)} has already been initialized.");
            }
            
            _instance = new PersistantServiceManager(resolvableContainer);
        }
    }
}