using System;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Health
{
    public abstract class ObjectHealth: MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth;

        public float MaxHealth => maxHealth;
        public float CurrentHealth { get; private set; }

        private void OnEnable() => CurrentHealth = maxHealth;

        public void SetMaxHealth(float health)
        {
            maxHealth = health;
            CurrentHealth = maxHealth;
        }
        
        public virtual void TakeDamage(float damage)
        {
            if (damage <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage), damage, "Damage must be positive.");
            }
            
            CurrentHealth -= damage;
        }

        public void TakeHealth(float health)
        {
            if (health <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(health), health, "Health must be positive.");
            }
            
            CurrentHealth += health;
            
            if (CurrentHealth > maxHealth) CurrentHealth = maxHealth;
        }
    }
}