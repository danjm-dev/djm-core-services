namespace DJM.CoreServices.ServiceLocator
{
    public static class DJMServiceLocator
    {
        public static T Resolve<T>()
        {
            return PersistantServiceManager.Instance.Container.Resolve<T>();
        }
    }
}