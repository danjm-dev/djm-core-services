using System;

namespace DJM.CoreServices.DependencyInjection
{
    internal class BindingData
    {
        public Type ConcreteType;
        public ConstructorOption ConstructorOption;
        public bool IsSingle;
        public bool IsNonLazy;

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