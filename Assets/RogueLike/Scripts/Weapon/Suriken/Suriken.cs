using UnityEngine;

namespace RogueLike.Scripts.Weapon.Suriken
{
    public class Suriken : Projectile
    {
        [SerializeField] private Transform sprite;
        
        private ProjectileParams _params;

        public void Init(ProjectileParams projectileParams)
        {
            _params = projectileParams;
            Timer = new WaitForSeconds(_params.Duration);
            Damage = _params.Damage;
        }

        private void Update()
        {
            sprite.transform.Rotate(0, 0, 500f * Time.deltaTime);
            transform.position += transform.right * (_params.Speed * Time.deltaTime);
        }
    }
}