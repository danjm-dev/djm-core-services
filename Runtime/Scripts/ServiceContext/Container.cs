using System;
using System.Collections.Generic;

namespace DJM.CoreUtilities.ServiceContext
{
    internal sealed class Container
    {
        private readonly Dictionary<Type, RegisteredType> _typeMappings = new();
        private readonly Dictionary<Type, object> _singleInstances = new();
        

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
            if (_typeMappings.ContainsKey(interfaceType))
                throw new Exception($"Type {interfaceType.FullName} is already registered.");
            _typeMappings[interfaceType] = new RegisteredType(implementationType, isSingle);
        }

        public TInterface Resolve<TInterface>()
        {
            var type = typeof(TInterface);
            if (!_typeMappings.ContainsKey(type))
            {
                throw new Exception($"Type {type.FullName} is not registered.");
            }

            var registeredType = _typeMappings[type];
            
            var implementationType = registeredType.Type;
            var constructor = implementationType.GetConstructors()[0]; // dont think this will get non public constructors
            var constructorParameters = constructor.GetParameters();
            var parameters = new object[constructorParameters.Length];

            for (var i = 0; i < constructorParameters.Length; i++)
            {
                var parameterType = constructorParameters[i].ParameterType;
                
                // type not registered, throw exception
                if (!_typeMappings.TryGetValue(parameterType, out var mapping))
                {
                    throw new Exception($"Type {parameterType.FullName} is not registered.");
                }

                // type is transient, create instance
                if (!mapping.IsSingle)
                {
                    parameters[i] = Activator.CreateInstance(mapping.Type);
                    continue;
                }

                // type is single, get existing instance or create new one
                if (_singleInstances.TryGetValue(parameterType, out var instance))
                    parameters[i] = instance;
                else
                    parameters[i] = _singleInstances[parameterType] = Activator.CreateInstance(mapping.Type);
            }

            return (TInterface) constructor.Invoke(parameters);
        }
        
        private readonly struct RegisteredType
        {
            public readonly Type Type;
            public readonly bool IsSingle;

            public RegisteredType(Type type, bool isSingle)
            {
                Type = type;
                IsSingle = isSingle;
            }
        }
    }
}