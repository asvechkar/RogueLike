using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField] private float damage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out PlayerHealth player)) return;
            
            player.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}