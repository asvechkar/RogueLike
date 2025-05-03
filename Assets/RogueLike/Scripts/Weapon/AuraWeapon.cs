using System.Collections;
using System.Collections.Generic;
using RogueLike.Scripts.Enemy;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Enemy;
using RogueLike.Scripts.Events.InputEvents;
using RogueLike.Scripts.Events.Weapon;
using RogueLike.Scripts.GameCore;
using UnityEngine;

namespace RogueLike.Scripts.Weapon
{
    public class AuraWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private Transform targetContainer;
        [SerializeField] private CircleCollider2D weaponCollider;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private readonly List<EnemyHealth> _enemiesInZone = new();
        private Coroutine _auraCoroutine;
        private float _range;

        protected override void Start()
        {
            WeaponType = WeaponType.Aura;
            spriteRenderer.enabled = false;
            weaponCollider.enabled = false;
            Activate();
        }

        private void OnEnable()
        {
            WeaponManager.AddWeapon(this);
            EventBus.Subscribe<OnEnemyDead>(RemoveDeadEnemies);
            EventBus.Subscribe<OnWeaponLevelUpdated>(ChangeLevel);
            EventBus.Subscribe<OnAttacked>(CheckZone);
        }

        private void OnDisable()
        {
            WeaponManager.RemoveWeapon(this);
            EventBus.Unsubscribe<OnEnemyDead>(RemoveDeadEnemies);
            EventBus.Unsubscribe<OnWeaponLevelUpdated>(ChangeLevel);
            EventBus.Unsubscribe<OnAttacked>(CheckZone);
        }
        
        private void ChangeLevel(OnWeaponLevelUpdated evt)
        {
            if (WeaponType == evt.WeaponType && CurrentLevel < MaxLevel)
            {
                LevelUp();
            }
        }

        private void RemoveDeadEnemies(OnEnemyDead evt)
        {
            _enemiesInZone.Remove(evt.Enemy);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                _enemiesInZone.Add(enemy);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                _enemiesInZone.Remove(enemy);
            }
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _range = WeaponStats[CurrentLevel - 1].Range;
            targetContainer.transform.localScale = Vector3.one * _range;
            weaponCollider.radius = _range / 3f;
        }

        private void CheckZone(OnAttacked evt)
        {
            if (evt.WeaponType != WeaponType) return;
            
            spriteRenderer.enabled = true;
            weaponCollider.enabled = true;

            StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            var duration = WeaponStats[CurrentLevel].Duration;
            while (duration > 0)
            {
                for (var i = 0; i < _enemiesInZone.Count; i++)
                {
                    if (CurrentLevel >= 5 && CurrentLevel <= 8)
                    {
                        if (_enemiesInZone[i].TryGetComponent(out EnemyMovement enemyMovement))
                        {
                            enemyMovement.Freeze(0.5f);
                        }
                    }
                    
                    _enemiesInZone[i].TakeDamage(_damage);
                }
                
                duration -= Time.deltaTime;
                
                yield return null;
            }
            
            spriteRenderer.enabled = false;
            weaponCollider.enabled = false;
        }

        public void Activate()
        {
            SetStats(0);
        }

        public void Deactivate()
        {
        }
    }
}