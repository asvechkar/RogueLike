using System;
using System.Collections;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.GameCore;
using RogueLike.Scripts.GameCore.Pool;
using UnityEngine;

namespace RogueLike.Scripts.Weapon.Trap
{
    public class TrapWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool trapPool;
        
        private Transform container;
        private WaitForSeconds _timeBetweenAttacks;
        private Coroutine _trapCoroutine;

        protected override void Start()
        {
            WeaponType = WeaponType.Trap;
            base.Start();
        }
        
        private void ChangeLevel(OnWeaponLevelUpdated evt)
        {
            if (WeaponType == evt.WeaponType && CurrentLevel < MaxLevel)
            {
                LevelUp();
            }
        }

        private void OnEnable()
        {
            WeaponManager.AddWeapon(this);
            Activate();
            EventBus.Subscribe<OnWeaponLevelUpdated>(ChangeLevel);
        }

        private void OnDisable()
        {
            WeaponManager.RemoveWeapon(this);
            EventBus.Unsubscribe<OnWeaponLevelUpdated>(ChangeLevel);
        }

        public void Activate()
        {
            container = GameObject.FindGameObjectWithTag("Weapon").transform;
            SetStats(0);
            _trapCoroutine = StartCoroutine(SpawnTrap());
        }

        public void Deactivate()
        {
            if (_trapCoroutine != null)
                StopCoroutine(_trapCoroutine);
        }

        protected override void SetStats(int value)
        {
            base.SetStats(CurrentLevel - 1);
            _timeBetweenAttacks = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
        }

        private IEnumerator SpawnTrap()
        {
            while (true)
            {
                var trap = trapPool.GetFromPool();
                trap.GetComponent<Trap>().Init(new ProjectileParams(0, 0, Damage, CurrentLevel));
                trap.transform.SetParent(container);
                trap.transform.position = transform.position;
                
                yield return _timeBetweenAttacks;
            }
        }
    }
}