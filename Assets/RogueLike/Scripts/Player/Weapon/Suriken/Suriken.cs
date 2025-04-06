using UnityEngine;
using Zenject;

namespace RogueLike.Scripts.Player.Weapon.Suriken
{
    public class Suriken : Projectile
    {
        [SerializeField] private Transform sprite;
        private SurikenWeapon _surikenWeapon;

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_surikenWeapon.Duration);
            Damage = _surikenWeapon.Damage;
        }

        [Inject]
        private void Construct(SurikenWeapon surikenWeapon) => _surikenWeapon = surikenWeapon;

        private void Update()
        {
            sprite.transform.Rotate(0, 0, 500f * Time.deltaTime);
            transform.position += transform.right * (_surikenWeapon.Speed * Time.deltaTime);
        }
    }
}