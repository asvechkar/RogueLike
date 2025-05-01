using System.Collections.Generic;
using System.Linq;
using RogueLike.Scripts.Weapon;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class WeaponManager : MonoBehaviour
    {
        private List<BaseWeapon> weapons = new();

        public void AddWeapon(BaseWeapon weapon)
        {
            weapons.Add(weapon);
        }

        public void RemoveWeapon(BaseWeapon weapon)
        {
            if (weapons.Contains(weapon))
            {
                weapons.Remove(weapon);
            }
        }

        public int GetWeaponLevel(WeaponType weaponType)
        {
            foreach (var weapon in weapons.Where(weapon => weapon.WeaponType == weaponType))
            {
                return weapon.CurrentLevel;
            }

            return 0;
        }

        public bool CanUpgrade(WeaponType weaponType)
        {
            foreach (var weapon in weapons.Where(weapon => weapon.WeaponType == weaponType))
            {
                return weapon.CurrentLevel < weapon.MaxLevel;
            }
            
            return false;
        }
    }
}