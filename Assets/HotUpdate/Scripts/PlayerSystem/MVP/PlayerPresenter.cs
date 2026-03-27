using UniRx;
using System;

namespace FelixGame.PlayerSystem
{
    public class PlayerPresenter : IDisposable
    {
        private readonly IPlayerService _playerService;
        private readonly IPlayerView _playerView;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public PlayerPresenter(IPlayerService playerService, IPlayerView playerView)
        {
            _playerService = playerService;
            _playerView = playerView;

            Initialize();
        }

        public void Initialize()
        {
            // Subscribe to model changes
            _playerService.PlayerModel.Level
                .Subscribe(_playerView.SetLevel)
                .AddTo(_disposables);

            _playerService.PlayerModel.Name
                .Subscribe(_playerView.SetName)
                .AddTo(_disposables);

            // Subscribe to view events
            _playerView.OnLevelUpClicked
                .Subscribe(_ => _playerService.AddExperience(100))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
