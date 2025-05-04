using System;
using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using RogueLike.Scripts.Events.Player;
using RogueLike.Scripts.GameCore.SaveSystem;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class ExperienceManager : MonoBehaviour
    {
        private int _currentExperience;
        private int _experienceToUp = 5;

        public int CurrentLevel { get; private set; } = 1;

        [Inject] private readonly GameData _gameData;

        private void Awake()
        {
            CurrentLevel = _gameData.playerData.level;
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnPickupExperience>(Add);
            EventBus.Invoke(new OnPlayerLevelChanged(CurrentLevel));
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnPickupExperience>(Add);
        }

        private void Add(OnPickupExperience evt)
        {
            if (evt.ExperienceValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(evt.ExperienceValue));
            }
            _currentExperience += evt.ExperienceValue;
            
            if (_currentExperience >= _experienceToUp)
            {
                LevelUp();
            }
            
            EventBus.Invoke(new OnExperienceChanged(_currentExperience, _experienceToUp));
        }

        private void LevelUp()
        {
            _currentExperience = 0;
            CurrentLevel++;

            switch (CurrentLevel)
            {
                case <= 20:
                    _experienceToUp += 10;
                    break;
                case <= 40:
                    _experienceToUp += 20;
                    break;
                case <= 60:
                    _experienceToUp += 30;
                    break;
                case <= 80:
                    _experienceToUp += 40;
                    break;
            }
            
            EventBus.Invoke(new OnPlayerLevelChanged(CurrentLevel));
        }
    }
}