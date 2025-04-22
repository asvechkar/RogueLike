using UnityEngine;

namespace RogueLike.Scripts.Weapon.Bow
{
    public class Arrow : Projectile
    {
        private ProjectileParams _params;

        public void Init(ProjectileParams projectileParams)
        {
            _params = projectileParams;
            Timer = new WaitForSeconds(_params.Duration);
            Damage = _params.Damage;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            if (_params.WeaponLevel <= 4)
            {
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            transform.position += transform.up * (-1 * _params.Speed * Time.deltaTime);
        }
    }
}