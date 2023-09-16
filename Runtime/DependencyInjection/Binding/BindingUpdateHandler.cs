using System;

namespace DJM.CoreServices.DependencyInjection.Binding
{
    internal sealed class BindingUpdateHandler
    {
        internal readonly Type BindingType;
        internal BindingData BindingData { get; private set; }
        private readonly Action<BindingData> _updateCallback;
        
        internal BindingUpdateHandler(Type bindingType, BindingData bindingData, Action<BindingData> updateCallback)
        {
            BindingType = bindingType;
            BindingData = bindingData;
            _updateCallback = updateCallback;
        }

        internal void SetConcreteType(Type concreteType)
        {
            if(BindingData.ConcreteType == concreteType) return;
            
            var updatedBindingData = new BindingData
            (
                concreteType, 
                BindingData.ConstructorOption, 
                BindingData.IsSingle, 
                BindingData.IsNonLazy
            );
            
            BindingData = updatedBindingData;
            _updateCallback?.Invoke(BindingData);
        }
        
        internal void SetConstructorOption(ConstructorOption constructorOption)
        {
            if(BindingData.ConstructorOption == constructorOption) return;
            
            var updatedBindingData = new BindingData
            (
                BindingData.ConcreteType, 
                constructorOption, 
                BindingData.IsSingle, 
                BindingData.IsNonLazy
            );
            
            BindingData = updatedBindingData;
            _updateCallback?.Invoke(BindingData);
        }
        
        internal void SetIsSingle(bool isSingle)
        {
            if(BindingData.IsSingle == isSingle) return;
            
            var updatedBindingData = new BindingData
            (
                BindingData.ConcreteType, 
                BindingData.ConstructorOption, 
                isSingle, 
                BindingData.IsNonLazy
            );
            
            BindingData = updatedBindingData;
            _updateCallback?.Invoke(BindingData);
        }
        
        internal void SetIsNonLazy(bool isNonLazy)
        {
            if(BindingData.IsNonLazy == isNonLazy) return;
            
            var updatedBindingData = new BindingData
            (
                BindingData.ConcreteType, 
                BindingData.ConstructorOption, 
                BindingData.IsSingle, 
                isNonLazy
            );
            
            BindingData = updatedBindingData;
            _updateCallback?.Invoke(BindingData);
        }
    }
}