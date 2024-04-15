using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebase.Infrastructure
{
    public class EnemyManager
    {
        private readonly IGameFactory _gameFactory;
        private readonly IBoundaryService _boundaryService;
        private readonly IRandomService _randomService;

        public EnemyManager(IGameFactory gameFactory, IBoundaryService boundaryService, IRandomService randomService)
        {
            _gameFactory = gameFactory;
            _boundaryService = boundaryService;
            _randomService = randomService;
        }
    }
}
