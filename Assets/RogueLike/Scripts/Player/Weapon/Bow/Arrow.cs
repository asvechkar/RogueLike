using Reflex.Attributes;
using UnityEngine;

namespace RogueLike.Scripts.Player.Weapon.Bow
{
    public class Arrow : Projectile
    {
        [Inject] private BowWeapon _bowWeapon;

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_bowWeapon.Duration);
            Damage = _bowWeapon.Damage;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            if (_bowWeapon.CurrentLevel <= 4)
            {
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            transform.position += transform.up * (-1 * _bowWeapon.Speed * Time.deltaTime);
        }
    }
}