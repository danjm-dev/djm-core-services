using System;

namespace DJM.CoreServices.Services.DI
{
    public readonly struct BindingData
    {
        public readonly Type ConcreteType;
        public readonly ConstructorOption ConstructorOption;
        public readonly bool IsSingle;
        public readonly bool IsNonLazy;

        public BindingData(Type concreteType, ConstructorOption constructorOption = ConstructorOption.New, bool isSingle = false, bool isNonLazy = false)
        {
            ConcreteType = concreteType;
            ConstructorOption = constructorOption;
            IsSingle = isSingle;
            IsNonLazy = isNonLazy;
        }
    }
        
    public enum ConstructorOption : byte
    {
        New,
        NewComponentOnNewGameObject
    }
}