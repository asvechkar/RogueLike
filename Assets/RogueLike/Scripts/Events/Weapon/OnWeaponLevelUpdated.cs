using RogueLike.Scripts.Weapon;

namespace RogueLike.Scripts.Events.Weapon
{
    public class OnWeaponLevelUpdated
    {
        public WeaponType WeaponType { get; private set; }

        public OnWeaponLevelUpdated(WeaponType weaponType)
        {
            WeaponType = weaponType;
        }
    }
}