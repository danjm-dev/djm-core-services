using System;

namespace DJM.CoreServices.DependencyInjection
{
    internal readonly struct BindingData
    {
        public readonly Type ConcreteType;
        public readonly ConstructorOption ConstructorOption;
        public readonly bool IsSingle;
        public readonly bool IsNonLazy;

        internal BindingData(Type concreteType, ConstructorOption constructorOption = ConstructorOption.New, bool isSingle = false, bool isNonLazy = false)
        {
            ConcreteType = concreteType;
            ConstructorOption = constructorOption;
            IsSingle = isSingle;
            IsNonLazy = isNonLazy;
        }
    }
        
    internal enum ConstructorOption : byte
    {
        New,
        NewComponentOnNewGameObject
    }
}