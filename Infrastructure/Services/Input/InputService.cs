using System;
using UnityEngine.InputSystem;

namespace Codebase.Infrastructure
{
    public partial class InputService
    {
        private readonly InputControls _inputs;

        public InputService(InputControls inputs)
        {
            _inputs = inputs;
            _inputs.Enable();

            _inputs.Gameplay.Boost.performed += OnBoostPerformed;
            _inputs.Gameplay.Fire.performed += OnFirePerformed;
        }

        private void OnBoostPerformed(InputAction.CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Performed))
                BoostPressed.Invoke();
        }

        private void OnFirePerformed(InputAction.CallbackContext context)
        {
            if(context.phase.Equals(InputActionPhase.Performed))
                FirePressed.Invoke();
        }
    } 

    public partial class InputService : IGameplayInput
    {
        public event Action BoostPressed = delegate { };
        public event Action FirePressed = delegate { };
    }

    public partial class InputService : IDisposable
    {
        public void Dispose()
        {
            _inputs.Gameplay.Boost.performed -= OnBoostPerformed;
            _inputs.Gameplay.Fire.performed -= OnFirePerformed;
        }
    }
}
