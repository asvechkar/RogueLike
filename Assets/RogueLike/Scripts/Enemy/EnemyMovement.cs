using System.Collections;
using Reflex.Attributes;
using RogueLike.Scripts.Player;
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
        
        [Inject] private readonly PlayerMovement _playerMovement;
        
        private WaitForSeconds _checkTime = new(3f);
        private Coroutine _distanceToHide;
        private float _originalSpeed;

        private void Awake()
        {
            _originalSpeed = speed;
        }

        private void OnEnable()
        {
            speed = _originalSpeed;
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
