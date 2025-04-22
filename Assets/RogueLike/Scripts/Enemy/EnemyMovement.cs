using System.Collections;
using RogueLike.Scripts.Events;
using UnityEngine;

namespace RogueLike.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        
        [SerializeField] private float speed;
        [SerializeField] private Animator animator;
        [SerializeField] private float freezeTimer;
        
        private Vector3 _direction;
        
        private WaitForSeconds _checkTime = new(3f);
        private Coroutine _distanceToHide;
        private float _originalSpeed;
        private Vector3 _playerPosition;

        private void Awake()
        {
            _originalSpeed = speed;
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnPlayerMoved>(Move);
            speed = _originalSpeed;
            _distanceToHide = StartCoroutine(CheckDistanceToHide());
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnPlayerMoved>(Move);
            if (_distanceToHide == null) return;
            
            StopCoroutine(_distanceToHide);
        }

        private void Move(OnPlayerMoved evt)
        {
            _playerPosition = evt.Position;
            _direction = (evt.Position - transform.position).normalized;
            transform.position += _direction * (speed * Time.deltaTime);
            
            animator.SetFloat(Horizontal, _direction.x);
            animator.SetFloat(Vertical, _direction.y);
        }

        private IEnumerator CheckDistanceToHide()
        {
            while (true)
            {
                if (_playerPosition != Vector3.zero)
                {
                    var distance = Vector3.Distance(transform.position, _playerPosition);
                    if (distance >= 20f)
                    {
                        gameObject.SetActive(false);
                    }
                }

                yield return _checkTime;
            }
        }
        
        public void Freeze(float multiplier)
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(FreezeRoutine(multiplier));
            }
        }

        private IEnumerator FreezeRoutine(float multiplier)
        {
            speed /= multiplier;

            yield return new WaitForSeconds(freezeTimer);

            speed = _originalSpeed;
        }
    }
}
