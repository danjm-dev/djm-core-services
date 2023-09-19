namespace DJM.CoreServices.ServiceLocator
{
    public static class DJMServiceLocator
    {
        public static T Resolve<T>()
        {
            return ServiceManager.Instance.Container.Resolve<T>();
        }
    }
}