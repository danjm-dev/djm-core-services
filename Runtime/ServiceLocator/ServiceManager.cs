using System;
using DJM.DependencyInjection;

namespace DJM.CoreServices.ServiceLocator
{
    internal sealed class ServiceManager
    {
        private static ServiceManager _instance;
        internal static ServiceManager Instance => _instance ?? throw new InvalidOperationException($"{nameof(ServiceManager)} not initialized.");
        
        public readonly IResolvableContainer Container;
        
        private ServiceManager(IResolvableContainer container) => Container = container;

        internal static void Initialize(IResolvableContainer resolvableContainer)
        {
            if (_instance is not null)
            {
                throw new InvalidOperationException($"{nameof(ServiceManager)} has already been initialized.");
            }
            
            _instance = new ServiceManager(resolvableContainer);
        }
    }
}