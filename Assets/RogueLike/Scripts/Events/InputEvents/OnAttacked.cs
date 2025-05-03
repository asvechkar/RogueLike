using RogueLike.Scripts.Weapon;

namespace RogueLike.Scripts.Events.InputEvents
{
    public class OnAttacked
    {
        public WeaponType WeaponType { get; private set; }

        public OnAttacked(WeaponType weaponType)
        {
            WeaponType = weaponType;
        }
    }
}