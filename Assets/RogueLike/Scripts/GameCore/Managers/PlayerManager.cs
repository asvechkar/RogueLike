using System;
using Reflex.Attributes;
using RogueLike.Scripts.GameCore.SaveSystem;
using RogueLike.Scripts.Player;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public float MaxHealth { get; private set; }
        public float Speed { get; private set; }
        public float HealPoints { get; private set; }
        
        [Inject] private readonly GameData _gameData;

        private void Awake()
        {
            MaxHealth = _gameData.playerData.maxHealth == 0 ? 100f : _gameData.playerData.maxHealth;
            Speed = _gameData.playerData.speed == 0 ? 4f : _gameData.playerData.speed;
            HealPoints = _gameData.playerData.healPoints == 0 ? 1f : _gameData.playerData.healPoints;
        }

        public void SetMaxHealth(float maxHealth)
        {
            MaxHealth = maxHealth;
        }
        
        public void SetSpeed(float speed)
        {
            Speed = speed;
        }
        
        public void SetHealPoints(float healPoints)
        {
            HealPoints = healPoints;
        }

        public void UpgradeSkill(PlayerSkillType playerSkillType)
        {
            switch (playerSkillType)
            {
                case PlayerSkillType.Health:
                    SetMaxHealth(MaxHealth + 10);
                    break;
                case PlayerSkillType.HealthRegen:
                    SetHealPoints(HealPoints + 1);
                    break;
                case PlayerSkillType.Speed:
                    default:
                    SetSpeed(Speed + 1);
                    break;
            }
        }

        public float GetSkillValue(PlayerSkillType playerSkillType)
        {
            return playerSkillType switch
            {
                PlayerSkillType.Health => MaxHealth,
                PlayerSkillType.HealthRegen => HealPoints,
                PlayerSkillType.Speed => Speed,
                _ => 0
            };
        }
    }
}