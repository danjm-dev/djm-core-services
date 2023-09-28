using System;
using DJM.DependencyInjection;

namespace DJM.CoreServices.ServiceLocator
{
    internal sealed class ServiceManager
    {
        private static ServiceManager _instance;
        internal static ServiceManager Instance => _instance ?? throw new InvalidOperationException($"{nameof(ServiceManager)} not initialized.");

        private readonly DependencyContainer _dependencyContainer;
        public IResolvableContainer Container => _dependencyContainer;
        
        private ServiceManager(DependencyContainer container) => _dependencyContainer = container;

        internal static void Initialize(DependencyContainer resolvableContainer)
        {
            if (_instance is not null)
            {
                throw new InvalidOperationException($"{nameof(ServiceManager)} has already been initialized.");
            }
            
            _instance = new ServiceManager(resolvableContainer);
        }

        internal static void Reset()
        {
            if(_instance is null) return;
            _instance._dependencyContainer.Dispose();
            _instance = null;
        }
    }
}