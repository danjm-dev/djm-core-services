using DJM.CoreServices.Bootstrap;

namespace DJM.CoreServices.API
{
    public static class DJMPersistantServices
    {
        public static T Resolve<T>()
        {
            return PersistantServiceManager.Instance.Container.Resolve<T>();
        }
    }
}