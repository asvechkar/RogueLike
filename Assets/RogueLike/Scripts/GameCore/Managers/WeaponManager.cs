using System.Collections.Generic;
using System.Linq;
using RogueLike.Scripts.Weapon;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class WeaponManager : MonoBehaviour
    {
        public List<BaseWeapon> Weapons { get; } = new();

        public void AddWeapon(BaseWeapon weapon)
        {
            Weapons.Add(weapon);
        }

        public void RemoveWeapon(BaseWeapon weapon)
        {
            if (Weapons.Contains(weapon))
            {
                Weapons.Remove(weapon);
            }
        }

        public int GetWeaponLevel(WeaponType weaponType)
        {
            foreach (var weapon in Weapons.Where(weapon => weapon.WeaponType == weaponType))
            {
                return weapon.CurrentLevel;
            }

            return 0;
        }

        public bool CanUpgrade(WeaponType weaponType)
        {
            foreach (var weapon in Weapons.Where(weapon => weapon.WeaponType == weaponType))
            {
                return weapon.CurrentLevel < weapon.MaxLevel;
            }
            
            return false;
        }

        public float GetWeaponCooldown(WeaponType weaponType)
        {
            foreach (var weapon in Weapons.Where(weapon => weapon.WeaponType == weaponType))
            {
                return weapon.GetCooldown();
            }
            
            return 0;
        }
    }
}