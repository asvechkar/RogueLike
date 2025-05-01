using System;
using System.Collections;
using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using RogueLike.Scripts.Events.Loot;
using RogueLike.Scripts.Events.Player;
using RogueLike.Scripts.GameCore.Health;
using RogueLike.Scripts.GameCore.Managers;
using UnityEngine;

namespace RogueLike.Scripts.Player
{
    public class PlayerHealth: ObjectHealth
    {
        private readonly WaitForSeconds _regenerationDelay = new(5f);
        private float _regenerationValue = 1f;
        
        [Inject] private PlayerManager _playerManager;
        
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            
            EventBus.Invoke(new OnPlayerHealthChanged(CurrentHealth, MaxHealth, _regenerationValue));
            
            if (CurrentHealth <= 0)
            {
                EventBus.Invoke(new OnGameOver("You lost :("));
            }
        }

        private void Start()
        {
            _playerManager.SetMaxHealth(MaxHealth);
            _playerManager.SetHealPoints(_regenerationValue);
            
            EventBus.Invoke(new OnPlayerHealthChanged(CurrentHealth, MaxHealth, _regenerationValue));
            StartCoroutine(Regenerate());
        }
        
        private void OnEnable()
        {
            EventBus.Subscribe<OnPlayerLevelChanged>(GetFullHealth);
            EventBus.Subscribe<OnPickupHeart>(HealFromLoot);
            EventBus.Subscribe<OnPlayerSkillChanged>(UpdateHealth);
        }
        
        private void OnDisable()
        {
            EventBus.Unsubscribe<OnPlayerLevelChanged>(GetFullHealth);
            EventBus.Unsubscribe<OnPickupHeart>(HealFromLoot);
            EventBus.Unsubscribe<OnPlayerSkillChanged>(UpdateHealth);
        }

        private void Heal(float healAmount)
        {
            TakeHealth(healAmount);
            EventBus.Invoke(new OnPlayerHealthChanged(CurrentHealth, MaxHealth, _regenerationValue));
        }

        private void GetFullHealth(OnPlayerLevelChanged evt)
        {
            TakeHealth(evt.Level * 10);
            EventBus.Invoke(new OnPlayerHealthChanged(CurrentHealth, MaxHealth, _regenerationValue));
        }

        private void HealFromLoot(OnPickupHeart evt)
        {
            Heal(evt.Health);
        }

        private IEnumerator Regenerate()
        {
            while (true)
            {
                TakeHealth(_regenerationValue);
                EventBus.Invoke(new OnPlayerHealthChanged(CurrentHealth, MaxHealth, _regenerationValue));
                yield return _regenerationDelay;
            }
        }

        private void UpdateHealth(OnPlayerSkillChanged evt)
        {
            switch (evt.Skill)
            {
                case PlayerSkillType.Health:
                    SetMaxHealth(_playerManager.MaxHealth);
                    break;
                case PlayerSkillType.HealthRegen:
                    _regenerationValue = _playerManager.HealPoints;
                    break;
            }

            EventBus.Invoke(new OnPlayerHealthChanged(CurrentHealth, MaxHealth, _regenerationValue));
        }
    }
}