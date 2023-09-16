using System;
using System.Collections.Generic;
using System.Reflection;
using DJM.CoreServices.DependencyInjection.Binding;
using DJM.CoreServices.DependencyInjection.ComponentContext;

namespace DJM.CoreServices.DependencyInjection
{
    internal sealed class DependencyContainer : IResolvableContainer, IBindableContainer
    {
        private readonly Dictionary<Type, BindingData> _bindings;
        private readonly Dictionary<Type, object> _singleInstances;
        private readonly HashSet<Type> _nonLazyBindings; // ignoring for now
        private readonly GameObjectContext _gameObjectContext;

        internal DependencyContainer(GameObjectContext gameObjectContext)
        {
            _bindings = new Dictionary<Type, BindingData>();
            _singleInstances = new Dictionary<Type, object>();
            _nonLazyBindings = new HashSet<Type>();
            
            _gameObjectContext = gameObjectContext;
            _gameObjectContext.OnDestroyContext += () => { }; // what to do with this...?
        }
        
        public IGenericBind<TBinding> Bind<TBinding>()
        {
            var bindingType = typeof(TBinding);
            if (_bindings.ContainsKey(bindingType)) throw new TypeAlreadyRegisteredException(bindingType);
            
            // register binding with default data
            var bindingData = new BindingData(bindingType);
            _bindings[bindingType] = bindingData;
            
            var bindingUpdateHandler = new BindingUpdateHandler(bindingType, bindingData, BinderUpdateHandler);
            return new GenericBinder<TBinding>(bindingUpdateHandler);
        }

        private void BinderUpdateHandler(Type bindingType, BindingData bindingData)
        {
            if (bindingData.IsNonLazy) _nonLazyBindings.Add(bindingType);
            _bindings[bindingType] = bindingData;
        }
        
        public void Install(IInstaller installer)
        {
            installer.InstallBindings(this);
            ValidateBindings();
            ResolveNonLazyBindings();
        }

        public TBinding Resolve<TBinding>()
        {
            var type = typeof(TBinding);
            return (TBinding)Resolve(type);
        }
        
        private object Resolve(Type bindingType)
        {
            if (!_bindings.ContainsKey(bindingType)) throw new TypeNotRegisteredException(bindingType);
            
            var bindingData = _bindings[bindingType];

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
            return _gameObjectContext.AddComponentToNewChildGameObject(bindingData.ConcreteType);
        }
        
        private void ValidateBindings()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            foreach (var type in _bindings.Keys)
            {
                try
                {
                    Resolve(type);
                }
                catch (Exception exception)
                {
                    throw new InstallationValidationFailedException(type, exception);
                }
            }
            _singleInstances.Clear();
#endif
        }

        private void ResolveNonLazyBindings()
        {
            // these should all be single
            foreach (var binding in _nonLazyBindings) Resolve(binding);
        }
    }
}