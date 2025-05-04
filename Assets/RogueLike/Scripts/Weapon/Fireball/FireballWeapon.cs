using System.Collections;
using System.Collections.Generic;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.InputEvents;
using RogueLike.Scripts.Events.Weapon;
using RogueLike.Scripts.GameCore;
using RogueLike.Scripts.GameCore.Pool;
using UnityEngine;

namespace RogueLike.Scripts.Weapon.Fireball
{
    public class FireballWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool objectPool;
        
        private WaitForSeconds _interval;
        private float _rotationSpeed, _range;
        private Coroutine _attackCoroutine;
        private readonly List<GameObject> _fireballList = new();

        private void Update()
        {
            transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }

        protected override void Start()
        {
            WeaponType = WeaponType.Fireball;
            base.Start();
        }

        private void OnEnable()
        {
            WeaponManager.AddWeapon(this);
            EventBus.Subscribe<OnWeaponLevelUpdated>(ChangeLevel);
            EventBus.Subscribe<OnAttacked>(RunFireball);
        }

        private void OnDisable()
        {
            WeaponManager.RemoveWeapon(this);
            EventBus.Unsubscribe<OnWeaponLevelUpdated>(ChangeLevel);
            EventBus.Unsubscribe<OnAttacked>(RunFireball);
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _rotationSpeed = WeaponStats[CurrentLevel - 1].Speed;
            _range = WeaponStats[CurrentLevel - 1].Range;
        }
        
        private void ChangeLevel(OnWeaponLevelUpdated evt)
        {
            if (WeaponType == evt.WeaponType && CurrentLevel < MaxLevel)
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
                    AddFireball(new Vector3(_range, 0, 0));
                    break;
                case >=5 and <= 6:
                {
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
            var duration = WeaponStats[CurrentLevel - 1].Duration;
            
            while (duration > 0)
            {
                for (var i = 0; i < _fireballList.Count; i++)
                {
                    _fireballList[i].SetActive(!_fireballList[i].activeSelf);
                }
                duration -= Time.deltaTime;
                yield return null;
            }
            
            Deactivate();
        }

        private void RunFireball(OnAttacked evt)
        {
            if (evt.WeaponType != WeaponType) return;
            
            Activate();
        }

        public void Activate()
        {
            Debug.Log("Fireball Activate");
            SetupWeapon();
            _attackCoroutine = StartCoroutine(WeaponLifeCycle());
        }

        public void Deactivate()
        {
            if (_attackCoroutine == null) return;
            Debug.Log("Fireball Deactivate");
            StopCoroutine(_attackCoroutine);
            foreach (var current in _fireballList)
            {
                current.SetActive(false);
            }
            _fireballList.Clear();
        }
    }
}