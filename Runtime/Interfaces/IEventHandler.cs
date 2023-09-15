namespace DJM.CoreServices
{
    public interface IEventHandler
    {
        public void Initialize();
        public void Dispose();
    }
}