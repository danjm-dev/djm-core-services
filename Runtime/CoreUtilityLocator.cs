namespace DJM.CoreUtilities
{
    public class CoreUtilityLocator : PersistantSingletonComponent<CoreUtilityLocator>
    {
        public SceneLoader SceneLoader { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            SceneLoader = GetComponentInChildren<SceneLoader>();
        }
    }
}
