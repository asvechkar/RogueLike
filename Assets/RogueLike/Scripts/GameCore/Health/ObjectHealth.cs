using System;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Health
{
    public abstract class ObjectHealth: MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        
        private void OnEnable() => currentHealth = maxHealth;
        
        public virtual void TakeDamage(float damage)
        {
            if (damage <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage), damage, "Damage must be positive.");
            }
            
            currentHealth -= damage;
        }

        public void TakeHealth(float health)
        {
            if (health <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(health), health, "Health must be positive.");
            }
            
            currentHealth += health;
            
            if (currentHealth > maxHealth) currentHealth = maxHealth;
        }
    }
}