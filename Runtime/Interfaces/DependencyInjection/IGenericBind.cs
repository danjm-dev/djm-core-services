namespace DJM.CoreServices
{
    public interface IGenericBindNon<T> : IBindTo<T>, IBindFrom<T>, IBindScope<T>, IBindNonLazy<T> { }
    
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
        IBindNonLazy<T> AsSingle();
        void AsTransient();
    }

    public interface IBindNonLazy<T>
    {
        void NonLazy();
    }
}