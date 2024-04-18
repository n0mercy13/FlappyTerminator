using System;
using System.Collections.Generic;
using Codebase.View;

namespace Codebase.Infrastructure
{
    public partial class ViewManager
    {
        private readonly IGameFactory _gameFactory;
        private readonly IGameManager _gameManager;
        private IReadOnlyList<ElementView> _views;

        public ViewManager(IGameFactory gameFactory, IGameManager gameManager)
        {
            _gameFactory = gameFactory;
            _gameManager = gameManager;

            _gameManager.ScoreUpdated += OnScoreUpdated;
        }

        private void OnScoreUpdated(int score)
        {
            ScoreView scoreView = Get<ScoreView>();
            scoreView.Refresh(score);
        }

        private TView Get<TView>() where TView : ElementView
        {
            foreach(ElementView view in _views)
            {
                if(view is TView viewToGet)
                    return viewToGet;
            }

            throw new InvalidOperationException(
                $"Unable to find view of type: {typeof(TView)}!");
        }

        private void Open<TView>() where TView : ElementView
        {
            TView view = Get<TView>();
            view.Open();
        }

        private void Close<TView>() where TView : ElementView
        {
            TView view = Get<TView>();
            view.Close();
        }

        private void CloseAllViews()
        {
            foreach (ElementView view in _views)
                view.Close();
        }
    }

    public partial class ViewManager : IViewManager
    {
        public void StartGameLoop()
        {
            Open<ScoreView>();
        }

        public void StopGameLoop()
        {
            Open<GameOverPopupView>();
        }

        public void Reset()
        {
            CloseAllViews();
        }
    }

    public partial class ViewManager : IInitializable
    {
        public void Initialize()
        {
            _views = _gameFactory.CreateViews();
            CloseAllViews();
        }
    }

    public partial class ViewManager : IDisposable
    {
        public void Dispose()
        {
            _gameManager.ScoreUpdated -= OnScoreUpdated;
        }
    }
}
