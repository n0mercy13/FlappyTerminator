using System;

namespace Codebase.Infrastructure
{
    public interface IUIInput
    {
        event Action ContinuePressed;
    }
}