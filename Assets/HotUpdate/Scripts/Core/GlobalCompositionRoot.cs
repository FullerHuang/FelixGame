using UnityEngine;
using FelixGame.Core.DI;
using FelixGame.PlayerSystem;

namespace FelixGame.Core
{
    public class GlobalCompositionRoot : MonoBehaviour
    {
        public static GlobalCompositionRoot Instance { get; private set; }
        public IContainer Container { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeContainer();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeContainer()
        {
            Container = new SimpleContainer();

            // Register Global Services
            Container.Register<IPlayerModel, PlayerModel>();
            Container.Register<IPlayerService, PlayerService>();

            // You can add more global services here
            Debug.Log("Global Container Initialized.");
        }
    }
}
