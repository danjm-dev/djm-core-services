namespace DJM.CoreServices
{
    public interface IBindableContainer
    {
        public IBindTo<TBinding> Bind<TBinding>();
    }
}