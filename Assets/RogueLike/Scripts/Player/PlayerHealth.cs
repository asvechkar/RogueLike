using System;
using System.Collections;
using RogueLike.Scripts.GameCore.Health;
using UnityEngine;

namespace RogueLike.Scripts.Player
{
    public class PlayerHealth: ObjectHealth
    {
        public Action OnHealthChanged;
        private WaitForSeconds _regenerationDelay = new WaitForSeconds(5f);
        private float _regenerationValue = 1f;
        
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            OnHealthChanged?.Invoke();
            if (CurrentHealth <= 0)
            {
                Debug.Log("Player is dead");
            }
        }

        private void Start()
        {
            StartCoroutine(Regenerate());
        }

        public void Heal(float healAmount)
        {
            TakeHealth(healAmount);
            OnHealthChanged?.Invoke();
        }

        private IEnumerator Regenerate()
        {
            while (true)
            {
                TakeHealth(_regenerationValue);
                OnHealthChanged?.Invoke();
                yield return _regenerationDelay;
            }
        }
    }
}