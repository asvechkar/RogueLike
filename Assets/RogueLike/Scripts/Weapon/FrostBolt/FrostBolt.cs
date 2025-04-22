using RogueLike.Scripts.Enemy;
using UnityEngine;

namespace RogueLike.Scripts.Weapon.FrostBolt
{
    public class FrostBolt : Projectile
    {
        private ProjectileParams _params;

        public void Init(ProjectileParams projectileParams)
        {
            _params = projectileParams;
            Timer = new WaitForSeconds(_params.Duration);
            Damage = _params.Damage;
        }

        private void Update()
        {
            transform.position += transform.right * (_params.Speed * Time.deltaTime);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out EnemyHealth enemy)) return;
            
                
            enemy.TakeDamage(Damage);
            enemy.GetComponent<EnemyMovement>().Freeze(3);

            if (_params.WeaponLevel <= 4)
            {
                gameObject.SetActive(false);
            }
        }
    }
}