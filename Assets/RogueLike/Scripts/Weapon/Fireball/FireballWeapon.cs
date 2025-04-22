using System.Collections;
using System.Collections.Generic;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.GameCore;
using RogueLike.Scripts.GameCore.Pool;
using UnityEngine;

namespace RogueLike.Scripts.Weapon.Fireball
{
    public class FireballWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool objectPool;
        
        private WaitForSeconds _interval, _timeBetweenAttack;
        private float _rotationSpeed, _range;
        private Coroutine _attackCoroutine;
        private readonly List<GameObject> _fireballList = new();

        private void Update()
        {
            transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }

        protected override void Start()
        {
            base.Start();
            SetupWeapon();
            Activate();
        }

        public override void LevelUp()
        {
            base.LevelUp();
            SetupWeapon();
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _rotationSpeed = WeaponStats[CurrentLevel - 1].Speed;
            _range = WeaponStats[CurrentLevel - 1].Range;
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
        }
        
        private void OnEnable() => EventBus.Subscribe<OnPlayerLevelChanged>(ChangeLevel);
        private void OnDisable() => EventBus.Unsubscribe<OnPlayerLevelChanged>(ChangeLevel);

        private void ChangeLevel(OnPlayerLevelChanged evt)
        {
            if (CurrentLevel < MaxLevel)
            {
                LevelUp();
            }
        }

        private void SetupWeapon()
        {
            foreach (var current in _fireballList)
            {
                current.SetActive(false);
            }
            _fireballList.Clear();
            
            switch (CurrentLevel)
            {
                case >=0 and <= 4:
                    Debug.Log($"Fireball Level 4 {CurrentLevel}");
                    AddFireball(new Vector3(_range, 0, 0));
                    break;
                case >=5 and <= 6:
                {
                    Debug.Log($"Fireball Level 6 {CurrentLevel}");
                    for (var i = 0; i < 2; i++)
                    {
                        var angleOffset = 360f / 2 * i;
                        
                        var x = transform.position.x + Mathf.Cos(angleOffset * Mathf.Deg2Rad) * _range;
                        var y = transform.position.y + Mathf.Sin(angleOffset * Mathf.Deg2Rad) * _range;
                        
                        AddFireball(new Vector3(x, y, 0));
                    }
                    break;
                }
                default:
                {
                    Debug.Log($"Fireball Level {CurrentLevel}");
                    for(var i = 0; i < 3; i++)
                    {
                        var angleOffset = 360f / 3 * i;
                        
                        var x = transform.position.x + Mathf.Cos(angleOffset * Mathf.Deg2Rad) * _range;
                        var y = transform.position.y + Mathf.Sin(angleOffset * Mathf.Deg2Rad) * _range;
                        
                        AddFireball(new Vector3(x, y, 0));
                    }

                    break;
                }
            }
        }

        private void AddFireball(Vector3 position)
        {
            var fireball = objectPool.GetFromPool();
            fireball.GetComponent<FireBall>().Init(new ProjectileParams(0, 0, Damage, 0));
            fireball.transform.SetParent(transform);
            fireball.transform.localPosition = position;
            _fireballList.Add(fireball);
        }

        private IEnumerator WeaponLifeCycle()
        {
            while (true)
            {
                for (var i = 0; i < _fireballList.Count; i++)
                {
                    _fireballList[i].SetActive(!_fireballList[i].activeSelf);
                }
                yield return _timeBetweenAttack;
            }
        }

        public void Activate()
        {
            _attackCoroutine = StartCoroutine(WeaponLifeCycle());
        }

        public void Deactivate()
        {
            if (_attackCoroutine == null) return;
            
            StopCoroutine(_attackCoroutine);
        }
    }
}