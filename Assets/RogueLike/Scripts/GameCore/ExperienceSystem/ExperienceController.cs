using System;
using RogueLike.Scripts.Events;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.ExperienceSystem
{
    public class ExperienceController : MonoBehaviour
    {
        [SerializeField] private GameObject upgradeWindow;
        
        private int _currentExperience = 0;
        private int _experienceToUp = 5;
        private int _currentLevel = 1;

        private void OnEnable()
        {
            EventBus.Subscribe<OnExperiencePickup>(Add);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnExperiencePickup>(Add);
        }

        private void Add(OnExperiencePickup evt)
        {
            if (evt.ExperienceValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(evt.ExperienceValue));
            }
            _currentExperience += evt.ExperienceValue;
            
            EventBus.Invoke(new OnExperienceChanged(_currentExperience, _experienceToUp));

            if (_currentExperience >= _experienceToUp)
            {
                LevelUp();
            }
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
            
            EventBus.Invoke(new OnExperienceChanged(_currentExperience, _experienceToUp));
            EventBus.Invoke(new OnPlayerLevelChanged(_currentLevel));
        }
    }
}