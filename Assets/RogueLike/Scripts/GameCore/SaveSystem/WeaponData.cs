using RogueLike.Scripts.Weapon;

namespace RogueLike.Scripts.GameCore.SaveSystem
{
    public struct WeaponData
    {
        public Weapon[] Weapons;
    }

    public struct Weapon
    {
        public WeaponType Type;
        public int Level;
    }
}