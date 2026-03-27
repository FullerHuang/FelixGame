using UnityEngine;
using FelixGame.Core.DI;
using FelixGame.PlayerSystem;

namespace FelixGame.Core
{
    public class SceneCompositionRoot : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerView;

        private PlayerPresenter _playerPresenter;

        private void Start()
        {
            if (GlobalCompositionRoot.Instance == null)
            {
                Debug.LogError("GlobalCompositionRoot not found.");
                return;
            }

            var container = GlobalCompositionRoot.Instance.Container;

            // Resolve Global Service
            var playerService = container.Resolve<IPlayerService>();

            // Create and Initialize Presenter
            _playerPresenter = new PlayerPresenter(playerService, _playerView);
            _playerPresenter.Initialize();

            Debug.Log("Scene Root Initialized.");
        }

        private void OnDestroy()
        {
            if (_playerPresenter != null)
            {
                _playerPresenter.Dispose();
            }
        }
    }
}
