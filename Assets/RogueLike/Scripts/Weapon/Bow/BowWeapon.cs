using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.InputEvents;
using RogueLike.Scripts.GameCore;
using RogueLike.Scripts.GameCore.Pool;
using UnityEngine;

namespace RogueLike.Scripts.Weapon.Bow
{
    public class BowWeapon : BaseWeapon, IActivate
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        private Camera mainCamera;
        
        [SerializeField] private Transform shootPoint, weaponTransform;
        [SerializeField] private ProjectilePool arrowPool;
        [SerializeField] private Animator animator;
        
        private Vector3 _direction;
        private float _duration, _speed;
        private Transform container;

        private void OnEnable()
        {
            Activate();
            EventBus.Subscribe<OnPlayerLevelChanged>(ChangeLevel);
            EventBus.Subscribe<OnAttacked>(StartThrowArrow);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnPlayerLevelChanged>(ChangeLevel);
            EventBus.Unsubscribe<OnAttacked>(StartThrowArrow);
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
            
            _speed = currentStats.Speed;
            _duration = currentStats.Duration;
        }

        private void ChangeLevel(OnPlayerLevelChanged evt)
        {
            if (CurrentLevel < MaxLevel)
            {
                Debug.Log($"Bow Level UP {evt.Level}");
                LevelUp();
            }
        }

        public void Activate()
        {
            mainCamera = Camera.main;
            container = GameObject.FindGameObjectWithTag("Weapon").transform;
            SetStats(0);
        }

        public void Deactivate()
        {
        }

        public void ThrowArrow()
        {
            var arrow = arrowPool.GetProjectile();
            arrow.GetComponent<Arrow>().Init(new ProjectileParams(_speed, _duration, Damage, CurrentLevel));
            arrow.transform.SetParent(container);
            arrow.transform.position = shootPoint.position;
            arrow.transform.rotation = transform.rotation;
            arrow.SetActive(true);
            animator.SetTrigger(Idle);
        }

        private void StartThrowArrow(OnAttacked evt)
        {
            if (gameObject.activeInHierarchy)
            {
                animator.SetTrigger(Attack);
            }
        }
    }
}