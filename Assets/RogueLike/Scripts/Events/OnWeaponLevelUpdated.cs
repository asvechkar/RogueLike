using RogueLike.Scripts.Weapon;

namespace RogueLike.Scripts.Events
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