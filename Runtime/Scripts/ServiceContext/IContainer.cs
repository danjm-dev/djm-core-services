namespace DJM.CoreUtilities.ServiceContext
{
    public interface IContainer
    {
        public void Register<TInterface, TImplementation>() where TImplementation : TInterface;
        
        public TInterface Resolve<TInterface>();
    }
}