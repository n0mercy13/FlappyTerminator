namespace Codebase.Infrastructure
{
    public interface IInitializable
    {
        void Initialize();
    }

    public interface IInitializable<TArgument>
    {
        void Initialize(TArgument argument);
    }
}
