using System;

namespace Codebase.Infrastructure
{
    public interface IGameplayInput
    {
        event Action BoostPressed;
        event Action FirePressed;
    }
}