using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace FelixGame.PlayerSystem
{
    public interface IPlayerView
    {
        void SetLevel(int level);
        void SetName(string name);
        IObservable<Unit> OnLevelUpClicked { get; }
    }

    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private Text _levelText;
        [SerializeField] private Text _nameText;
        [SerializeField] private Button _levelUpButton;

        public IObservable<Unit> OnLevelUpClicked => _levelUpButton.OnClickAsObservable();

        public void SetLevel(int level)
        {
            if (_levelText != null)
                _levelText.text = $"Level: {level}";
        }

        public void SetName(string name)
        {
            if (_nameText != null)
                _nameText.text = $"Name: {name}";
        }
    }
}
