using System;
using System.Collections;
using GameCore;
using GameCore.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player.Weapon.Suriken
{
    public class SurikenWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool objectPool;
        [SerializeField] private Transform container;
        [SerializeField] private LayerMask layerMask;
        
        private WaitForSeconds _timeBetweenAttacks;
        private Coroutine _surikenCoroutine;
        private float _duration, _speed, _range;
        private Vector3 _direction;

        public float Duration => _duration;

        public float Speed => _speed;

        public Vector3 Direction => _direction;

        private void OnEnable()
        {
            Activate();
        }

        public void Activate()
        {
            SetStats(0);
            _surikenCoroutine = StartCoroutine(SpawnSuriken());
        }

        public void Deactivate()
        {
            if (_surikenCoroutine != null)
                StopCoroutine(_surikenCoroutine);
        }

        protected override void SetStats(int value)
        {
            base.SetStats(CurrentLevel - 1);
            
            _timeBetweenAttacks = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _speed = WeaponStats[CurrentLevel - 1].Speed;
            _range = WeaponStats[CurrentLevel - 1].Range;
            _duration = WeaponStats[CurrentLevel - 1].Duration;
        }

        private IEnumerator SpawnSuriken()
        {
            while (true)
            {
                var enemiesInRange = Physics2D.OverlapCircleAll(transform.position, _range, layerMask);
                if (enemiesInRange.Length > 0)
                {
                    var targetPosition = enemiesInRange[Random.Range(0, enemiesInRange.Length)].transform.position;
                    _direction = (targetPosition - transform.position).normalized;
                    var angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                    var suriken = objectPool.GetFromPool();
                    suriken.transform.SetParent(container);
                    suriken.transform.position = transform.position;
                    suriken.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }

                yield return _timeBetweenAttacks;
            }
        }
    }
}