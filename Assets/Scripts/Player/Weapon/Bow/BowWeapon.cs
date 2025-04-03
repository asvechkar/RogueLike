using System;
using System.Collections;
using GameCore;
using GameCore.Pool;
using UnityEngine;
using Zenject;

namespace Player.Weapon.Bow
{
    public class BowWeapon : BaseWeapon, IActivate
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Attack = Animator.StringToHash("Attack");
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform container, shootPoint, weaponTransform;
        [SerializeField] private ProjectilePool arrowPool;
        [SerializeField] private Animator animator;
        
        private WaitForSeconds _timeBetweenAttack;
        private PlayerMovement _playerMovement;
        private Coroutine _bowCoroutine;
        private Vector3 _direction;
        private float _duration, _speed;

        public float Duration => _duration;
        public float Speed => _speed;

        private void OnEnable()
        {
            Activate();
        }

        private void Update()
        {
            _direction = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            weaponTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            var currentStats = WeaponStats[CurrentLevel - 1];
            
            _timeBetweenAttack = new WaitForSeconds(currentStats.TimeBetweenAttack);
            _speed = currentStats.Speed;
            _duration = currentStats.Duration;
        }

        public void Activate()
        {
            SetStats(0);
            _bowCoroutine = StartCoroutine(StartThrowArrow());
        }

        public void Deactivate()
        {
            if (_bowCoroutine != null)
                StopCoroutine(_bowCoroutine);
        }

        public void ThrowArrow()
        {
            var arrow = arrowPool.GetProjectile();
            arrow.transform.SetParent(container);
            arrow.transform.position = shootPoint.position;
            arrow.transform.rotation = transform.rotation;
            animator.SetTrigger(Idle);
        }

        private IEnumerator StartThrowArrow()
        {
            while (true)
            {
                if (_playerMovement.Movement != Vector3.zero)
                {
                    animator.SetTrigger(Attack);
                }
                
                yield return _timeBetweenAttack;
            }
        }
        
        [Inject]
        private void Construct(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }
    }
}