namespace DJM.CoreServices.PersistantServices
{
    public static class DJMServiceLocator
    {
        public static T Resolve<T>()
        {
            return PersistantServiceManager.Instance.Container.Resolve<T>();
        }
    }
}