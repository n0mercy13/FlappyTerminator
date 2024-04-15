using System;

namespace Codebase.Logic
{
    public interface IEnergy
    {
        event Action<int, int> Changed;
        event Action Depleted;

        void Decrease(int amount);
        void Increase(int amount);
        void Refresh();
    }
}