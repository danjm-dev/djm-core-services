namespace DJM.CoreServices.DependencyInjection.Binding
{
    internal class GenericBinder<TBinding> : IGenericBind<TBinding>
    {
        private readonly BindingData _bindingData;
        private BindingOperationOrder? _latestOperationCompleted;

        internal GenericBinder(BindingData bindingData)
        {
            _bindingData = bindingData;
            _latestOperationCompleted = null;
        }

        // IBindTo
        public IBindFrom<TBinding> To<TImplementation>() where TImplementation : TBinding
        {
            ValidateOperationOrder(BindingOperationOrder.BindTo);
            _bindingData.ConcreteType = typeof(TImplementation);
            return this;
        }
        
        // IBindFrom
        public IBindScope<TBinding> FromNew()
        {
            ValidateOperationOrder(BindingOperationOrder.BindFrom);
            _bindingData.ConstructorOption = ConstructorOption.New;
            return this;
        }
        
        public IBindScope<TBinding> FromNewComponentOnNewGameObject()
        {
            ValidateOperationOrder(BindingOperationOrder.BindFrom);
            _bindingData.ConstructorOption = ConstructorOption.NewComponentOnNewGameObject;
            return this;
        }
        
        // IBindScope
        public IBindLazy<TBinding> AsSingle()
        {
            ValidateOperationOrder(BindingOperationOrder.BindScope);
            _bindingData.IsSingle = true;
            return this;
        }
        
        public void AsTransient()
        {
            ValidateOperationOrder(BindingOperationOrder.BindScope);
            _bindingData.IsSingle = false;
        }
        
        // IBindLazy
        public void NonLazy()
        {
            ValidateOperationOrder(BindingOperationOrder.BindLazy);
            _bindingData.IsNonLazy = true;
        }

        public void Lazy()
        {
            ValidateOperationOrder(BindingOperationOrder.BindLazy);
            _bindingData.IsNonLazy = false;
        }
        
        private void ValidateOperationOrder(BindingOperationOrder currentOperation)
        {
            if (_latestOperationCompleted.HasValue && 
                (byte)_latestOperationCompleted.Value >= (byte)currentOperation)
                throw new InvalidBindingOrderException
                (
                    currentOperation, 
                    _latestOperationCompleted.Value
                );
            _latestOperationCompleted = currentOperation;
        }
    }
    
    internal enum BindingOperationOrder : byte
    {
        BindTo,
        BindFrom,
        BindScope,
        BindLazy
    }
}