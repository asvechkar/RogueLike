using System;
using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Player;
using RogueLike.Scripts.GameCore.SaveSystem;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class SkillManager : MonoBehaviour
    {
        public int SkillPoints { get; private set; }
        
        [Inject] private readonly GameData _gameData;

        private void Awake()
        {
            SkillPoints = _gameData.skillPoints;
        }

        public void AddSkillPoints(int amount)
        {
            SkillPoints += amount;
            EventBus.Invoke(new OnChangeSkillPoints(SkillPoints));
        }

        public void SpendSkillPoints(int amount)
        {
            SkillPoints -= amount;
            EventBus.Invoke(new OnChangeSkillPoints(SkillPoints));
        }

        private void UpdateSkillPoints(OnPlayerLevelChanged evt)
        {
            AddSkillPoints(1);
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnPlayerLevelChanged>(UpdateSkillPoints);
        }
        
        private void OnDisable()
        {
            EventBus.Unsubscribe<OnPlayerLevelChanged>(UpdateSkillPoints);
        }
    }
}