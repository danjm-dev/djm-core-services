namespace DJM.CoreServices
{
    public interface IBindableContainer
    {
        public IGenericBind<TBinding> Bind<TBinding>();
    }
}