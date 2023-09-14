using System;
using System.Collections.Generic;
using DJM.CoreUtilities.Services.Events;
using DJM.CoreUtilities.Services.Logger;
using UnityEngine;

namespace DJM.CoreUtilities.ServiceContext
{
    internal sealed class DJMServiceContext
    {
        private static DJMServiceContext _instance;
        internal static DJMServiceContext Instance => _instance ?? throw new InvalidOperationException($"{nameof(DJMServiceContext)} not initialized.");

        private readonly PersistantContextComponent _persistantContextComponent;
        private readonly Dictionary<Type, MonoBehaviour> _persistantMonoBehaviourServices;
        
        
        internal readonly IEventManagerService EventManagerService;
        internal readonly ILoggerService LoggerService;
        
        private DJMServiceContext(IEventManagerService eventManagerService, ILoggerService loggerService, PersistantContextComponent persistantContextComponent)
        {
            EventManagerService = eventManagerService;
            LoggerService = loggerService;
            _persistantContextComponent = persistantContextComponent;
            _persistantMonoBehaviourServices = new Dictionary<Type, MonoBehaviour>();
        }

        internal static DJMServiceContext Initialize(IEventManagerService eventManagerService, ILoggerService loggerService)
        {
            if (_instance is not null)
            {
                loggerService.LogWarning("Can not initialize, as instance already exists.", nameof(DJMServiceContext));
                return null;
            }
            
            // persistant context component
            var contextGameObject = new GameObject("[DJMContext]") { isStatic = true };
            var persistantContextComponent = contextGameObject.AddComponent<PersistantContextComponent>();
            
            
            _instance = new DJMServiceContext(eventManagerService, loggerService, persistantContextComponent);
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