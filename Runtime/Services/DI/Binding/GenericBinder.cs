namespace DJM.CoreServices.Services.DI.Binding
{
    public interface IBindTo<T>
    {
        IBindFrom<T> To<TImplementation>() where TImplementation : T;
    }

    public interface IBindFrom<T>
    {
        IBindScope<T> FromNew();
        IBindScope<T> FromNewComponentOnNewGameObject();
    }

    public interface IBindScope<T>
    {
        IBindLazy<T> AsSingle();
        void AsTransient();
    }

    public interface IBindLazy<T>
    {
        void NonLazy();
        void Lazy();
    }
    
    internal enum BindingOperationOrder : byte
    {
        BindTo,
        BindFrom,
        BindScope,
        BindLazy
    }
    
    public class GenericBinder<TBinding> : IBindTo<TBinding>, IBindFrom<TBinding>, IBindScope<TBinding>, IBindLazy<TBinding>
    {
        private readonly BindingUpdateHandler _bindingUpdateHandler;
        private BindingOperationOrder? _latestOperationCompleted;

        public GenericBinder(BindingUpdateHandler bindingUpdateHandler)
        {
            _bindingUpdateHandler = bindingUpdateHandler;
            _latestOperationCompleted = null;
        }

        // IBindTo
        public IBindFrom<TBinding> To<TImplementation>() where TImplementation : TBinding
        {
            ValidateOperationOrder(BindingOperationOrder.BindTo);
            _bindingUpdateHandler.SetConcreteType(typeof(TImplementation));
            return this;
        }
        
        // IBindFrom
        public IBindScope<TBinding> FromNew()
        {
            ValidateOperationOrder(BindingOperationOrder.BindFrom);
            _bindingUpdateHandler.SetConstructorOption(ConstructorOption.New);
            return this;
        }
        
        public IBindScope<TBinding> FromNewComponentOnNewGameObject()
        {
            ValidateOperationOrder(BindingOperationOrder.BindFrom);
            _bindingUpdateHandler.SetConstructorOption(ConstructorOption.NewComponentOnNewGameObject);
            return this;
        }
        
        // IBindScope
        public IBindLazy<TBinding> AsSingle()
        {
            ValidateOperationOrder(BindingOperationOrder.BindScope);
            _bindingUpdateHandler.SetIsSingle(true);
            return this;
        }
        
        public void AsTransient()
        {
            ValidateOperationOrder(BindingOperationOrder.BindScope);
            _bindingUpdateHandler.SetIsSingle(false);
        }
        
        // IBindLazy
        public void NonLazy()
        {
            ValidateOperationOrder(BindingOperationOrder.BindLazy);
            _bindingUpdateHandler.SetIsNonLazy(true);
        }

        public void Lazy()
        {
            ValidateOperationOrder(BindingOperationOrder.BindLazy);
            _bindingUpdateHandler.SetIsNonLazy(false);
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
}