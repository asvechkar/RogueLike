using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private Vector3 _direction;
        
        private PlayerMovement _playerMovement;

        [Inject]
        private void Construct(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }

        private void Move()
        {
            _direction = (_playerMovement.transform.position - transform.position).normalized;
            transform.position += _direction * (speed * Time.deltaTime);
        }
    }
}
