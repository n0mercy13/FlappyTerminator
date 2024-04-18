namespace Codebase.Infrastructure
{
    public interface IManager : IResettable
    {
        void Start();
        void Stop();
    }
}