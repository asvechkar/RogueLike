using System.Collections;
using System.Collections.Generic;
using GameCore;
using GameCore.Pool;
using UnityEngine;

namespace Player.Weapon.FrostBolt
{
    public class FrostBoltWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool objectPool;
        [SerializeField] private Transform container;
        [SerializeField] private List<Transform> shootPoints = new();
        
        private WaitForSeconds _timeBetweenAttacks;
        private Coroutine _frostBoltCoroutine;
        private float _duration, _speed;
        private Vector3 _direction;

        public float Speed => _speed;
        public Vector3 Direction => _direction;
        public float Duration => _duration;

        private void OnEnable()
        {
            Activate();
        }

        public void Activate()
        {
            SetStats(0);
            _frostBoltCoroutine = StartCoroutine(StartThrowFrostBolt());
        }

        public void Deactivate()
        {
            if (_frostBoltCoroutine != null)
                StopCoroutine(_frostBoltCoroutine);
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            var currentStats = WeaponStats[CurrentLevel - 1];
            
            _timeBetweenAttacks = new WaitForSeconds(currentStats.TimeBetweenAttack);
            _speed = currentStats.Speed;
            _duration = currentStats.Duration;
        }

        private IEnumerator StartThrowFrostBolt()
        {
            while (true)
            {
                for (var i = 0; i < shootPoints.Count; i++)
                {
                    _direction = (shootPoints[i].position - transform.position).normalized;
                    var angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                    var bolt = objectPool.GetFromPool();
                    bolt.transform.SetParent(container);
                    bolt.transform.position = transform.position;
                    bolt.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                
                yield return _timeBetweenAttacks;
            }
        }
    }
}