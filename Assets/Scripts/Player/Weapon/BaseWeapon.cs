using System.Collections.Generic;
using Enemy;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Player.Weapon
{
    public abstract class BaseWeapon: MonoBehaviour
    {
        [SerializeField] private List<WeaponStats> _weaponStats = new List<WeaponStats>();
        [SerializeField] private float damage;
        
        private DiContainer _diContainer;
        private int _currentLevel = 1;
        private readonly int _maxLevel = 8;

        public List<WeaponStats> WeaponStats => _weaponStats;
        public float Damage => damage;
        public int CurrentLevel => _currentLevel;
        public int MaxLevel => _maxLevel;

        private void Awake()
        {
            _diContainer.Inject(this);
        }

        private void Start()
        {
            SetStats(0);
        }

        public virtual void LevelUp()
        {
            if (_currentLevel < _maxLevel)
                _currentLevel++;
            
            SetStats(_currentLevel - 1);
        }

        protected void SetStats(int value)
        {
            damage = _weaponStats[value].Damage;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                var randomDamage = Random.Range(damage / 2f, damage * 1.5f);
                enemy.TakeDamage(randomDamage);
            }
        }

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
    }
}