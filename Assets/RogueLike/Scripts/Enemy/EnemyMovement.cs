using System.Collections;
using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Player;
using RogueLike.Scripts.GameCore;
using RogueLike.Scripts.GameCore.Managers;
using UnityEngine;

namespace RogueLike.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        
        [SerializeField] private float speed = 1f;
        [SerializeField] private Animator animator;
        [SerializeField] private float freezeTimer;
        
        [Inject] private GameManager _gameManager;
        
        private Vector3 _direction;
        
        private float _originalSpeed;

        private void Start()
        {
            switch (_gameManager.Difficulty)
            {
                case GameDifficultyType.Easy:
                default:
                    speed *= 1;
                    break;
                case GameDifficultyType.Normal:
                    speed *= 2;
                    break;
                case GameDifficultyType.Hard:
                    speed *= 3;
                    break;
            }
            _originalSpeed = speed;
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnPlayerMoved>(Move);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnPlayerMoved>(Move);
        }

        private void Move(OnPlayerMoved evt)
        {
            _direction = (evt.Position - transform.position).normalized;
            transform.position += _direction * (speed * Time.deltaTime);
            
            animator.SetFloat(Horizontal, _direction.x);
            animator.SetFloat(Vertical, _direction.y);
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
