using System.Collections.Generic;
using RogueLike.Scripts.Enemy;
using UnityEngine;

using Random = UnityEngine.Random;

namespace RogueLike.Scripts.Player.Weapon
{
    public abstract class BaseWeapon: MonoBehaviour
    {
        [SerializeField] private List<WeaponStats> _weaponStats = new();
        protected float _damage;
        
        private int _currentLevel = 1;
        private readonly int _maxLevel = 8;

        public List<WeaponStats> WeaponStats => _weaponStats;
        public float Damage => _damage;
        public int CurrentLevel => _currentLevel;
        public int MaxLevel => _maxLevel;

        protected virtual void Start()
        {
            SetStats(0);
        }

        public virtual void LevelUp()
        {
            if (_currentLevel < _maxLevel)
                _currentLevel++;
            
            SetStats(_currentLevel - 1);
        }

        protected virtual void SetStats(int value)
        {
            _damage = _weaponStats[value].Damage;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                var randomDamage = Random.Range(_damage / 2f, _damage * 1.5f);
                enemy.TakeDamage(randomDamage);
            }
        }
    }
}