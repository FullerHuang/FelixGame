
namespace FelixGame.PlayerSystem
{
    public interface IPlayerService
    {
        IPlayerModel PlayerModel { get; }
        void AddExperience(int amount);
    }

    public class PlayerService : IPlayerService
    {
        public IPlayerModel PlayerModel { get; }

        public PlayerService(IPlayerModel playerModel)
        {
            PlayerModel = playerModel;
        }

        public void AddExperience(int amount)
        {
            // Simple logic: every 100 exp = 1 level
            if (amount >= 100)
            {
                PlayerModel.UpdateLevel(amount / 100);
            }
        }
    }
}
