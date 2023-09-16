namespace DJM.CoreServices
{
    public interface IGenericBind<T> : IBindTo<T>, IBindFrom<T>, IBindScope<T>, IBindLazy<T> { }
    
    public interface IBindTo<T>
    {
        IBindFrom<T> To<TImplementation>() where TImplementation : T;
    }

    public interface IBindFrom<T>
    {
        IBindScope<T> FromNew();
        IBindScope<T> FromNewComponentOnNewGameObject();
    }

    public interface IBindScope<T>
    {
        IBindLazy<T> AsSingle();
        void AsTransient();
    }

    public interface IBindLazy<T>
    {
        void NonLazy();
        void Lazy();
    }
}