using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int LastDirectionX = Animator.StringToHash("LastDirectionX");
        private static readonly int LastDirectionY = Animator.StringToHash("LastDirectionY");

        [SerializeField] private float speed;
        [SerializeField] private Animator animator;
        
        private Vector3 _movement;
        public Vector3 Movement => _movement;
        
        private Vector2 _lastDirection;

        private void Update()
        {
            transform.position += _movement.normalized * (speed * Time.deltaTime);
            
            animator.SetFloat(Horizontal, _movement.x);
            animator.SetFloat(Vertical, _movement.y);
            animator.SetFloat(Speed, _movement.sqrMagnitude);
            animator.SetFloat(LastDirectionX, _lastDirection.x);
            animator.SetFloat(LastDirectionY, _lastDirection.y);
        }

        public void Move(InputAction.CallbackContext context)
        {
            if (_movement != Vector3.zero)
            {
                _lastDirection = new Vector2(_movement.x, _movement.y);
            }
            var movement = context.ReadValue<Vector2>();
            _movement = new Vector3(movement.x, movement.y, 0);
        }
    }
}
