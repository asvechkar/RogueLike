using Reflex.Attributes;
using RogueLike.Scripts.Enemy;
using UnityEngine;

namespace RogueLike.Scripts.Player.Weapon.FrostBolt
{
    public class FrostBolt : Projectile
    {
        [Inject] private FrostBoltWeapon _frostBoltWeapon;

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_frostBoltWeapon.Duration);
            Damage = _frostBoltWeapon.Damage;
        }

        private void Update()
        {
            transform.position += transform.right * (_frostBoltWeapon.Speed * Time.deltaTime);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out EnemyHealth enemy)) return;
            
                
            enemy.TakeDamage(Damage);
            enemy.GetComponent<EnemyMovement>().Freeze(3);

            if (_frostBoltWeapon.CurrentLevel <= 4)
            {
                gameObject.SetActive(false);
            }
        }
    }
}