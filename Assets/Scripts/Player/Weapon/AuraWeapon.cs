using System.Collections;
using System.Collections.Generic;
using Enemy;
using GameCore;
using UnityEngine;

namespace Player.Weapon
{
    public class AuraWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private Transform targetContainer;
        [SerializeField] private CircleCollider2D weaponCollider;

        private List<EnemyHealth> _enemiesInZone = new();
        private WaitForSeconds _timeBetweenAttacks;
        private Coroutine _auraCoroutine;
        private float _range;

        protected override void Start()
        {
            Activate();
            LevelUp();
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
            _timeBetweenAttacks = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _range = WeaponStats[CurrentLevel - 1].Range;
            targetContainer.transform.localScale = Vector3.one * _range;
            weaponCollider.radius = _range / 3f;
        }

        private IEnumerator CheckZone()
        {
            while (true)
            {
                for (int i = 0; i < _enemiesInZone.Count; i++)
                {
                    _enemiesInZone[i].TakeDamage(_damage);
                }
                
                yield return _timeBetweenAttacks;
            }
        }

        public void Activate()
        {
            SetStats(0);
            _auraCoroutine = StartCoroutine(CheckZone());
        }

        public void Deactivate()
        {
            if (_auraCoroutine == null) return;
            
            StopCoroutine(_auraCoroutine);
        }
    }
}