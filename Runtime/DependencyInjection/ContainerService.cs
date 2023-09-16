using System;
using System.Collections.Generic;
using System.Reflection;
using DJM.CoreServices.Bootstrap;
using DJM.CoreServices.DependencyInjection.Binding;

namespace DJM.CoreServices.DependencyInjection
{
    internal sealed class ContainerService : IContainer
    {
        private readonly Dictionary<Type, BindingData> _bindingRegistrations = new();
        private readonly Dictionary<Type, object> _singleInstances = new();
        private readonly List<Type> _nonLazyBindings = new(); // ignoring for now

        private PersistantContextComponent _persistantContextComponent;
        
        // todo: circular dependencies, monoBehaviour/component dependencies, non public constructors
        
        // id like to expose less to the installer - maybe a special interface

        internal ContainerService(PersistantContextComponent persistantContextComponent)
        {
            _persistantContextComponent = persistantContextComponent;
        }
        
        public void Install(IInstaller installer) => installer.InstallBindings(this);
        
        public IGenericBind<TBinding> Bind<TBinding>()
        {
            var bindingType = typeof(TBinding);
            
            if (_bindingRegistrations.ContainsKey(bindingType)) 
                throw new TypeAlreadyRegisteredException(bindingType);
            
            // register binding with default binding data
            var bindingData = new BindingData(bindingType);
            _bindingRegistrations[bindingType] = bindingData;


            var bindingUpdateHandler = new BindingUpdateHandler(bindingType, bindingData,
                data => BindingUpdateHandler(bindingType, data));
            
            return new GenericBinder<TBinding>(bindingUpdateHandler);
        }
        
        public void RunValidation()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            foreach (var type in _bindingRegistrations.Keys)
            {
                try
                {
                    Resolve(type);
                }
                catch (Exception exception)
                {
                    throw new InitializationValidationFailedException(type, exception);
                }
            }
            _singleInstances.Clear();
#endif
        }
        
        public TBinding Resolve<TBinding>()
        {
            var type = typeof(TBinding);
            return (TBinding)Resolve(type);
        }
        
        private void BindingUpdateHandler(Type bindingType, BindingData bindingData)
        {
            // surely theres a better way to do this -
            // feels very cumbersome, particularly with the readonly struct and callback for each update
            _bindingRegistrations[bindingType] = bindingData;
        }
        
        private object Resolve(Type bindingType)
        {
            if (!_bindingRegistrations.ContainsKey(bindingType)) throw new TypeNotRegisteredException(bindingType);
            
            var bindingData = _bindingRegistrations[bindingType];

            // create transient instance
            if (!bindingData.IsSingle) return CreateInstance(bindingData);
            
            // return existing single instance
            if (_singleInstances.TryGetValue(bindingType, out var singleInstance)) return singleInstance;

            // create single instance
            var newInstance = CreateInstance(bindingData);
            _singleInstances[bindingType] = newInstance;
            return newInstance;
        }
        
        private object CreateInstance(BindingData bindingData)
        {
            return bindingData.ConstructorOption switch
            {
                ConstructorOption.New => CreateNewInstance(bindingData),
                ConstructorOption.NewComponentOnNewGameObject => CreateNewComponentOnNewGameObjectInstance(bindingData),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private object CreateNewInstance(BindingData bindingData)
        {
            var concreteType = bindingData.ConcreteType;
            var constructor = concreteType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)[0]; // currently gets first public constructor
            var constructorParameters = constructor.GetParameters();
            var parameters = new object[constructorParameters.Length];

            for (var i = 0; i < constructorParameters.Length; i++)
            {
                var parameterType = constructorParameters[i].ParameterType;
                parameters[i] = Resolve(parameterType);
            }

            return Activator.CreateInstance(concreteType, parameters);
        }
        
        private object CreateNewComponentOnNewGameObjectInstance(BindingData bindingData)
        {
            return _persistantContextComponent.AddComponentToNewChildGameObject(bindingData.ConcreteType);
        }
    }
}