using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.InputEvents;
using RogueLike.Scripts.Events.Weapon;
using RogueLike.Scripts.GameCore;
using RogueLike.Scripts.GameCore.Pool;
using UnityEngine;

namespace RogueLike.Scripts.Weapon.Trap
{
    public class TrapWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool trapPool;
        
        private Transform container;

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
            EventBus.Subscribe<OnAttacked>(SpawnTrap);
        }

        private void OnDisable()
        {
            WeaponManager.RemoveWeapon(this);
            EventBus.Unsubscribe<OnWeaponLevelUpdated>(ChangeLevel);
            EventBus.Unsubscribe<OnAttacked>(SpawnTrap);
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
        }

        private void SpawnTrap(OnAttacked evt)
        {
            if (evt.WeaponType != WeaponType) return;
            
            var trap = trapPool.GetFromPool();
            
            trap.GetComponentInChildren<Trap>().Init(new ProjectileParams(0, 0, Damage, CurrentLevel));
            trap.transform.SetParent(container);
            trap.transform.position = transform.position;
        }
    }
}