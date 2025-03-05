using System.Collections;
using System.Collections.Generic;
using GameCore;
using UnityEngine;

namespace Player.Weapon
{
    public class FireballWeapon : BaseWeapon, IActivate
    {
        [Header("Single")]
        [SerializeField] private SpriteRenderer spriteRenderer1X;
        [SerializeField] private Collider2D collider1X;
        [SerializeField] private Transform transformSprite1X, targetContainer1X;
        
        [Header("Double")]
        [SerializeField] private List<SpriteRenderer> spriteRenderer2X = new();
        [SerializeField] private List<Collider2D> collider2X = new();
        [SerializeField] private List<Transform> transformSprite2X = new();
        [SerializeField] private Transform targetContainer2X;
        
        [Header("Triple")]
        [SerializeField] private List<SpriteRenderer> spriteRenderer3X = new();
        [SerializeField] private List<Collider2D> collider3X = new();
        [SerializeField] private List<Transform> transformSprite3X = new();
        [SerializeField] private Transform targetContainer3X;
        
        private WaitForSeconds _interval, _duration, _timeBetweenAttack;
        private float _rotationSpeed, _range;
        private Coroutine _attackCoroutine;

        private void Update()
        {
            transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }

        protected override void Start()
        {
            base.Start();
            SetupWeapon();
            Activate();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
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
            _duration = new WaitForSeconds(WeaponStats[CurrentLevel - 1].Duration);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
        }

        private void SetupWeapon()
        {
            switch (CurrentLevel)
            {
                case < 4:
                    targetContainer1X.gameObject.SetActive(true);
                    targetContainer2X.gameObject.SetActive(false);
                    targetContainer3X.gameObject.SetActive(false);
                    transformSprite1X.localPosition = new Vector3(_range, 0, 0);
                    collider1X.offset = new Vector2(_range, 0);
                    break;
                case < 6:
                {
                    targetContainer1X.gameObject.SetActive(false);
                    targetContainer3X.gameObject.SetActive(false);
                    targetContainer2X.gameObject.SetActive(true);

                    foreach (var current in collider2X)
                    {
                        current.gameObject.SetActive(true);
                    }
                
                    transformSprite2X[0].localPosition = new Vector3(_range, 0, 0);
                    collider2X[0].offset = new Vector2(_range, 0);
                    transformSprite2X[1].localPosition = new Vector3(-_range, 0, 0);
                    collider2X[1].offset = new Vector2(-_range, 0);
                    break;
                }
                default:
                {
                    targetContainer1X.gameObject.SetActive(false);
                    targetContainer2X.gameObject.SetActive(false);
                    targetContainer3X.gameObject.SetActive(true);

                    foreach (var current in collider3X)
                    {
                        current.gameObject.SetActive(true);
                    }

                    for(var i = 0; i < transformSprite3X.Count; i++)
                    {
                        var delta = _range / 100f;
                        transformSprite3X[i].localPosition = new Vector3(transformSprite3X[i].localPosition.x * delta, 0, 0);
                        collider3X[i].offset = new Vector2(collider3X[i].offset.x * delta, 0);
                    }

                    break;
                }
            }
        }

        private IEnumerator WeaponLifeCycle()
        {
            while (true)
            {
                if (CurrentLevel < 4)
                {
                    spriteRenderer1X.enabled = !spriteRenderer1X.enabled;
                    collider1X.enabled = !collider1X.enabled;
                }
                else if (CurrentLevel < 6)
                {
                    for (var i = 0; i < spriteRenderer2X.Count; i++)
                    {
                        spriteRenderer2X[i].enabled = !spriteRenderer2X[i].enabled;
                        collider2X[i].enabled = !collider2X[i].enabled;
                    }
                }
                else
                {
                    for (var i = 0; i < spriteRenderer3X.Count; i++)
                    {
                        spriteRenderer3X[i].enabled = !spriteRenderer3X[i].enabled;
                        collider3X[i].enabled = !collider3X[i].enabled;
                    }
                }

                _interval = spriteRenderer1X.enabled || spriteRenderer2X[0].enabled || spriteRenderer3X[0].enabled ? _duration : _timeBetweenAttack;

                yield return _interval;
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