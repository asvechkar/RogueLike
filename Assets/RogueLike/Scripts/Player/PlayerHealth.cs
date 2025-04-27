using System.Collections;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using RogueLike.Scripts.Events.Loot;
using RogueLike.Scripts.Events.Player;
using RogueLike.Scripts.GameCore.Health;
using UnityEngine;

namespace RogueLike.Scripts.Player
{
    public class PlayerHealth: ObjectHealth
    {
        private readonly WaitForSeconds _regenerationDelay = new(5f);
        private readonly float _regenerationValue = 1f;
        
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            
            EventBus.Invoke(new OnHealthChanged(MaxHealth, CurrentHealth));
            
            if (CurrentHealth <= 0)
            {
                EventBus.Invoke(new OnGameOver("You lose :("));
            }
        }

        private void Start()
        {
            StartCoroutine(Regenerate());
        }
        
        private void OnEnable()
        {
            EventBus.Subscribe<OnPlayerLevelChanged>(GetFullHealth);
            EventBus.Subscribe<OnPickupHeart>(HealFromLoot);
        }
        
        private void OnDisable()
        {
            EventBus.Unsubscribe<OnPlayerLevelChanged>(GetFullHealth);
            EventBus.Unsubscribe<OnPickupHeart>(HealFromLoot);
        }

        private void Heal(float healAmount)
        {
            TakeHealth(healAmount);
            EventBus.Invoke(new OnHealthChanged(MaxHealth, CurrentHealth));
        }

        private void GetFullHealth(OnPlayerLevelChanged evt)
        {
            TakeHealth(evt.Level * 10);
            EventBus.Invoke(new OnHealthChanged(MaxHealth, CurrentHealth));
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
                EventBus.Invoke(new OnHealthChanged(MaxHealth, CurrentHealth));
                yield return _regenerationDelay;
            }
        }
    }
}