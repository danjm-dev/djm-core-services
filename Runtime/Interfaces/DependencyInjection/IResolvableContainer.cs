namespace DJM.CoreServices
{
    public interface IResolvableContainer
    {
        public TBinding Resolve<TBinding>();
    }
}