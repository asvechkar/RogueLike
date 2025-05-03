using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.InputEvents;
using RogueLike.Scripts.Events.Player;
using RogueLike.Scripts.GameCore.Managers;
using UnityEngine;

namespace RogueLike.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int LastDirectionX = Animator.StringToHash("LastDirectionX");
        private static readonly int LastDirectionY = Animator.StringToHash("LastDirectionY");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        [SerializeField] private float speed;
        [SerializeField] private float runSpeed;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioClip moveSound;
        
        [Inject] private PlayerManager _playerManager;
        
        private Vector3 _movement;
        private bool _isRunning;
        private Vector2 _lastDirection;
        private AudioSource _audioSource;

        private void Start()
        {
            _playerManager.SetSpeed(speed);
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = moveSound;
        }

        private void Update()
        {
            var currentSpeed = speed;
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = runSpeed;
                _isRunning = true;
            }
            else
            {
                _isRunning = false;
            }
            
            transform.position += _movement.normalized * (currentSpeed * Time.deltaTime);
            
            animator.SetFloat(Horizontal, _movement.x);
            animator.SetBool(IsRunning, _isRunning);
            animator.SetFloat(Vertical, _movement.y);
            animator.SetFloat(Speed, _movement.sqrMagnitude);
            animator.SetFloat(LastDirectionX, _lastDirection.x);
            animator.SetFloat(LastDirectionY, _lastDirection.y);
            
            EventBus.Invoke(new OnPlayerMoved(transform.position));
        }
        
        private void OnEnable()
        {
            EventBus.Invoke(new OnPlayerSpeedChanged(speed));
            EventBus.Subscribe<OnMoved>(Move);
            EventBus.Subscribe<OnPlayerSkillChanged>(UpdateSpeed);
        }
        
        private void OnDisable()
        {
            EventBus.Unsubscribe<OnMoved>(Move);
            EventBus.Unsubscribe<OnPlayerSkillChanged>(UpdateSpeed);
        }

        private void Move(OnMoved evt)
        {
            if (_movement != Vector3.zero)
            {
                _lastDirection = new Vector2(_movement.x, _movement.y);
            }
            _movement = new Vector3(evt.Position.x, evt.Position.y, 0);
            _audioSource.Play();
        }

        private void UpdateSpeed(OnPlayerSkillChanged evt)
        {
            if (evt.Skill != PlayerSkillType.Speed) return;
            speed = _playerManager.Speed;
            EventBus.Invoke(new OnPlayerSpeedChanged(speed));
        }
    }
}
