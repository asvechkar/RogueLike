using System.Collections;
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

        private void OnEnable()
        {
            Activate();
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