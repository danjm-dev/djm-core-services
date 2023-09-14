using System;
using System.Collections.Generic;
using DJM.CoreServices.Interfaces;
using UnityEngine;
using ILogger = DJM.CoreServices.Interfaces.ILogger;

namespace DJM.CoreServices.Bootstrap
{
    internal sealed class DJMServiceContext
    {
        private static DJMServiceContext _instance;
        internal static DJMServiceContext Instance => _instance ?? throw new InvalidOperationException($"{nameof(DJMServiceContext)} not initialized.");

        private readonly PersistantContextComponent _persistantContextComponent;
        private readonly Dictionary<Type, MonoBehaviour> _persistantMonoBehaviourServices;
        
        
        internal readonly IEventManager EventManager;
        internal readonly ILogger Logger;
        
        private DJMServiceContext(IEventManager eventManager, ILogger logger, PersistantContextComponent persistantContextComponent)
        {
            EventManager = eventManager;
            Logger = logger;
            _persistantContextComponent = persistantContextComponent;
            _persistantMonoBehaviourServices = new Dictionary<Type, MonoBehaviour>();
        }

        internal static DJMServiceContext Initialize(IEventManager eventManager, ILogger logger)
        {
            if (_instance is not null)
            {
                logger.LogWarning("Can not initialize, as instance already exists.", nameof(DJMServiceContext));
                return null;
            }
            
            // persistant context component
            var contextGameObject = new GameObject("[DJMContext]") { isStatic = true };
            var persistantContextComponent = contextGameObject.AddComponent<PersistantContextComponent>();
            
            
            _instance = new DJMServiceContext(eventManager, logger, persistantContextComponent);
            return _instance;
        }

        internal T GetMonoBehaviorService<T>() where T : MonoBehaviour
        {
            if (_persistantMonoBehaviourServices.ContainsKey(typeof(T)))
            {
                return (T)_persistantMonoBehaviourServices[typeof(T)];
            }

            // todo: log add persistant component
            
            var component = _persistantContextComponent.AddComponentToNewChildGameObject<T>();
            _persistantMonoBehaviourServices.Add(typeof(T), component);
            return component;
        }
    }
}