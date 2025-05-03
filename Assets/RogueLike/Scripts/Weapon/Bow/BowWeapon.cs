using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.InputEvents;
using RogueLike.Scripts.Events.Weapon;
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
        [SerializeField] private GameObjectPool arrowPool;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource audioSource;
        
        private Vector3 _direction;
        private float _duration, _speed;
        private Transform container;

        protected override void Start()
        {
            WeaponType = WeaponType.Bow;
            base.Start();
        }

        private void OnEnable()
        {
            WeaponManager.AddWeapon(this);
            Activate();
            EventBus.Subscribe<OnAttacked>(StartThrowArrow);
            EventBus.Subscribe<OnWeaponLevelUpdated>(ChangeLevel);
        }

        private void OnDisable()
        {
            WeaponManager.RemoveWeapon(this);
            EventBus.Unsubscribe<OnAttacked>(StartThrowArrow);
            EventBus.Unsubscribe<OnWeaponLevelUpdated>(ChangeLevel);
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

        private void ChangeLevel(OnWeaponLevelUpdated evt)
        {
            if (WeaponType == evt.WeaponType && CurrentLevel < MaxLevel)
            {
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
            audioSource.Play();
            var arrow = arrowPool.GetFromPool();
            arrow.GetComponent<Arrow>().Init(new ProjectileParams(_speed, _duration, Damage, CurrentLevel));
            arrow.transform.SetParent(container);
            arrow.transform.position = shootPoint.position;
            arrow.transform.rotation = transform.rotation;
            arrow.SetActive(true);
            animator.SetTrigger(Idle);
        }

        private void StartThrowArrow(OnAttacked evt)
        {
            if (evt.WeaponType == WeaponType)
            {
                animator.SetTrigger(Attack);
            }
        }
    }
}