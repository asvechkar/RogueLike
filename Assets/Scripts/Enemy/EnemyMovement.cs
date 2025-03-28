using System;
using System.Collections;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        
        [SerializeField] private float speed;
        [SerializeField] private Animator animator;
        [SerializeField] private float freezeTimer;
        
        private Vector3 _direction;
        private PlayerMovement _playerMovement;
        private WaitForSeconds _checkTime = new(3f);
        private Coroutine _distanceToHide;

        private void OnEnable()
        {
            _distanceToHide = StartCoroutine(CheckDistanceToHide());
        }

        private void OnDisable()
        {
            if (_distanceToHide == null) return;
            
            StopCoroutine(_distanceToHide);
        }

        private void Update()
        {
            Move();
        }

        [Inject]
        private void Construct(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }

        private void Move()
        {
            _direction = (_playerMovement.transform.position - transform.position).normalized;
            transform.position += _direction * (speed * Time.deltaTime);
            
            animator.SetFloat(Horizontal, _direction.x);
            animator.SetFloat(Vertical, _direction.y);
        }

        private IEnumerator CheckDistanceToHide()
        {
            while (true)
            {
                var distance = Vector3.Distance(transform.position, _playerMovement.transform.position);
                if (distance >= 20f)
                {
                    gameObject.SetActive(false);
                }
                yield return _checkTime;
            }
        }
        
        public void Freeze(float multiplier)
        {
            StartCoroutine(FreezeRoutine(multiplier));
        }

        private IEnumerator FreezeRoutine(float multiplier)
        {
            var originalSpeed = speed;
            speed = speed * multiplier;

            yield return new WaitForSeconds(freezeTimer);

            speed = originalSpeed;
        }
    }
}
