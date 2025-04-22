using System.Collections;
using RogueLike.Scripts.Events;
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
                Debug.Log("Player is dead");
            }
        }

        private void Start()
        {
            StartCoroutine(Regenerate());
        }
        
        private void OnEnable()
        {
            EventBus.Subscribe<OnPlayerLevelChanged>(GetFullHealth);
        }
        
        private void OnDisable()
        {
            EventBus.Unsubscribe<OnPlayerLevelChanged>(GetFullHealth);
        }

        public void Heal(float healAmount)
        {
            TakeHealth(healAmount);
            EventBus.Invoke(new OnHealthChanged(MaxHealth, CurrentHealth));
        }

        private void GetFullHealth(OnPlayerLevelChanged evt)
        {
            TakeHealth(evt.Level * 10);
            EventBus.Invoke(new OnHealthChanged(MaxHealth, CurrentHealth));
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