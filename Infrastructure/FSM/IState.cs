namespace Codebase.Infrastructure
{
    public interface IState : IExitableState 
    {
        void Enter();
    }
}