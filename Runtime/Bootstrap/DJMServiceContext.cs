using System;
using DJM.CoreServices.DependencyInjection;
using UnityEngine;

namespace DJM.CoreServices.Bootstrap
{
    internal sealed class DJMServiceContext
    {
        private static DJMServiceContext _instance;
        internal static DJMServiceContext Instance => _instance ?? throw new InvalidOperationException($"{nameof(DJMServiceContext)} not initialized.");

        private readonly PersistantContextComponent _persistantContextComponent;
        //private readonly Dictionary<Type, MonoBehaviour> _persistantMonoBehaviourServices;
        public readonly IContainer DependencyContainer;

        
        private DJMServiceContext(PersistantContextComponent persistantContextComponent)
        {
            _persistantContextComponent = persistantContextComponent;
            //_persistantMonoBehaviourServices = new Dictionary<Type, MonoBehaviour>();
            DependencyContainer = new ContainerService(persistantContextComponent);
        }

        internal static DJMServiceContext Initialize()
        {
            if (_instance is not null)
            {
               // logger.LogWarning("Can not initialize, as instance already exists.", nameof(DJMServiceContext));
                return null;
            }
            
            // persistant context component
            var contextGameObject = new GameObject("[DJMContext]") { isStatic = true };
            var persistantContextComponent = contextGameObject.AddComponent<PersistantContextComponent>();
            
            
            _instance = new DJMServiceContext(persistantContextComponent);
            return _instance;
        }

        // internal T GetMonoBehaviorService<T>() where T : MonoBehaviour
        // {
        //     if (_persistantMonoBehaviourServices.ContainsKey(typeof(T)))
        //     {
        //         return (T)_persistantMonoBehaviourServices[typeof(T)];
        //     }
        //
        //     // todo: log add persistant component
        //     
        //     var component = _persistantContextComponent.AddComponentToNewChildGameObject<T>();
        //     _persistantMonoBehaviourServices.Add(typeof(T), component);
        //     return component;
        // }
    }
}