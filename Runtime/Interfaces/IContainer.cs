using System;

namespace DJM.CoreServices
{
    public interface IContainer
    {
        public void RegisterTransient<TInterface, TImplementation>() where TImplementation : TInterface;
        public void RegisterSingle<TInterface, TImplementation>() where TImplementation : TInterface;
        public object Resolve(Type type);
    }
}