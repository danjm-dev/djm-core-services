using System;

namespace DJM.CoreUtilities
{
    public interface IContainer
    {
        public void RegisterTransient<TInterface, TImplementation>() where TImplementation : TInterface;
        public void RegisterSingle<TInterface, TImplementation>() where TImplementation : TInterface;
        public object Resolve(Type type);
    }
}