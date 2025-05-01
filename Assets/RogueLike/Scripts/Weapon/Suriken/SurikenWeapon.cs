using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.InputEvents;
using RogueLike.Scripts.GameCore;
using RogueLike.Scripts.GameCore.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RogueLike.Scripts.Weapon.Suriken
{
    public class SurikenWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool objectPool;

        [SerializeField] private LayerMask layerMask;

        private Transform container;
        private float _duration, _speed, _range;
        private Vector3 _direction;

        protected override void Start()
        {
            WeaponType = WeaponType.Suriken;
            base.Start();
        }

        private void OnEnable()
        {
            WeaponManager.AddWeapon(this);
            Activate();
            EventBus.Subscribe<OnAttacked>(SpawnSuriken);
            EventBus.Subscribe<OnWeaponLevelUpdated>(ChangeLevel);
        }
        
        private void OnDisable()
        {
            WeaponManager.RemoveWeapon(this);
            EventBus.Unsubscribe<OnAttacked>(SpawnSuriken);
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
            base.SetStats(CurrentLevel - 1);
            
            _speed = WeaponStats[CurrentLevel - 1].Speed;
            _range = WeaponStats[CurrentLevel - 1].Range;
            _duration = WeaponStats[CurrentLevel - 1].Duration;
        }

        private void SpawnSuriken(OnAttacked evt)
        {
            if (!gameObject.activeInHierarchy) return;
            
            var enemiesInRange = Physics2D.OverlapCircleAll(transform.position, _range, layerMask);

            if (enemiesInRange.Length <= 0) return;
            
            var targetPosition = enemiesInRange[Random.Range(0, enemiesInRange.Length)].transform.position;
            _direction = (targetPosition - transform.position).normalized;
            var angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            var suriken = objectPool.GetFromPool();
            suriken.GetComponent<Suriken>().Init(new ProjectileParams(_speed, _duration, Damage, CurrentLevel));
            suriken.transform.SetParent(container);
            suriken.transform.position = transform.position;
            suriken.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}