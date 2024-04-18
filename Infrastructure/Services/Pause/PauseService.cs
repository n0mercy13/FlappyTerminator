using UnityEngine;

namespace Codebase.Infrastructure.Services
{
    public class PauseService : IPauseService
    {
        private readonly float _pauseTimeScale = 0.1f;
        private readonly float _unPauseTimeScale = 1.0f;

        public void Pause() => 
            Time.timeScale = _pauseTimeScale;

        public void UnPause() => 
            Time.timeScale = _unPauseTimeScale;
    }
}
