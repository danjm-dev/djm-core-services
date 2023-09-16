namespace DJM.CoreServices
{
    public interface IBindableContainer
    {
        public IGenericBindNon<TBinding> Bind<TBinding>();
    }
}