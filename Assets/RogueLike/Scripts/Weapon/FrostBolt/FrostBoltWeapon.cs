using System.Collections.Generic;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.InputEvents;
using RogueLike.Scripts.Events.Weapon;
using RogueLike.Scripts.GameCore;
using RogueLike.Scripts.GameCore.Pool;
using UnityEngine;

namespace RogueLike.Scripts.Weapon.FrostBolt
{
    public class FrostBoltWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool objectPool;

        [SerializeField] private List<Transform> shootPoints = new();

        private Transform container;
        private float _duration, _speed;
        private Vector3 _direction;

        protected override void Start()
        {
            WeaponType = WeaponType.Frostbolt;
            base.Start();
        }

        private void OnEnable()
        {
            WeaponManager.AddWeapon(this);
            Activate();
            EventBus.Subscribe<OnAttacked>(StartThrowFrostBolt);
            EventBus.Subscribe<OnWeaponLevelUpdated>(ChangeLevel);
        }
        
        private void OnDisable()
        {
            WeaponManager.RemoveWeapon(this);
            EventBus.Unsubscribe<OnAttacked>(StartThrowFrostBolt);
            EventBus.Unsubscribe<OnWeaponLevelUpdated>(ChangeLevel);
        }
        
        private void ChangeLevel(OnWeaponLevelUpdated evt)
        {
            if (WeaponType == evt.WeaponType && CurrentLevel < MaxLevel)
            {
                LevelUp();
            }
        }

        public void Activate()
        {
            container = GameObject.FindGameObjectWithTag("Weapon").transform;
            SetStats(0);
        }

        public void Deactivate()
        {
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            var currentStats = WeaponStats[CurrentLevel - 1];
            
            _speed = currentStats.Speed;
            _duration = currentStats.Duration;
        }

        private void StartThrowFrostBolt(OnAttacked evt)
        {
            if (evt.WeaponType != WeaponType) return;
            
            for (var i = 0; i < shootPoints.Count; i++)
            {
                _direction = (shootPoints[i].position - transform.position).normalized;
                var angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                var bolt = objectPool.GetFromPool();
                bolt.GetComponent<FrostBolt>().Init(new ProjectileParams(_speed, _duration, Damage, CurrentLevel));
                bolt.transform.SetParent(container);
                bolt.transform.position = transform.position;
                bolt.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
}