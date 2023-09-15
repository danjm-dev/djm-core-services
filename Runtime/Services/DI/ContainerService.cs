using System;
using System.Collections.Generic;

namespace DJM.CoreServices.Services.DI
{
    internal sealed class ContainerService : IContainer
    {
        private readonly Dictionary<Type, DependencyRegistration> _dependencyRegistrations = new();
        private readonly Dictionary<Type, WeakReference> _singleInstances = new();
        
        // todo: circular dependencies, monoBehaviour/component dependencies, non public constructors

        public void RegisterTransient<TInterface, TImplementation>() where TImplementation : TInterface
        {
            RegisterType(typeof(TInterface), typeof(TImplementation), false);
        }
        
        public void RegisterSingle<TInterface, TImplementation>() where TImplementation : TInterface
        {
            RegisterType(typeof(TInterface), typeof(TImplementation), true);
        }

        private void RegisterType(Type interfaceType, Type implementationType, bool isSingle)
        {
            if (_dependencyRegistrations.ContainsKey(interfaceType))
                throw new Exception($"Type {interfaceType.FullName} is already registered.");
            _dependencyRegistrations[interfaceType] = new DependencyRegistration(implementationType, isSingle);
        }

        public object Resolve(Type type)
        {
            if (!_dependencyRegistrations.ContainsKey(type))
            {
                throw new Exception($"Type {type.FullName} is not registered.");
            }

            var registration = _dependencyRegistrations[type];
            var dependencyType = registration.Type;
            
            // if derived from Component - divert here


            if (!registration.IsSingle) return CreateInstance(dependencyType);
            
            if (_singleInstances.TryGetValue(type, out var weakReference) && weakReference.IsAlive)
            {
                return weakReference.Target;
            }

            var newInstance = CreateInstance(dependencyType);
            _singleInstances[type] = new WeakReference(newInstance);
            return newInstance;
        }
        
        public void Validate()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            foreach (var type in _dependencyRegistrations.Keys)
            {
                try
                {
                    Resolve(type);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to resolve type {type.FullName}.", ex);
                }
            }
            _singleInstances.Clear();
#endif
        }
        
        private object CreateInstance(Type type)
        {
            var constructor = type.GetConstructors()[0]; // currently gets first public constructor
            var constructorParameters = constructor.GetParameters();
            var parameters = new object[constructorParameters.Length];

            for (var i = 0; i < constructorParameters.Length; i++)
            {
                var parameterType = constructorParameters[i].ParameterType;
                parameters[i] = Resolve(parameterType);
            }

            return Activator.CreateInstance(type, parameters);
        }
        
        private readonly struct DependencyRegistration
        {
            public readonly Type Type;
            public readonly bool IsSingle;

            public DependencyRegistration(Type type, bool isSingle)
            {
                Type = type;
                IsSingle = isSingle;
            }
        }
    }
}