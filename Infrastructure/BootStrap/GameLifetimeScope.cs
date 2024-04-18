using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Codebase.StaticData;
using Codebase.Logic;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private GameConfig _gameConfig;

        private void OnValidate()
        {
            if(_sceneData == null)
                throw new ArgumentNullException(nameof(_sceneData));

            if(_gameConfig == null)
                throw new ArgumentNullException(nameof(_gameConfig));
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterEntryPoint<Bootstrap>(Lifetime.Singleton)
                .AsSelf();

            builder
                .RegisterInstance(_sceneData);
            builder 
                .RegisterInstance(_gameConfig.PlayerConfig);
            builder
                .RegisterInstance(_gameConfig.EnemyConfig);
            builder
                .RegisterInstance(_gameConfig.ProjectileConfig);
            builder
                .RegisterInstance(_gameConfig.SpawnConfig);
            builder
                .RegisterInstance(_gameConfig.ViewConfig);
            builder
                .Register<PlayerManager>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<EnemyManager>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<ViewManager>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<GameManager>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<GameFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder 
                .Register<GamePool>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<InputControls>(Lifetime.Singleton)
                .AsSelf();
            builder
                .Register<InputService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<BoundaryService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<RandomService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<PauseService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<GameStateMachine>(Lifetime.Singleton)
                .AsSelf();
            builder
                .Register<LoadLevelState>(Lifetime.Singleton)
                .AsSelf();
            builder
                .Register<GameReloadState>(Lifetime.Singleton)
                .AsSelf();
            builder
                .Register<GameLoopState>(Lifetime.Singleton)
                .AsSelf();
            builder
                .Register<GameOverState>(Lifetime.Singleton)
                .AsSelf();

            builder
                .Register<Energy>(Lifetime.Transient) 
                .AsSelf();
        }
    } 
}
