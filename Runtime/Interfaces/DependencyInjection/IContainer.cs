namespace DJM.CoreServices
{
    public interface IContainer
    {
        public IGenericBind<TBinding> Bind<TBinding>();
        public void RunValidation();
        public TBinding Resolve<TBinding>();
        public void Install(IInstaller installer);
    }
}