/*using System;

namespace DJM.CoreServices.DependencyInjection.Binding
{
    internal sealed class BindingUpdateHandler
    {
        internal readonly Type BindingType;
        private BindingData _bindingData;
        private readonly Action<Type, BindingData> _updateCallback;
        
        internal BindingUpdateHandler(Type bindingType, BindingData bindingData, Action<Type, BindingData> updateCallback)
        {
            BindingType = bindingType;
            _bindingData = bindingData;
            _updateCallback = updateCallback;
        }

        internal void SetConcreteType(Type concreteType)
        {
            if(_bindingData.ConcreteType == concreteType) return;
            
            var updatedBindingData = new BindingData
            (
                concreteType, 
                _bindingData.InitializationOption, 
                _bindingData.IsSingle, 
                _bindingData.IsNonLazy
            );
            
            _bindingData = updatedBindingData;
            _updateCallback?.Invoke(BindingType, _bindingData);
        }
        
        internal void SetConstructorOption(InitializationOption initializationOption)
        {
            if(_bindingData.InitializationOption == initializationOption) return;
            
            var updatedBindingData = new BindingData
            (
                _bindingData.ConcreteType, 
                initializationOption, 
                _bindingData.IsSingle, 
                _bindingData.IsNonLazy
            );
            
            _bindingData = updatedBindingData;
            _updateCallback?.Invoke(BindingType, _bindingData);
        }
        
        internal void SetIsSingle(bool isSingle)
        {
            if(_bindingData.IsSingle == isSingle) return;
            
            var updatedBindingData = new BindingData
            (
                _bindingData.ConcreteType, 
                _bindingData.InitializationOption, 
                isSingle, 
                _bindingData.IsNonLazy
            );
            
            _bindingData = updatedBindingData;
            _updateCallback?.Invoke(BindingType, _bindingData);
        }
        
        internal void SetIsNonLazy(bool isNonLazy)
        {
            if(_bindingData.IsNonLazy == isNonLazy) return;
            
            var updatedBindingData = new BindingData
            (
                _bindingData.ConcreteType, 
                _bindingData.InitializationOption, 
                _bindingData.IsSingle, 
                isNonLazy
            );
            
            _bindingData = updatedBindingData;
            _updateCallback?.Invoke(BindingType, _bindingData);
        }
    }
}*/