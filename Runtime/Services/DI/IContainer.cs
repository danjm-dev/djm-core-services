using DJM.CoreServices.Services.DI.Binding;
using DJM.CoreServices.Services.DI.Installer;

namespace DJM.CoreServices.Services.DI
{
    public interface IContainer
    {
        public GenericBinder<TBinding> Bind<TBinding>();
        public void RunValidation();
        public TBinding Resolve<TBinding>();
        public void Install(IInstaller installer);
    }
}