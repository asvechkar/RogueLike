using System.Collections.Generic;
using Reflex.Attributes;
using RogueLike.Scripts.Enemy;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Weapon;
using RogueLike.Scripts.GameCore.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RogueLike.Scripts.Weapon
{
    public abstract class BaseWeapon: MonoBehaviour
    {
        [SerializeField] private List<WeaponStats> weaponStats = new();

        [Inject] protected WeaponManager WeaponManager;
        
        protected float _damage;
        public WeaponType WeaponType { get; protected set; }
        
        private int _currentLevel = 1;
        private readonly int _maxLevel = 8;

        protected List<WeaponStats> WeaponStats => weaponStats;
        public float Damage => _damage;
        public int CurrentLevel => _currentLevel;
        public int MaxLevel => _maxLevel;

        protected virtual void Start()
        {
            SetStats(0);
        }

        protected virtual void LevelUp()
        {
            if (_currentLevel < _maxLevel)
                _currentLevel++;
            
            SetStats(_currentLevel - 1);
            Debug.Log($"Bow Current Level: {_currentLevel}");
        }

        protected virtual void SetStats(int value)
        {
            _damage = weaponStats[value].Damage;
            EventBus.Invoke(new OnWeaponLevelChanged(WeaponType, _currentLevel));
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                var randomDamage = Random.Range(_damage / 2f, _damage * 1.5f);
                enemy.TakeDamage(randomDamage);
            }
        }

        public float GetCooldown()
        {
            return weaponStats[CurrentLevel - 1].TimeBetweenAttack;
        }
        
        public bool CanUpgrade()
        {
            return CurrentLevel < MaxLevel;
        }
    }
}