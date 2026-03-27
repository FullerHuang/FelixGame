using UniRx;

namespace FelixGame.PlayerSystem
{
    public interface IPlayerModel
    {
        IReadOnlyReactiveProperty<int> Level { get; }
        IReadOnlyReactiveProperty<string> Name { get; }
        void UpdateLevel(int delta);
    }

    public class PlayerModel : IPlayerModel
    {
        //readonly: 只读属性，初始化后不能修改(不能重新赋值该ReactiveProperty类，只能调用方法)
        private readonly IntReactiveProperty _level = new IntReactiveProperty(1);
        private readonly StringReactiveProperty _name = new StringReactiveProperty("New Player");

        public IReadOnlyReactiveProperty<int> Level => _level;
        public IReadOnlyReactiveProperty<string> Name => _name;

        public void UpdateLevel(int delta)
        {
            _level.Value += delta;
        }
    }
}
