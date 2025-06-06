using RogueLike.Scripts.Enemy;
using UnityEngine;

namespace RogueLike.Scripts.Weapon.Fireball
{
    public class FireBall : MonoBehaviour
    {
        private ProjectileParams _params;

        public void Init(ProjectileParams projectileParams)
        {
            _params = projectileParams;
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out EnemyHealth enemy)) return;

            enemy.TakeDamage(_params.Damage);
        }
    }
}