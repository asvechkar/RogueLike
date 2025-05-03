using RogueLike.Scripts.Weapon;

namespace RogueLike.Scripts.Events.Weapon
{
    public class OnWeaponLevelChanged
    {
        public WeaponType WeaponType { get; private set; }
        public int Level { get; private set; }

        public OnWeaponLevelChanged(WeaponType weaponType, int level)
        {
            WeaponType = weaponType;
            Level = level;
        }
    }
}