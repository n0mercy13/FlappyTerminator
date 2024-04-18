namespace Codebase.Infrastructure
{
    public interface IManager : IResettable
    {
        void StartGameLoop();
        void StopGameLoop();
    }
}