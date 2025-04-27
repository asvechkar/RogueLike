using System;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using RogueLike.Scripts.Events.Player;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class ExperienceManager : MonoBehaviour
    {
        private int _currentExperience = 0;
        private int _experienceToUp = 5;
        private int _currentLevel = 1;

        private void OnEnable()
        {
            EventBus.Subscribe<OnPickupExperience>(Add);
            EventBus.Invoke(new OnPlayerLevelChanged(_currentLevel));
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
            _currentLevel++;

            switch (_currentLevel)
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
            
            EventBus.Invoke(new OnPlayerLevelChanged(_currentLevel));
        }
    }
}